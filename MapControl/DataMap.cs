namespace System.Windows.Forms
{
    public class DataMap : System.Windows.Forms.Map
    {
        private readonly System.Collections.Generic.List<System.Point> _Points;

        public System.Collections.Generic.List<System.Point> Points
        {
            get
            {
                return _Points;
            }
        }

        public DataMap()
        {
            _Points = new System.Collections.Generic.List<System.Point>();
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs EventArguments)
        {
            base.OnPaint(EventArguments);

            foreach(var GeoLocation in _Points)
            {
                var ScreenLocation = GetScreenLocationFromGeoLocation(GeoLocation);

                if((ScreenLocation.X >= 0) && (ScreenLocation.Y >= 0) && (ScreenLocation.X <= Width) && (ScreenLocation.Y <= Height))
                {
                    EventArguments.Graphics.FillRectangle(System.Drawing.Brushes.OrangeRed, ScreenLocation.X - 2, ScreenLocation.Y - 2, 5, 5);
                }
            }
        }
    }
}
