namespace System.Windows.Forms
{
    public abstract class MapProvider
    {
        private readonly System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.Dictionary<System.Drawing.Point, System.Windows.Forms.MapTile>> _Cache;
        private readonly System.ImageHarddriveCache _HarddriveCache;

        protected MapProvider()
        {
            _Cache = new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.Dictionary<System.Drawing.Point, System.Windows.Forms.MapTile>>();
            _HarddriveCache = new ImageHarddriveCache();
            _HarddriveCache.RootDirectory = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Cache");
        }

        public System.Boolean HasTile(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
        {
            return (_Cache.ContainsKey(Zoom) == true) && (_Cache[Zoom].ContainsKey(new System.Drawing.Point(X, Y)) == true);
        }

        public System.Boolean HasImage(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
        {
            return (_Cache.ContainsKey(Zoom) == true) && (_Cache[Zoom].ContainsKey(new System.Drawing.Point(X, Y)) == true) && (_Cache[Zoom][new System.Drawing.Point(X, Y)].Image != null);
        }

        public System.Windows.Forms.MapTile GetTile(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
        {
            System.Collections.Generic.Dictionary<System.Drawing.Point, System.Windows.Forms.MapTile> CacheForZoom;

            if(_Cache.ContainsKey(Zoom) == false)
            {
                CacheForZoom = new System.Collections.Generic.Dictionary<System.Drawing.Point, System.Windows.Forms.MapTile>();
                _Cache.Add(Zoom, CacheForZoom);
            }
            else
            {
                CacheForZoom = _Cache[Zoom];
            }

            var TileIndex = new System.Drawing.Point(X, Y);

            if(CacheForZoom.ContainsKey(TileIndex) == false)
            {
                var Tile = new System.Windows.Forms.MapTile(GetSetIdentifier(), Zoom, X, Y);

                if(_SupportsTile(Tile) == true)
                {
                    CacheForZoom.Add(TileIndex, Tile);

                    var Image = _HarddriveCache.LoadTileImage(GetSetIdentifier(), Zoom, X, Y);

                    if(Image == null)
                    {
                        Tile.ImageChanged += () => _StoreImageOnHarddrive(Tile);
                        // here, a preliminary tile image could be calculated from other zoom levels
                        _FetchTile(Tile);
                    }
                    else
                    {
                        Tile.SetImage(Image);
                    }

                    return Tile;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return CacheForZoom[TileIndex];
            }
        }

        private void _StoreImageOnHarddrive(System.Windows.Forms.MapTile Tile)
        {
            lock(Tile.Image)
            {
                if(Tile.ExpireDateTime.HasValue == true)
                {
                    _HarddriveCache.StoreTileImage(Tile.SetIdentifier, Tile.Zoom, Tile.X, Tile.Y, Tile.Image, Tile.ExpireDateTime.Value);
                }
                else
                {
                    _HarddriveCache.StoreTileImage(Tile.SetIdentifier, Tile.Zoom, Tile.X, Tile.Y, Tile.Image, System.DateTime.Now.AddDays(7));
                }
            }
        }

        protected abstract System.Boolean _SupportsTile(System.Windows.Forms.MapTile Tile);
        protected abstract void _FetchTile(System.Windows.Forms.MapTile Tile);
        public abstract System.String GetSetIdentifier();
        public abstract System.Int32 GetTileSize();
    }
}
