namespace System.Windows.Forms
{
    public class MapControl : System.Windows.Forms.Control
    {
        private const System.Int32 _TileSize = 256;

        private System.Windows.Forms.MapProvider _MapProvider;
        private readonly System.Collections.Generic.Dictionary<System.Drawing.Point, System.Windows.Forms.MapTile> _Tiles;
        private System.Int32 _TranslateX;
        private System.Int32 _TranslateY;
        private System.Int32 _Zoom;

        public System.Windows.Forms.MapProvider MapProvider
        {
            get
            {
                return _MapProvider;
            }
            set
            {
                _MapProvider = value;
                _Tiles.Clear();
                Refresh();
            }
        }

        public System.Int32 TranslateX
        {
            get
            {
                return _TranslateX;
            }
            set
            {
                _TranslateX = value;
                Refresh();
            }
        }

        public System.Int32 TranslateY
        {
            get
            {
                return _TranslateY;
            }
            set
            {
                _TranslateY = value;
                Refresh();
            }
        }

        public System.Int32 Zoom
        {
            get
            {
                return _Zoom;
            }
        }

        public MapControl()
        {
            _MapProvider = null;
            _Tiles = new System.Collections.Generic.Dictionary<System.Drawing.Point, System.Windows.Forms.MapTile>();
            _Zoom = 0;
        }

        public System.Drawing.PointF GetTilePositionFromScreenCoordinates(System.Int32 ScreenX, System.Int32 ScreenY)
        {
            var Result = new System.Drawing.PointF();

            Result.X = System.Convert.ToSingle(ScreenX - _TranslateX) / System.Convert.ToSingle(_TileSize);
            Result.Y = System.Convert.ToSingle(ScreenY - _TranslateY) / System.Convert.ToSingle(_TileSize);

            return Result;
        }

        public System.Drawing.PointF GetTilePositionFromWorldFraction(System.Single WorldX, System.Single WorldY)
        {
            var Result = new System.Drawing.PointF();

            Result.X = (0.5f + WorldX) * System.Convert.ToSingle(System.Math.Pow(2.0, _Zoom));
            Result.Y = (0.5f - WorldY) * System.Convert.ToSingle(System.Math.Pow(2.0, _Zoom));

            return Result;
        }

        public System.Drawing.Point GetPixelPositionFromWorldFraction(System.Single WorldX, System.Single WorldY)
        {
            var TilePosition = GetTilePositionFromWorldFraction(WorldX, WorldY);

            return new System.Drawing.Point(System.Convert.ToInt32(TilePosition.X * _TileSize), System.Convert.ToInt32(TilePosition.Y * _TileSize));
        }

        public System.Drawing.PointF GetWorldFractionFromScreenCoordinates(System.Int32 ScreenX, System.Int32 ScreenY)
        {
            var Result = GetTilePositionFromScreenCoordinates(ScreenX, ScreenY);

            Result.X = System.Convert.ToSingle(Result.X / System.Math.Pow(2.0, _Zoom) - 0.5);
            Result.Y = System.Convert.ToSingle(0.5 - Result.Y / System.Math.Pow(2.0, _Zoom));

            return Result;
        }

        public void SetZoom(System.Int32 Zoom)
        {
            SetZoom(Zoom, Width / 2, Height / 2);
        }

        public void SetZoom(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
        {
            if((Zoom >= 0) && (_Zoom != Zoom))
            {
                var ZoomCenter = GetWorldFractionFromScreenCoordinates(X, Y);

                _Zoom = Zoom;

                var NewCenter = GetPixelPositionFromWorldFraction(ZoomCenter.X, ZoomCenter.Y);

                _TranslateX = X - NewCenter.X;
                _TranslateY = Y - NewCenter.Y;
                _Tiles.Clear();
                Refresh();
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs EventArguments)
        {
            base.OnPaint(EventArguments);
            if(_MapProvider != null)
            {
                for(var X = -_TranslateX / _TileSize; X * _TileSize < Width - _TranslateX; ++X)
                {
                    for(var Y = -_TranslateY / _TileSize; Y * _TileSize < Width - _TranslateY; ++Y)
                    {
                        var Position = new System.Drawing.Point(X, Y);
                        System.Windows.Forms.MapTile Tile;

                        if(_Tiles.ContainsKey(Position) == false)
                        {
                            Tile = _MapProvider.GetTile(_Zoom, X, Y);
                            _Tiles[Position] = Tile;
                        }
                        else
                        {
                            Tile = _Tiles[Position];
                        }
                        switch(Tile.Status)
                        {
                        case System.Windows.Forms.MapTile.ImageStatus.Available:
                            {
                                EventArguments.Graphics.DrawImageUnscaled(_Tiles[Position].Image, X * 256 + _TranslateX, Y * 256 + _TranslateY);

                                break;
                            }
                        case System.Windows.Forms.MapTile.ImageStatus.Fetching:
                            {
                                Tile.StatusChanged += () => Invalidate(new System.Drawing.Rectangle(Tile.X * 256 + _TranslateX, Tile.Y * 256 + _TranslateY, 256, 256));

                                break;
                            }
                        }
                    }
                }
            }

            //var Random = new System.Random();

            //EventArguments.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(Random.Next(256), Random.Next(256), Random.Next(256))), EventArguments.ClipRectangle);
        }
    }
}
