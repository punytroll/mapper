﻿namespace Test
{
    internal class MainWindow : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ToolStripStatusLabel _CoordinatesLabel;
        private System.Windows.Forms.DataMap _Map;
        private System.Windows.Forms.ToolStripStatusLabel _ZoomLabel;
        private System.Drawing.Point? _MapControlDragPoint;

        public MainWindow()
        {
            InitializeComponent();
            _Map.MapProvider = new System.Windows.Forms.MapnikDownloader();
            _MapControlDragPoint = null;
            MouseWheel += _OnMouseWheel;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.StatusStrip _StatusBar;
            this._CoordinatesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._Map = new System.Windows.Forms.DataMap();
            this._ZoomLabel = new System.Windows.Forms.ToolStripStatusLabel();
            _StatusBar = new System.Windows.Forms.StatusStrip();
            _StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _StatusBar
            // 
            _StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ZoomLabel,
            this._CoordinatesLabel});
            _StatusBar.Location = new System.Drawing.Point(0, 508);
            _StatusBar.Name = "_StatusBar";
            _StatusBar.Size = new System.Drawing.Size(844, 22);
            _StatusBar.TabIndex = 3;
            _StatusBar.Text = "statusStrip1";
            // 
            // _CoordinatesLabel
            // 
            this._CoordinatesLabel.Name = "_CoordinatesLabel";
            this._CoordinatesLabel.Size = new System.Drawing.Size(71, 17);
            this._CoordinatesLabel.Text = "Coordinates";
            // 
            // _Map
            // 
            this._Map.BackColor = System.Drawing.SystemColors.ControlDark;
            this._Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Map.Location = new System.Drawing.Point(0, 0);
            this._Map.MapProvider = null;
            this._Map.Margin = new System.Windows.Forms.Padding(0);
            this._Map.Name = "_Map";
            this._Map.Size = new System.Drawing.Size(844, 508);
            this._Map.TabIndex = 4;
            this._Map.TranslateX = 0;
            this._Map.TranslateY = 0;
            this._Map.MouseMove += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseMoved);
            this._Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseDown);
            this._Map.MouseUp += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseUp);
            // 
            // _ZoomLabel
            // 
            this._ZoomLabel.Name = "_ZoomLabel";
            this._ZoomLabel.Size = new System.Drawing.Size(59, 17);
            this._ZoomLabel.Text = "Zoom: XX";
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(844, 530);
            this.Controls.Add(this._Map);
            this.Controls.Add(_StatusBar);
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this._OnMainWindowLoaded);
            _StatusBar.ResumeLayout(false);
            _StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void _OnMapControlMouseMoved(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            var GeoLocation = _Map.GetGeoLocationFromScreenLocation(EventArguments.Location);

            _CoordinatesLabel.Text = _GetGeoCoordinates(GeoLocation);
            if(_MapControlDragPoint != null)
            {
                _Map.TranslateX += EventArguments.X - _MapControlDragPoint.Value.X;
                _Map.TranslateY += EventArguments.Y - _MapControlDragPoint.Value.Y;
                _MapControlDragPoint = EventArguments.Location;
            }
        }

        private static System.String _GetGeoCoordinates(System.Drawing.PointF GeoLocation)
        {
            var Longitude = GeoLocation.X * 180.0 / System.Math.PI;
            var Latitude = GeoLocation.Y * 180.0 / System.Math.PI;

            return Latitude.GetTruncatedAsInt32() + "° " + System.Math.Abs(Latitude.GetFraction() / 100.0 * 60000.0).GetTruncatedAsInt32() + "’ " + ((Latitude > 0) ? ("N") : ("S")) + ", " + Longitude.GetTruncatedAsInt32() + "° " + System.Math.Abs(Longitude.GetFraction() / 100.0 * 60000.0).GetTruncatedAsInt32() + "’ " + ((Longitude > 0) ? ("O") : ("W"));
        }

        private void _OnMouseWheel(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            var MapControlPoint = _Map.PointToClient(PointToScreen(EventArguments.Location));

            _Map.SetZoom(_Map.Zoom + EventArguments.Delta / 120, MapControlPoint.X, MapControlPoint.Y);
            _ZoomLabel.Text = "Zoom: " + _Map.Zoom;
        }

        private void _OnMapControlMouseDown(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            if(EventArguments.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                _Map.Capture = true;
                _MapControlDragPoint = EventArguments.Location;
            }
        }

        private void _OnMapControlMouseUp(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            if(EventArguments.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                _Map.Capture = false;
                _MapControlDragPoint = null;
            }
        }

        private void _OnMainWindowLoaded(System.Object Sender, System.EventArgs EventArguments)
        {
            _Map.TranslateX = _Map.Width / 2 - 128;
            _Map.TranslateY = _Map.Height / 2 - 128;
        }
    }
}
