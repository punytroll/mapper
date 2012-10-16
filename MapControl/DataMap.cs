namespace System.Windows.Forms
{
    public class DataMap : System.Windows.Forms.Map
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs EventArguments)
        {
            base.OnPaint(EventArguments);

            var GeoLocation = GetGeoLocationFromGeoCoordinates(52.518611f, 13.408056f);
            var ScreenLocation = GetScreenLocationFromGeoLocation(GeoLocation);

            EventArguments.Graphics.FillRectangle(System.Drawing.Brushes.OrangeRed, ScreenLocation.X - 2, ScreenLocation.Y - 2, 5, 5);
        }
    }
}
