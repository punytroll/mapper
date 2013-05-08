namespace System
{
    public class ImageMemoryCache
    {
        private struct ImageIdentifier
        {
            System.Int32 X;
            System.Int32 Y;
            System.Int32 Zoom;

            public ImageIdentifier(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
            {
                this.X = X;
                this.Y = Y;
                this.Zoom = Zoom;
            }
        }

        private readonly System.Collections.Generic.Dictionary<ImageIdentifier, System.Drawing.Image> _Cache;

        public ImageMemoryCache()
        {
            _Cache = new System.Collections.Generic.Dictionary<ImageIdentifier, System.Drawing.Image>();
        }

        public System.Boolean HasImage(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
        {
            return _Cache.ContainsKey(new ImageIdentifier(Zoom, X, Y)) == true;
        }

        public System.Drawing.Image GetImage(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
        {
            var ImageIdentifier = new ImageIdentifier(Zoom, X, Y);

            if(_Cache.ContainsKey(ImageIdentifier) == true)
            {
                return _Cache[ImageIdentifier];
            }
            else
            {
                return null;
            }
        }

        public void SetImage(System.Int32 Zoom, System.Int32 X, System.Int32 Y, System.Drawing.Image Image)
        {
            var ImageIdentifier = new ImageIdentifier(Zoom, X, Y);

            _Cache[ImageIdentifier] = Image;
        }
    }
}
