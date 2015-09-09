using System.Collections.Generic.ThreadSafe;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading;

namespace System.Windows.Forms
{
    public class MapnikDownloader : System.ITileDownloader
    {
        public delegate void TileReportingDelegate(MapTile Tile);

        public event TileReportingDelegate OnTileDownloading;
        public event TileReportingDelegate OnTileDownloaded;

        private const Int32 _TileSize = 256;
        private const String _TileFormat = "http://tile.openstreetmap.org/{0}/{1}/{2}.png";
		
		private UnboundedNonBlockingQueue<MapTile> _Queue;
		private Boolean _Stop;
		private Object _StopLock;
		private Thread _Thread;
		
		public MapnikDownloader()
		{
			_Queue = new UnboundedNonBlockingQueue<MapTile>();
			_Stop = false;
			_StopLock = new Object();
			_Thread = new Thread(_Run);
		}
		
		public void Start()
		{
			_Thread.Start();
		}
		
		public void Stop()
		{
			lock(_StopLock)
			{
				_Stop = true;
			}
		}

        public bool SupportsTile(MapTile Tile)
        {
            return (Tile.Zoom >= 0) && (Tile.Zoom <= 18) && (Tile.X >= 0) && (Tile.X < (1 << Tile.Zoom)) && (Tile.Y >= 0) && (Tile.Y < (1 << Tile.Zoom));
        }

        public void FetchTile(MapTile Tile)
        {
            System.Console.WriteLine("Queuing to download " + Tile.Zoom + ":" + Tile.X + "/" + Tile.Y + ".");
            OnTileDownloading(Tile);
			_Queue.Enqueue(Tile);
        }

        public String GetSetIdentifier()
        {
            return "mapnik";
        }

        public Int32 GetTileSize()
        {
            return _TileSize;
        }
		
		private void _Run()
		{
			Console.WriteLine("Starting tile downloader thread.");
			while(true)
			{
				lock(_StopLock)
				{
					if(_Stop == true)
					{
						break;
					}
				}
				
				var Tile = _Queue.Dequeue();
				
				if(Tile != null)
				{
					Console.WriteLine("Downloading " + Tile.Zoom + ":" + Tile.X + "/" + Tile.Y + ".");
					try
					{
						var Request = WebRequest.Create(String.Format(_TileFormat, Tile.Zoom, Tile.X, Tile.Y)) as HttpWebRequest;

						Request.KeepAlive = true;
						Request.Timeout = 500;
						Request.Proxy = null;
						Request.UserAgent = "GPX Track Viewer  (hagen.moebius@googlemail.com)";
						
						var Response = Request.GetResponse();
						var Stream = Response.GetResponseStream();

						if(Stream != null)
						{
							Tile.SetSetIdentifier(GetSetIdentifier());
							Tile.SetExpireDateTime(DateTime.Parse(Response.Headers["Expires"]));
							Tile.SetImage(new Bitmap(Stream));
							Stream.Close();
						}
						Response.Close();
					}
					catch(WebException)
					{
						Tile.SetImage(null);
					}
					OnTileDownloaded(Tile);
					Console.WriteLine("Downloaded " + Tile.Zoom + ":" + Tile.X + "/" + Tile.Y + ".");
				}
			}
		}
    }
}
