namespace System.Windows.Forms
{
    public class MapProvider
    {
        private readonly System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.Dictionary<System.Drawing.Point, System.Windows.Forms.MapTile>> _Cache;
        private System.ImageHarddriveCache _HarddriveCache;
        private System.ITileDownloader _TileDownloader;

        public System.ImageHarddriveCache HarddriveCache
        {
            get
            {
                return _HarddriveCache;
            }
            set
            {
                _HarddriveCache = value;
            }
        }

        public System.ITileDownloader TileDownloader
        {
            get
            {
                return _TileDownloader;
            }
            set
            {
                _TileDownloader = value;
            }
        }

        public MapProvider()
        {
            _Cache = new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.Dictionary<System.Drawing.Point, System.Windows.Forms.MapTile>>();
            _HarddriveCache = null;
            _TileDownloader = null;
        }

        public System.Int32 GetTileSize()
        {
            return _TileDownloader.GetTileSize();
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
                var Tile = new System.Windows.Forms.MapTile(Zoom, X, Y);

                if(_TileDownloader.SupportsTile(Tile) == true)
                {
                    CacheForZoom.Add(TileIndex, Tile);

                    System.Drawing.Image Image = null;

                    if(_HarddriveCache != null)
                    {
                        Image = _HarddriveCache.LoadTileImage(Zoom, X, Y);
                    }
                    if(Image == null)
                    {
                        Tile.ImageChanged += delegate
                        {
                            if(Tile.Image != null)
                            {
                                _StoreImageOnHarddrive(Tile);
                            }
                        };
                        // here, a preliminary tile image could be calculated from other zoom levels
                        _TileDownloader.FetchTile(Tile);
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

        private void _StoreImageOnHarddrive(MapTile Tile)
        {
            if(_HarddriveCache != null)
            {
                lock(Tile.Image)
                {
                    if(Tile.ExpireDateTime.HasValue == true)
                    {
                        _HarddriveCache.StoreTileImage(Tile.Zoom, Tile.X, Tile.Y, Tile.Image, Tile.ExpireDateTime.Value);
                    }
                    else
                    {
                        _HarddriveCache.StoreTileImage(Tile.Zoom, Tile.X, Tile.Y, Tile.Image, System.DateTime.Now.AddDays(7));
                    }
                }
            }
        }
    }
}
