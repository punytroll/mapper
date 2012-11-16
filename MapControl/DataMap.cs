namespace System.Windows.Forms
{
    public class DataMap : System.Windows.Forms.Map
    {
        public class Point
        {
            private System.Drawing.Color _Color;
            private System.Point _GeoLocation;
            private System.Object _Object;

            public System.Drawing.Color Color
            {
                get
                {
                    return _Color;
                }
                set
                {
                    _Color = value;
                }
            }

            public System.Point GeoLocation
            {
                get
                {
                    return _GeoLocation;
                }
                set
                {
                    _GeoLocation = value;
                }
            }

            public System.Object Object
            {
                get
                {
                    return _Object;
                }
                set
                {
                    _Object = value;
                }
            }
        }

        private readonly System.Collections.Generic.List<Point> _Points;

        public System.Collections.Generic.List<Point> Points
        {
            get
            {
                return _Points;
            }
        }

        public DataMap()
        {
            _Points = new System.Collections.Generic.List<Point>();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs EventArguments)
        {
            base.OnPaint(EventArguments);

            foreach(var Point in _Points)
            {
                var ScreenLocation = GetScreenLocationFromGeoLocation(Point.GeoLocation);

                if((ScreenLocation.X >= 0) && (ScreenLocation.Y >= 0) && (ScreenLocation.X <= Width) && (ScreenLocation.Y <= Height))
                {
                    EventArguments.Graphics.FillRectangle(new System.Drawing.SolidBrush(Point.Color), ScreenLocation.X - 2, ScreenLocation.Y - 2, 5, 5);
                }
            }
        }
    }
}
