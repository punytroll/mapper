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

        public bool SupportsTile(MapTile Tile)
        {
            return (Tile.Zoom >= 0) && (Tile.Zoom <= 18) && (Tile.X >= 0) && (Tile.X < (1 << Tile.Zoom)) && (Tile.Y >= 0) && (Tile.Y < (1 << Tile.Zoom));
        }

        public void FetchTile(MapTile Tile)
        {
            System.Console.WriteLine("Queuing to download " + Tile.Zoom + ":" + Tile.X + "/" + Tile.Y + ".");
            OnTileDownloading(Tile);
            ThreadPool.QueueUserWorkItem(_DownloadTile, Tile);
        }

        public String GetSetIdentifier()
        {
            return "mapnik";
        }

        public Int32 GetTileSize()
        {
            return _TileSize;
        }

        private void _DownloadTile(Object Parameter)
        {
            var Tile = Parameter as MapTile;

            Debug.Assert(Tile != null);
            System.Console.WriteLine("Downloading " + Tile.Zoom + ":" + Tile.X + "/" + Tile.Y + ".");
            try
            {
                var Request = WebRequest.Create(String.Format(_TileFormat, Tile.Zoom, Tile.X, Tile.Y)) as HttpWebRequest;

                Debug.Assert(Request != null);
                Request.UserAgent = "GPX Track Viewer  (hagen.moebius@googlemail.com)";

                using(var Response = Request.GetResponse())
                {
                    var Stream = Response.GetResponseStream();

                    if(Stream != null)
                    {
                        Tile.SetSetIdentifier(GetSetIdentifier());
                        Tile.SetExpireDateTime(DateTime.Parse(Response.Headers["Expires"]));
                        Tile.SetImage(new Bitmap(Stream));
                        Stream.Close();
                    }
                }
            }
            catch(WebException)
            {
                Tile.SetImage(null);
            }
            OnTileDownloaded(Tile);
            System.Console.WriteLine("Downloaded " + Tile.Zoom + ":" + Tile.X + "/" + Tile.Y + ".");
        }
    }
}
