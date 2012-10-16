namespace System.Windows.Forms
{
    public abstract class MapProvider
    {
        private readonly System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.Dictionary<System.Drawing.Point, System.Windows.Forms.MapTile>> _Cache;

        protected MapProvider()
        {
            _Cache = new System.Collections.Generic.Dictionary<System.Int32, System.Collections.Generic.Dictionary<System.Drawing.Point, System.Windows.Forms.MapTile>>();
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

                CacheForZoom.Add(TileIndex, Tile);
                // here, a preliminary tile image could be calculated from other zoom levels
                _FetchTile(Tile);

                return Tile;
            }
            else
            {
                return CacheForZoom[TileIndex];
            }
        }

        protected abstract void _FetchTile(System.Windows.Forms.MapTile Tile);
    }
}
