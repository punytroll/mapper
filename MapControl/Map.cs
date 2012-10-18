namespace System.Windows.Forms
{
    public class Map : System.Windows.Forms.Control
    {
        private System.Windows.Forms.MapProvider _MapProvider;
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

        public Map()
        {
            _MapProvider = null;
            _Zoom = 0;
            SetStyle(System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
        }

        /// <summary>
        /// Calculates the location in geo coordinates from a location in screen coordinates.
        /// </summary>
        public System.Point GetGeoLocationFromScreenLocation(System.Drawing.Point ScreenLocation)
        {
            return GetGeoLocationFromWorldLocation(GetWorldLocationFromScreenLocation(ScreenLocation));
        }

        /// <summary>
        /// Calculates the location in geo coordinates from a location in world coordinates.
        /// </summary>
        public System.Point GetGeoLocationFromWorldLocation(System.Point WorldLocation)
        {
            return GetGeoLocationFromWorldLocation(WorldLocation.X, WorldLocation.Y);
        }

        /// <summary>
        /// Calculates the location in geo coordinates from a location in world coordinates.
        /// </summary>
        public System.Point GetGeoLocationFromWorldLocation(System.Double WorldLocationX, System.Double WorldLocationY)
        {
            var Result = new System.Point();

            Result.X = WorldLocationX * System.Math.PI * 2.0;
            Result.Y = System.Math.Atan(System.Math.Sinh(2.0 * System.Math.PI * WorldLocationY));

            return Result;
        }

        /// <summary>
        /// Calculates the location in pixel coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.Point GetPixelLocationFromGeoLocation(System.Point GeoLocation)
        {
            return GetPixelLocationFromWorldLocation(GetWorldLocationFromGeoLocation(GeoLocation));
        }

        /// <summary>
        /// Calculates the location in pixel coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.Point GetPixelLocationFromGeoLocation(System.Double GeoLocationX, System.Double GeoLocationY)
        {
            return GetPixelLocationFromWorldLocation(GetWorldLocationFromGeoLocation(GeoLocationX, GeoLocationY));
        }

        /// <summary>
        /// Calculates the location in pixel coordinates from a location in screen coordinates.
        /// </summary>
        public System.Drawing.Point GetPixelLocationFromScreenLocation(System.Drawing.Point ScreenLocation)
        {
            return GetPixelLocationFromScreenLocation(ScreenLocation.X, ScreenLocation.Y);
        }

        /// <summary>
        /// Calculates the location in pixel coordinates from a location in screen coordinates.
        /// </summary>
        public System.Drawing.Point GetPixelLocationFromScreenLocation(System.Int32 ScreenLocationX, System.Int32 ScreenLocationY)
        {
            var Result = new System.Drawing.Point();

            Result.X = ScreenLocationX - _TranslateX;
            Result.Y = ScreenLocationY - _TranslateY;

            return Result;
        }

        /// <summary>
        /// Calculates the location in pixel coordinates from a location in tile coordinates.
        /// </summary>
        public System.Drawing.Point GetPixelLocationFromTileLocation(System.Point TileLocation)
        {
            var Result = new System.Drawing.Point();

            if(_MapProvider != null)
            {
                Result.X = System.Convert.ToInt32(TileLocation.X * _MapProvider.GetTileSize());
                Result.Y = System.Convert.ToInt32(TileLocation.Y * _MapProvider.GetTileSize());
            }

            return Result;
        }

        /// <summary>
        /// Calculates the location in pixel coordinates from a location in world coordinates.
        /// </summary>
        public System.Drawing.Point GetPixelLocationFromWorldLocation(System.Point WorldLocation)
        {
            return GetPixelLocationFromTileLocation(GetTileLocationFromWorldLocation(WorldLocation));
        }

        /// <summary>
        /// Calculates the location in pixel coordinates from a location in world coordinates.
        /// </summary>
        public System.Drawing.Point GetPixelLocationFromWorldLocation(System.Double WorldLocationX, System.Double WorldLocationY)
        {
            return GetPixelLocationFromTileLocation(GetTileLocationFromWorldLocation(WorldLocationX, WorldLocationY));
        }

        /// <summary>
        /// Calculates the location in screen coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.Point GetScreenLocationFromGeoLocation(System.Point GeoLocation)
        {
            return GetScreenLocationFromPixelLocation(GetPixelLocationFromGeoLocation(GeoLocation));
        }

        /// <summary>
        /// Calculates the location in screen coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.Point GetScreenLocationFromGeoLocation(System.Double GeoLocationX, System.Double GeoLocationY)
        {
            return GetScreenLocationFromPixelLocation(GetPixelLocationFromGeoLocation(GeoLocationX, GeoLocationY));
        }

        /// <summary>
        /// Calculates the location in screen coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.Point GetScreenLocationFromPixelLocation(System.Drawing.Point PixelLocation)
        {
            var Result = new System.Drawing.Point();

            Result.X = PixelLocation.X + _TranslateX;
            Result.Y = PixelLocation.Y + _TranslateY;

            return Result;
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in pixel coordinates.
        /// </summary>
        public System.Point GetTileLocationFromPixelLocation(System.Drawing.Point PixelLocation)
        {
            return GetTileLocationFromPixelLocation(PixelLocation.X, PixelLocation.Y);
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in pixel coordinates.
        /// </summary>
        public System.Point GetTileLocationFromPixelLocation(System.Int32 PixelLocationX, System.Int32 PixelLocationY)
        {
            var Result = new System.Point();

            if(_MapProvider != null)
            {
                Result.X = PixelLocationX / System.Convert.ToDouble(_MapProvider.GetTileSize());
                Result.Y = PixelLocationY / System.Convert.ToDouble(_MapProvider.GetTileSize());
            }

            return Result;
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in screen coordinates.
        /// </summary>
        public System.Point GetTileLocationFromScreenLocation(System.Drawing.Point ScreenLocation)
        {
            return GetTileLocationFromPixelLocation(GetPixelLocationFromScreenLocation(ScreenLocation));
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in screen coordinates.
        /// </summary>
        public System.Point GetTileLocationFromScreenLocation(System.Int32 ScreenLocationX, System.Int32 ScreenLocationY)
        {
            return GetTileLocationFromPixelLocation(GetPixelLocationFromScreenLocation(ScreenLocationX, ScreenLocationY));
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in world coordinates.
        /// </summary>
        public System.Point GetTileLocationFromWorldLocation(System.Point WorldLocation)
        {
            return GetTileLocationFromWorldLocation(WorldLocation.X, WorldLocation.Y);
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in world coordinates.
        /// </summary>
        public System.Point GetTileLocationFromWorldLocation(System.Double WorldLocationX, System.Double WorldLocationY)
        {
            var Result = new System.Point();

            Result.X = (0.5 + WorldLocationX) * System.Math.Pow(2.0, _Zoom);
            Result.Y = (0.5 - WorldLocationY) * System.Math.Pow(2.0, _Zoom);

            return Result;
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in geo coordinates.
        /// </summary>
        public System.Point GetWorldLocationFromGeoLocation(System.Point GeoLocation)
        {
            return GetWorldLocationFromGeoLocation(GeoLocation.X, GeoLocation.Y);
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in geo coordinates.
        /// </summary>
        public System.Point GetWorldLocationFromGeoLocation(System.Double GeoLocationX, System.Double GeoLocationY)
        {
            var Result = new System.Point();

            Result.X = GeoLocationX / 2.0 / System.Math.PI;
            Result.Y = (Math.Log(Math.Tan(GeoLocationY) + 1.0 / Math.Cos(GeoLocationY)) / Math.PI) / 2.0;

            return Result;
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in screen coordinates.
        /// </summary>
        public System.Point GetWorldLocationFromScreenLocation(System.Int32 ScreenLocationX, System.Int32 ScreenLocationY)
        {
            return GetWorldLocationFromTileLocation(GetTileLocationFromScreenLocation(ScreenLocationX, ScreenLocationY));
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in screen coordinates.
        /// </summary>
        public System.Point GetWorldLocationFromScreenLocation(System.Drawing.Point ScreenLocation)
        {
            return GetWorldLocationFromTileLocation(GetTileLocationFromScreenLocation(ScreenLocation));
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in tile coordinates.
        /// </summary>
        public System.Point GetWorldLocationFromTileLocation(System.Point TileLocation)
        {
            return GetWorldLocationFromTileLocation(TileLocation.X, TileLocation.Y);
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in tile coordinates.
        /// </summary>
        public System.Point GetWorldLocationFromTileLocation(System.Double TileLocationX, System.Double TileLocationY)
        {
            var Result = new System.Point();

            Result.X = TileLocationX / System.Math.Pow(2.0, _Zoom) - 0.5;
            Result.Y = 0.5 - TileLocationY / System.Math.Pow(2.0, _Zoom);

            return Result;
        }

        public System.Boolean IsScreenLocationInMap(System.Drawing.Point ScreenLocation)
        {
            var PixelLocation = GetPixelLocationFromScreenLocation(ScreenLocation);

            return (PixelLocation.X >= 0) && (PixelLocation.Y >= 0) && (PixelLocation.X < (1 << _Zoom)) && (PixelLocation.Y < (1 << _Zoom));
        }

        public void SetZoom(System.Int32 Zoom)
        {
            SetZoom(Zoom, Width / 2, Height / 2);
        }

        public void SetZoom(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
        {
            if((Zoom >= 0) && (_Zoom != Zoom) && (Zoom < 19))
            {
                var ZoomCenter = GetWorldLocationFromScreenLocation(X, Y);

                _Zoom = Zoom;

                var NewCenter = GetPixelLocationFromWorldLocation(ZoomCenter.X, ZoomCenter.Y);

                _TranslateX = X - NewCenter.X;
                _TranslateY = Y - NewCenter.Y;
                Refresh();
            }
        }

        public static System.Point GetGeoLocationFromGeoCoordinates(System.Double Latitude, System.Double Longitude)
        {
            return new System.Point(Longitude / 180.0 * System.Math.PI, Latitude / 180.0 * System.Math.PI);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs EventArguments)
        {
            base.OnPaint(EventArguments);

            var LoadingFont = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 20, System.Drawing.FontStyle.Regular);

            if(_MapProvider != null)
            {
                for(var X = System.Math.Max(0, -_TranslateX / _MapProvider.GetTileSize()); X * _MapProvider.GetTileSize() < Width - _TranslateX; ++X)
                {
                    for(var Y = -_TranslateY / _MapProvider.GetTileSize(); Y * _MapProvider.GetTileSize() < Width - _TranslateY; ++Y)
                    {
                        var Tile = _MapProvider.GetTile(_Zoom, X, Y);

                        if(Tile != null)
                        {
                            if(Tile.Image != null)
                            {
                                EventArguments.Graphics.DrawImageUnscaled(Tile.Image, X * _MapProvider.GetTileSize() + _TranslateX, Y * _MapProvider.GetTileSize() + _TranslateY);
                            }
                            else
                            {
                                EventArguments.Graphics.FillRectangle(System.Drawing.Brushes.DimGray, X * _MapProvider.GetTileSize() + _TranslateX, Y * _MapProvider.GetTileSize() + _TranslateY, _MapProvider.GetTileSize() - 1, _MapProvider.GetTileSize() - 1);
                                EventArguments.Graphics.DrawString("Loading ...", LoadingFont, System.Drawing.Brushes.Black, X * _MapProvider.GetTileSize() + _TranslateX, Y * _MapProvider.GetTileSize() + _TranslateY);
                                Tile.ImageChanged += () => Invoke(new MethodInvoker(Invalidate));
                            }
                        }
                    }
                }
            }
        }
    }
}
