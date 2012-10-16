namespace System.Windows.Forms
{
    public class MapnikDownloader : System.Windows.Forms.MapProvider
    {
        private const System.String _TileFormat = "http://tile.openstreetmap.org/{0}/{1}/{2}.png";

        protected override void _FetchTile(System.Windows.Forms.MapTile Tile)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(_DownloadTile, Tile);
        }

        public override System.Int32 GetTileSize()
        {
            return 256;
        }

        private static void _DownloadTile(System.Object Parameter)
        {
            var Tile = (System.Windows.Forms.MapTile)Parameter;

            try
            {
                var Request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(System.String.Format(_TileFormat, Tile.Zoom, Tile.X, Tile.Y));

                Request.UserAgent = "MapControl";

                using(var Response = Request.GetResponse())
                {
                    var Stream = Response.GetResponseStream();

                    if(Stream != null)
                    {
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
