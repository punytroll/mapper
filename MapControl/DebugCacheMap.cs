namespace System.Windows.Forms
{
    public class DebaugCacheMap : System.Windows.Forms.Map
    {
        private System.Int32 _DeltaZoom;

        public System.Int32 DeltaZoom
        {
            get
            {
                return _DeltaZoom;
            }
            set
            {
                _DeltaZoom = value;
                Refresh();
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs EventArguments)
        {
            base.OnPaint(EventArguments);

            var TileSize = System.Convert.ToInt32(MapProvider.GetTileSize() / System.Math.Pow(2.0, _DeltaZoom));

            for(var X = -TranslateX / TileSize; X * TileSize < Width - TranslateX; ++X)
            {
                for(var Y = -TranslateY / TileSize; Y * TileSize < Width - TranslateY; ++Y)
                {
                    if(MapProvider.HasTile(Zoom + DeltaZoom, X, Y) == true)
                    {
                        if(MapProvider.HasImage(Zoom + DeltaZoom, X, Y) == true)
                        {
                            EventArguments.Graphics.DrawRectangle(System.Drawing.Pens.Green, X * TileSize + TranslateX, Y * TileSize + TranslateY, TileSize - 1, TileSize - 1);
                        }
                        else
                        {
                            EventArguments.Graphics.DrawRectangle(System.Drawing.Pens.Yellow, X * TileSize + TranslateX, Y * TileSize + TranslateY, TileSize - 1, TileSize - 1);
                            MapProvider.GetTile(Zoom + DeltaZoom, X, Y).ImageChanged += () => Invoke(new MethodInvoker(Invalidate));
                        }
                    }
                    else
                    {
                        EventArguments.Graphics.DrawRectangle(System.Drawing.Pens.Red, X * TileSize + TranslateX, Y * TileSize + TranslateY, TileSize - 1, TileSize - 1);
                    }
                }
            }
        }
    }
}
