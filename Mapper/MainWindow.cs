namespace Mapper
{
    internal class MainWindow : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ToolStripStatusLabel _CoordinatesLabel;
        private System.Windows.Forms.DataMap _Map;
        private System.Windows.Forms.ToolStripStatusLabel _ZoomLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
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
            System.Windows.Forms.ToolStripButton _OpenButton;
            this._ZoomLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._CoordinatesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._Map = new System.Windows.Forms.DataMap();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            _StatusBar = new System.Windows.Forms.StatusStrip();
            _OpenButton = new System.Windows.Forms.ToolStripButton();
            _StatusBar.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            // _ZoomLabel
            // 
            this._ZoomLabel.Name = "_ZoomLabel";
            this._ZoomLabel.Size = new System.Drawing.Size(59, 17);
            this._ZoomLabel.Text = "Zoom: XX";
            // 
            // _CoordinatesLabel
            // 
            this._CoordinatesLabel.Name = "_CoordinatesLabel";
            this._CoordinatesLabel.Size = new System.Drawing.Size(71, 17);
            this._CoordinatesLabel.Text = "Coordinates";
            // 
            // _OpenButton
            // 
            _OpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            _OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            _OpenButton.Name = "_OpenButton";
            _OpenButton.Size = new System.Drawing.Size(40, 22);
            _OpenButton.Text = "Open";
            _OpenButton.Click += new System.EventHandler(this._OnOpenButtonClicked);
            // 
            // _Map
            // 
            this._Map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._Map.BackColor = System.Drawing.SystemColors.ControlDark;
            this._Map.Location = new System.Drawing.Point(0, 25);
            this._Map.MapProvider = null;
            this._Map.Margin = new System.Windows.Forms.Padding(0);
            this._Map.Name = "_Map";
            this._Map.Size = new System.Drawing.Size(844, 483);
            this._Map.TabIndex = 4;
            this._Map.TranslateX = 0;
            this._Map.TranslateY = 0;
            this._Map.MouseMove += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseMoved);
            this._Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseDown);
            this._Map.MouseUp += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            _OpenButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(844, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(844, 530);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this._Map);
            this.Controls.Add(_StatusBar);
            this.Name = "MainWindow";
            this.Text = "Mapper";
            this.Load += new System.EventHandler(this._OnLoaded);
            _StatusBar.ResumeLayout(false);
            _StatusBar.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void _OnMapControlMouseMoved(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            if(_Map.IsScreenLocationInMap(EventArguments.Location) == true)
            {
                _UpdateCoordinatesLabel(_Map.GetGeoLocationFromScreenLocation(EventArguments.Location));
            }
            else
            {
                _UpdateCoordinatesLabel();
            }
            if(_MapControlDragPoint != null)
            {
                _Map.TranslateX += EventArguments.X - _MapControlDragPoint.Value.X;
                _Map.TranslateY += EventArguments.Y - _MapControlDragPoint.Value.Y;
                _MapControlDragPoint = EventArguments.Location;
            }
        }

        private void _OnMouseWheel(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            var MapControlPoint = _Map.PointToClient(PointToScreen(EventArguments.Location));

            _Map.SetZoom(_Map.Zoom + EventArguments.Delta / 120, MapControlPoint.X, MapControlPoint.Y);
            _UpdateZoomLabel();
        }

        private void _OnMapControlMouseDown(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            if(EventArguments.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _Map.Capture = true;
                _MapControlDragPoint = EventArguments.Location;
            }
        }

        private void _OnMapControlMouseUp(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            if(EventArguments.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _Map.Capture = false;
                _MapControlDragPoint = null;
            }
        }

        private void _OnLoaded(System.Object Sender, System.EventArgs EventArguments)
        {
            _Map.TranslateX = _Map.Width / 2 - 128;
            _Map.TranslateY = _Map.Height / 2 - 128;
            _UpdateZoomLabel();
            _UpdateCoordinatesLabel(new System.Point(0.0, 0.0));
        }

        private void _OnOpenButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            var OpenFileDialog = new System.Windows.Forms.OpenFileDialog();

            if(OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using(var Stream = new System.IO.FileStream(OpenFileDialog.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
                {
                    var GPX = GPS.GPX.DOM10.GPX.ReadFromStream(Stream);
                    var MaximalSpeed = 0.0;

                    foreach(var Track in GPX.Tracks)
                    {
                        foreach(var TrackSegment in Track.TrackSegments)
                        {
                            foreach(var TrackPoint in TrackSegment.TrackPoints)
                            {
                                if(TrackPoint.Speed > MaximalSpeed)
                                {
                                    MaximalSpeed = TrackPoint.Speed;
                                }
                            }
                        }
                    }
                    foreach(var Track in GPX.Tracks)
                    {
                        foreach(var TrackSegment in Track.TrackSegments)
                        {
                            foreach(var TrackPoint in TrackSegment.TrackPoints)
                            {
                                var Point = new System.Windows.Forms.DataMap.Point();

                                if(MaximalSpeed == 0.0)
                                {
                                    Point.Color = System.Drawing.Color.Black;
                                }
                                else
                                {
                                    Point.Color = _Mix(System.Drawing.Color.Red, System.Drawing.Color.Lime, TrackPoint.Speed / MaximalSpeed);
                                }
                                Point.GeoLocation = System.Windows.Forms.Map.GetGeoLocationFromGeoCoordinates(TrackPoint.Latitude, TrackPoint.Longitude);
                                _Map.Points.Add(Point);
                            }
                        }
                    }
                }
                _Map.Refresh();
            }
        }

        private void _UpdateZoomLabel()
        {
            _ZoomLabel.Text = "Zoom: " + _Map.Zoom;
        }

        private void _UpdateCoordinatesLabel()
        {
            _CoordinatesLabel.Text = "";
        }

        private void _UpdateCoordinatesLabel(System.Point GeoLocation)
        {
            _CoordinatesLabel.Text = _GetGeoCoordinates(GeoLocation);
        }

        private static System.String _GetGeoCoordinates(System.Point GeoLocation)
        {
            var Longitude = GeoLocation.X * 180.0 / System.Math.PI;
            var Latitude = GeoLocation.Y * 180.0 / System.Math.PI;

            return Latitude.GetTruncatedAsInt32() + "° " + System.Math.Abs(Latitude.GetFraction() / 100.0 * 60000.0).GetTruncatedAsInt32() + "’ " + ((Latitude > 0) ? ("N") : ("S")) + ", " + Longitude.GetTruncatedAsInt32() + "° " + System.Math.Abs(Longitude.GetFraction() / 100.0 * 60000.0).GetTruncatedAsInt32() + "’ " + ((Longitude > 0) ? ("O") : ("W"));
        }

        private static System.Drawing.Color _Mix(System.Drawing.Color Low, System.Drawing.Color High, System.Double Fraction)
        {
            return System.Drawing.Color.FromArgb((Low.R + ((High.R - Low.R) * Fraction)).GetTruncatedAsInt32(), (Low.G + ((High.G - Low.G) * Fraction)).GetTruncatedAsInt32(), (Low.B + ((High.B - Low.B) * Fraction)).GetTruncatedAsInt32());
        }
    }
}
