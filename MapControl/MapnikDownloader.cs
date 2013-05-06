namespace System.Windows.Forms
{
    public class MapnikDownloader : System.Windows.Forms.MapProvider
    {
        private const System.Int32 _TileSize = 256;
        private const System.String _TileFormat = "http://tile.openstreetmap.org/{0}/{1}/{2}.png";

        protected override void _FetchTile(System.Windows.Forms.MapTile Tile)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(_DownloadTile, Tile);
        }

        protected override bool _SupportsTile(System.Windows.Forms.MapTile Tile)
        {
            return (Tile.Zoom >= 0) && (Tile.Zoom <= 18) && (Tile.X >= 0) && (Tile.X < (1 << Tile.Zoom)) && (Tile.Y >= 0) && (Tile.Y < (1 << Tile.Zoom));
        }

        public override string GetSetIdentifier()
        {
            return "mapnik";
        }

        public override System.Int32 GetTileSize()
        {
            return _TileSize;
        }

        private static void _DownloadTile(System.Object Parameter)
        {
            var Tile = (System.Windows.Forms.MapTile)Parameter;

            try
            {
                var Request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(System.String.Format(_TileFormat, Tile.Zoom, Tile.X, Tile.Y));

                Request.UserAgent = "GPX Track Viewer  (hagen.moebius@googlemail.com)";

                using(var Response = Request.GetResponse())
                {
                    var Stream = Response.GetResponseStream();

                    if(Stream != null)
                    {
                        Tile.SetExpireDateTime(System.DateTime.Parse(Response.Headers["Expires"]));
                        Tile.SetImage(new System.Drawing.Bitmap(Stream));
                        Stream.Close();
                    }
                }
            }
            catch(System.Net.WebException)
            {
                Tile.SetImage(null);
            }
        }
    }
}
