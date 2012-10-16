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
        public System.Drawing.PointF GetGeoLocationFromScreenLocation(System.Drawing.Point ScreenLocation)
        {
            return GetGeoLocationFromWorldLocation(GetWorldLocationFromScreenLocation(ScreenLocation));
        }

        /// <summary>
        /// Calculates the location in geo coordinates from a location in world coordinates.
        /// </summary>
        public System.Drawing.PointF GetGeoLocationFromWorldLocation(System.Drawing.PointF WorldLocation)
        {
            return GetGeoLocationFromWorldLocation(WorldLocation.X, WorldLocation.Y);
        }

        /// <summary>
        /// Calculates the location in geo coordinates from a location in world coordinates.
        /// </summary>
        public System.Drawing.PointF GetGeoLocationFromWorldLocation(System.Single WorldLocationX, System.Single WorldLocationY)
        {
            var Result = new System.Drawing.PointF();

            Result.X = System.Convert.ToSingle(WorldLocationX * System.Math.PI * 2.0);
            Result.Y = System.Convert.ToSingle(System.Math.Atan(System.Math.Sinh(2.0 * System.Math.PI * WorldLocationY)));

            return Result;
        }

        /// <summary>
        /// Calculates the location in pixel coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.Point GetPixelLocationFromGeoLocation(System.Drawing.PointF GeoLocation)
        {
            return GetPixelLocationFromWorldLocation(GetWorldLocationFromGeoLocation(GeoLocation));
        }

        /// <summary>
        /// Calculates the location in pixel coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.Point GetPixelLocationFromGeoLocation(System.Single GeoLocationX, System.Single GeoLocationY)
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
        public System.Drawing.Point GetPixelLocationFromTileLocation(System.Drawing.PointF TileLocation)
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
        public System.Drawing.Point GetPixelLocationFromWorldLocation(System.Drawing.PointF WorldLocation)
        {
            return GetPixelLocationFromTileLocation(GetTileLocationFromWorldLocation(WorldLocation));
        }

        /// <summary>
        /// Calculates the location in pixel coordinates from a location in world coordinates.
        /// </summary>
        public System.Drawing.Point GetPixelLocationFromWorldLocation(System.Single WorldLocationX, System.Single WorldLocationY)
        {
            return GetPixelLocationFromTileLocation(GetTileLocationFromWorldLocation(WorldLocationX, WorldLocationY));
        }

        /// <summary>
        /// Calculates the location in screen coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.Point GetScreenLocationFromGeoLocation(System.Drawing.PointF GeoLocation)
        {
            return GetScreenLocationFromPixelLocation(GetPixelLocationFromGeoLocation(GeoLocation));
        }

        /// <summary>
        /// Calculates the location in screen coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.Point GetScreenLocationFromGeoLocation(System.Single GeoLocationX, System.Single GeoLocationY)
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
        public System.Drawing.PointF GetTileLocationFromPixelLocation(System.Drawing.Point PixelLocation)
        {
            return GetTileLocationFromPixelLocation(PixelLocation.X, PixelLocation.Y);
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in pixel coordinates.
        /// </summary>
        public System.Drawing.PointF GetTileLocationFromPixelLocation(System.Int32 PixelLocationX, System.Int32 PixelLocationY)
        {
            var Result = new System.Drawing.PointF();

            if(_MapProvider != null)
            {
                Result.X = System.Convert.ToSingle(PixelLocationX) / System.Convert.ToSingle(_MapProvider.GetTileSize());
                Result.Y = System.Convert.ToSingle(PixelLocationY) / System.Convert.ToSingle(_MapProvider.GetTileSize());
            }

            return Result;
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in screen coordinates.
        /// </summary>
        public System.Drawing.PointF GetTileLocationFromScreenLocation(System.Drawing.Point ScreenLocation)
        {
            return GetTileLocationFromPixelLocation(GetPixelLocationFromScreenLocation(ScreenLocation));
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in screen coordinates.
        /// </summary>
        public System.Drawing.PointF GetTileLocationFromScreenLocation(System.Int32 ScreenLocationX, System.Int32 ScreenLocationY)
        {
            return GetTileLocationFromPixelLocation(GetPixelLocationFromScreenLocation(ScreenLocationX, ScreenLocationY));
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in world coordinates.
        /// </summary>
        public System.Drawing.PointF GetTileLocationFromWorldLocation(System.Drawing.PointF WorldLocation)
        {
            return GetTileLocationFromWorldLocation(WorldLocation.X, WorldLocation.Y);
        }

        /// <summary>
        /// Calculates the location in tile coordinates from a location in world coordinates.
        /// </summary>
        public System.Drawing.PointF GetTileLocationFromWorldLocation(System.Single WorldLocationX, System.Single WorldLocationY)
        {
            var Result = new System.Drawing.PointF();

            Result.X = (0.5f + WorldLocationX) * System.Convert.ToSingle(System.Math.Pow(2.0, _Zoom));
            Result.Y = (0.5f - WorldLocationY) * System.Convert.ToSingle(System.Math.Pow(2.0, _Zoom));

            return Result;
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.PointF GetWorldLocationFromGeoLocation(System.Drawing.PointF GeoLocation)
        {
            return GetWorldLocationFromGeoLocation(GeoLocation.X, GeoLocation.Y);
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in geo coordinates.
        /// </summary>
        public System.Drawing.PointF GetWorldLocationFromGeoLocation(System.Single GeoLocationX, System.Single GeoLocationY)
        {
            var Result = new System.Drawing.PointF();

            Result.X = System.Convert.ToSingle(GeoLocationX / 2.0 / System.Math.PI);
            Result.Y = System.Convert.ToSingle((Math.Log(Math.Tan(GeoLocationY) + 1.0 / Math.Cos(GeoLocationY)) / Math.PI) / 2.0);

            return Result;
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in screen coordinates.
        /// </summary>
        public System.Drawing.PointF GetWorldLocationFromScreenLocation(System.Int32 ScreenLocationX, System.Int32 ScreenLocationY)
        {
            return GetWorldLocationFromTileLocation(GetTileLocationFromScreenLocation(ScreenLocationX, ScreenLocationY));
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in screen coordinates.
        /// </summary>
        public System.Drawing.PointF GetWorldLocationFromScreenLocation(System.Drawing.Point ScreenLocation)
        {
            return GetWorldLocationFromTileLocation(GetTileLocationFromScreenLocation(ScreenLocation));
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in tile coordinates.
        /// </summary>
        public System.Drawing.PointF GetWorldLocationFromTileLocation(System.Drawing.PointF TileLocation)
        {
            return GetWorldLocationFromTileLocation(TileLocation.X, TileLocation.Y);
        }

        /// <summary>
        /// Calculates the location in world coordinates from a location in tile coordinates.
        /// </summary>
        public System.Drawing.PointF GetWorldLocationFromTileLocation(System.Single TileLocationX, System.Single TileLocationY)
        {
            var Result = new System.Drawing.PointF();

            Result.X = System.Convert.ToSingle(TileLocationX / System.Math.Pow(2.0, _Zoom) - 0.5);
            Result.Y = System.Convert.ToSingle(0.5 - TileLocationY / System.Math.Pow(2.0, _Zoom));

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
                var ZoomCenter = GetWorldLocationFromScreenLocation(X, Y);

                _Zoom = Zoom;

                var NewCenter = GetPixelLocationFromWorldLocation(ZoomCenter.X, ZoomCenter.Y);

                _TranslateX = X - NewCenter.X;
                _TranslateY = Y - NewCenter.Y;
                Refresh();
            }
        }

        public System.Drawing.PointF GetGeoLocationFromGeoCoordinates(System.Single Latitude, System.Single Longitude)
        {
            return new System.Drawing.PointF(System.Convert.ToSingle(Longitude / 180.0 * System.Math.PI), System.Convert.ToSingle(Latitude / 180.0 * System.Math.PI));
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs EventArguments)
        {
            base.OnPaint(EventArguments);
            if(_MapProvider != null)
            {
                for(var X = -_TranslateX / _MapProvider.GetTileSize(); X * _MapProvider.GetTileSize() < Width - _TranslateX; ++X)
                {
                    for(var Y = -_TranslateY / _MapProvider.GetTileSize(); Y * _MapProvider.GetTileSize() < Width - _TranslateY; ++Y)
                    {
                        var Tile = _MapProvider.GetTile(_Zoom, X, Y);
                        
                        if(Tile.Image != null)
                        {
                            EventArguments.Graphics.DrawImageUnscaled(Tile.Image, X * _MapProvider.GetTileSize() + _TranslateX, Y * _MapProvider.GetTileSize() + _TranslateY);
                        }
                        else
                        {
                            Tile.ImageChanged += () => Invoke(new MethodInvoker(Invalidate));
                        }
                    }
                }
            }
        }
    }
}
