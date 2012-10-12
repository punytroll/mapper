namespace System.Windows.Forms
{
    public class MapnikDownloader : System.Windows.Forms.MapProvider
    {
        private const System.String _TileFormat = "http://tile.openstreetmap.org/{0}/{1}/{2}.png";

        protected override System.Windows.Forms.MapTile _GetTile(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
        {
            var Result = new MapTile(Zoom, X, Y);

            System.Threading.ThreadPool.QueueUserWorkItem(_DownloadTile, Result);

            return Result;
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
            catch(System.Net.WebException Exception)
            {
                Tile.SetImage(null);
            }
        }
    }
}
