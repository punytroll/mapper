namespace Mapper
{
    internal class MainWindow : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ToolStripStatusLabel _CoordinatesLabel;
        private System.Windows.Forms.DataMap _Map;
        private System.Windows.Forms.ToolStripStatusLabel _ZoomLabel;
        private System.Drawing.Point? _MapControlDragPoint;
        private System.Windows.Forms.TrackBar _OpacityTrackbar;
        private System.Windows.Forms.ToolStripDropDownButton _ColoringMenuItem;
        private readonly System.Collections.Generic.List<Mapper.Track> _Tracks;

        public MainWindow()
        {
            InitializeComponent();
            _Tracks = new System.Collections.Generic.List<Mapper.Track>();
            _Map.Opacity = _OpacityTrackbar.Value / 100.0f;
            _Map.MapProvider = new System.Windows.Forms.MapProvider();
            _Map.MapProvider.TileDownloader = new System.Windows.Forms.MapnikDownloader();
            _Map.MapProvider.HarddriveCache = new System.ImageHarddriveCache();
            _Map.MapProvider.HarddriveCache.RootDirectory = System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Cache"), _Map.MapProvider.TileDownloader.GetSetIdentifier());
            _MapControlDragPoint = null;
            MouseWheel += _OnMouseWheel;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.StatusStrip _StatusBar;
            System.Windows.Forms.ToolStripButton _OpenButton;
            System.Windows.Forms.ToolStrip _MenuBar;
            System.Windows.Forms.ToolStripMenuItem _ColorBlackMenuItem;
            System.Windows.Forms.ToolStripMenuItem _ColorBySpeedMenuItem;
            System.Windows.Forms.ToolStripMenuItem _ColorByAltitudeMenuItem;
            System.Windows.Forms.ToolStripMenuItem _ColorByAltitudeDifferenceMenuItem;
            this._ZoomLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._CoordinatesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._ColoringMenuItem = new System.Windows.Forms.ToolStripDropDownButton();
            this._OpacityTrackbar = new System.Windows.Forms.TrackBar();
            this._Map = new System.Windows.Forms.DataMap();
            _StatusBar = new System.Windows.Forms.StatusStrip();
            _OpenButton = new System.Windows.Forms.ToolStripButton();
            _MenuBar = new System.Windows.Forms.ToolStrip();
            _ColorBlackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            _ColorBySpeedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            _ColorByAltitudeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            _ColorByAltitudeDifferenceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            _StatusBar.SuspendLayout();
            _MenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._OpacityTrackbar)).BeginInit();
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
            // _MenuBar
            // 
            _MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            _OpenButton,
            this._ColoringMenuItem});
            _MenuBar.Location = new System.Drawing.Point(0, 0);
            _MenuBar.Name = "_MenuBar";
            _MenuBar.Size = new System.Drawing.Size(844, 25);
            _MenuBar.TabIndex = 5;
            _MenuBar.Text = "toolStrip1";
            // 
            // _ColoringMenuItem
            // 
            this._ColoringMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._ColoringMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            _ColorBlackMenuItem,
            _ColorBySpeedMenuItem,
            _ColorByAltitudeMenuItem,
            _ColorByAltitudeDifferenceMenuItem});
            this._ColoringMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ColoringMenuItem.Name = "_ColoringMenuItem";
            this._ColoringMenuItem.Size = new System.Drawing.Size(66, 22);
            this._ColoringMenuItem.Text = "Coloring";
            // 
            // _ColorBlackMenuItem
            // 
            _ColorBlackMenuItem.Name = "_ColorBlackMenuItem";
            _ColorBlackMenuItem.Size = new System.Drawing.Size(152, 22);
            _ColorBlackMenuItem.Text = "Black";
            _ColorBlackMenuItem.Click += new System.EventHandler(this._OnColorBlackMenuItemClicked);
            // 
            // _ColorBySpeedMenuItem
            // 
            _ColorBySpeedMenuItem.Name = "_ColorBySpeedMenuItem";
            _ColorBySpeedMenuItem.Size = new System.Drawing.Size(152, 22);
            _ColorBySpeedMenuItem.Text = "by Speed";
            _ColorBySpeedMenuItem.Click += new System.EventHandler(this._OnColorBySpeedMenuItemClicked);
            // 
            // _ColorByAltitudeMenuItem
            // 
            _ColorByAltitudeMenuItem.Name = "_ColorByAltitudeMenuItem";
            _ColorByAltitudeMenuItem.Size = new System.Drawing.Size(152, 22);
            _ColorByAltitudeMenuItem.Text = "by Altitude";
            _ColorByAltitudeMenuItem.Click += new System.EventHandler(this._OnColorByAltitudeMenuItemClicked);
            // 
            // _ColorBySlopeMenuItem
            // 
            _ColorByAltitudeDifferenceMenuItem.Name = "_ColorByAltitudeDifferenceMenuItem";
            _ColorByAltitudeDifferenceMenuItem.Size = new System.Drawing.Size(152, 22);
            _ColorByAltitudeDifferenceMenuItem.Text = "by Altitude Difference";
            _ColorByAltitudeDifferenceMenuItem.Click += new System.EventHandler(this._OnColorByAltitudeDifferenceMenuItemClicked);
            // 
            // _OpacityTrackbar
            // 
            this._OpacityTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._OpacityTrackbar.BackColor = System.Drawing.SystemColors.Control;
            this._OpacityTrackbar.Location = new System.Drawing.Point(728, 41);
            this._OpacityTrackbar.Maximum = 100;
            this._OpacityTrackbar.Name = "_OpacityTrackbar";
            this._OpacityTrackbar.Size = new System.Drawing.Size(104, 45);
            this._OpacityTrackbar.TabIndex = 6;
            this._OpacityTrackbar.TabStop = false;
            this._OpacityTrackbar.TickStyle = System.Windows.Forms.TickStyle.None;
            this._OpacityTrackbar.Value = 50;
            this._OpacityTrackbar.Scroll += new System.EventHandler(this._OnOpacityTrackbarScrolled);
            this._OpacityTrackbar.Enter += new System.EventHandler(this._OnOpacityTrackbarFocusEntered);
            // 
            // _Map
            // 
            this._Map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._Map.BackColor = System.Drawing.Color.Black;
            this._Map.Location = new System.Drawing.Point(0, 25);
            this._Map.MapProvider = null;
            this._Map.Margin = new System.Windows.Forms.Padding(0);
            this._Map.Name = "_Map";
            this._Map.Opacity = 1F;
            this._Map.Size = new System.Drawing.Size(844, 483);
            this._Map.TabIndex = 4;
            this._Map.TranslateX = 0;
            this._Map.TranslateY = 0;
            this._Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseDown);
            this._Map.MouseMove += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseMoved);
            this._Map.MouseUp += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseUp);
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(844, 530);
            this.Controls.Add(this._OpacityTrackbar);
            this.Controls.Add(_MenuBar);
            this.Controls.Add(this._Map);
            this.Controls.Add(_StatusBar);
            this.Name = "MainWindow";
            this.Text = "Mapper";
            this.Load += new System.EventHandler(this._OnLoaded);
            _StatusBar.ResumeLayout(false);
            _StatusBar.PerformLayout();
            _MenuBar.ResumeLayout(false);
            _MenuBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._OpacityTrackbar)).EndInit();
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
                    if(OpenFileDialog.FileName.EndsWith(".gpx") == true)
                    {
                        var GPX = GPS.GPX.DOM10.GPX.ReadFromStream(Stream);
                        var Track = new Mapper.Track();

                        Track.DrawLines = false;
                        foreach(var GPXTrack in GPX.Tracks)
                        {
                            foreach(var GPXTrackSegment in GPXTrack.TrackSegments)
                            {
                                foreach(var GPXTrackPoint in GPXTrackSegment.TrackPoints)
                                {
                                    var TrackPoint = new Records.Record();

                                    TrackPoint.Add("latitude", System.Windows.Forms.Map.GetLatitudeLocationFromLatitudeCoordinates(GPXTrackPoint.Latitude));
                                    TrackPoint.Add("longitude", System.Windows.Forms.Map.GetLongitudeLocationFromLongitudeCoordinates(GPXTrackPoint.Longitude));
                                    TrackPoint.Add("altitude", GPXTrackPoint.Elevation);
                                    TrackPoint.Add("speed", GPXTrackPoint.Speed);
                                    TrackPoint.Add("size", 5.0f);
                                    TrackPoint.Add("color", System.Drawing.Color.Black);
                                    Track.Append(TrackPoint);
                                }
                            }
                        }
                        _Tracks.Add(Track);
                        Track.AddField("altitude-difference-before", 0.0);
                        Track.AddField("altitude-difference-after", 0.0);
                        Track.UpdateFieldOfSecondOfPair<System.Double, System.Double>("altitude-difference-before", "altitude", (One, Two) => Two - One);
                        Track.UpdateFieldOfFirstOfPair<System.Double, System.Double>("altitude-difference-after", "altitude", (One, Two) => Two - One);
                        Track.AddField("altitude-difference", "altitude-difference-before", "altitude-difference-after", (System.Double Before, System.Double After) => (Before + After) / 2.0);
                    }
                    else if(OpenFileDialog.FileName.EndsWith(".kml") == true)
                    {
                        var KML = GPS.KML.Version_2_2.KML.ReadFromStream(Stream);

                        foreach(var Placemark in KML.Placemarks)
                        {
                            var Track = new Mapper.Track();

                            Track.DrawLines = true;
                            if(Placemark.Name != null)
                            {
                                Track.Name = Placemark.Name;
                            }
                            if(Placemark.LineString != null)
                            {

                                foreach(var Coordinates in Placemark.LineString.Coordinates)
                                {
                                    var TrackPoint = new Records.Record();

                                    TrackPoint.Add("latitude", System.Windows.Forms.Map.GetLatitudeLocationFromLatitudeCoordinates(Coordinates.Latitude));
                                    TrackPoint.Add("longitude", System.Windows.Forms.Map.GetLongitudeLocationFromLongitudeCoordinates(Coordinates.Longitude));
                                    if(Coordinates.Altitude != null)
                                    {
                                        TrackPoint.Add("altitude", Coordinates.Altitude);
                                    }
                                    TrackPoint.Add("size", 5.0f);
                                    TrackPoint.Add("color", System.Drawing.Color.Black);
                                    Track.Append(TrackPoint);
                                }
                            }
                            _Tracks.Add(Track);
                        }
                    }
                }
                _RebuildDataMap();
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
            _CoordinatesLabel.Text = _GetGeoCoordinateString(GeoLocation);
        }

        private static System.String _GetGeoCoordinateString(System.Point GeoLocation)
        {
            var Longitude = System.Windows.Forms.Map.GetLongitudeCoordinatesFromLongitudeLocation(GeoLocation.X);
            var Latitude = System.Windows.Forms.Map.GetLongitudeCoordinatesFromLongitudeLocation(GeoLocation.Y);

            return Latitude.GetTruncatedAsInt32() + "° " + System.Math.Abs(Latitude.GetFraction() * 600000.0).GetTruncatedAsInt32() + "’ " + ((Latitude > 0) ? ("N") : ("S")) + ", " + Longitude.GetTruncatedAsInt32() + "° " + System.Math.Abs(Longitude.GetFraction() * 600000.0).GetTruncatedAsInt32() + "’ " + ((Longitude > 0) ? ("O") : ("W"));
        }

        private static System.Drawing.Color _Mix(System.Drawing.Color Low, System.Drawing.Color High, System.Double Fraction)
        {
            return System.Drawing.Color.FromArgb((Low.R + ((High.R - Low.R) * Fraction)).GetTruncatedAsInt32(), (Low.G + ((High.G - Low.G) * Fraction)).GetTruncatedAsInt32(), (Low.B + ((High.B - Low.B) * Fraction)).GetTruncatedAsInt32());
        }

        private void _RebuildDataMap()
        {
            _Map.Points.Clear();
            _Map.Lines.Clear();
            foreach(var Track in _Tracks)
            {
                Records.Record LastTrackPoint = null;

                foreach(var TrackPoint in Track)
                {
                    var Point = new System.Windows.Forms.DataMap.Point();

                    Point.Object = TrackPoint;
                    Point.Color = TrackPoint.Get<System.Drawing.Color>("color");
                    Point.Size = TrackPoint.Get<System.Single>("size");
                    Point.GeoLocation = new System.Point(TrackPoint.Get<System.Double>("longitude"), TrackPoint.Get<System.Double>("latitude"));
                    _Map.Points.Add(Point);
                    if((Track.DrawLines == true) && (LastTrackPoint != null))
                    {
                        var Line = new System.Windows.Forms.DataMap.Line();

                        Line.Object = new System.Pair<Records.Record, Records.Record>(LastTrackPoint, TrackPoint);
                        Line.Color = System.Drawing.Color.Black;
                        Line.Width = 2.0f;
                        Line.BeginGeoLocation = new System.Point(LastTrackPoint.Get<System.Double>("longitude"), LastTrackPoint.Get<System.Double>("latitude"));
                        Line.EndGeoLocation = new System.Point(TrackPoint.Get<System.Double>("longitude"), TrackPoint.Get<System.Double>("latitude"));
                        _Map.Lines.Add(Line);
                    }
                    LastTrackPoint = TrackPoint;
                }
            }
            _Map.Refresh();
        }

        private void _ColorByColourFunction(Mapper.Track Track, System.Func<Records.Record, System.Drawing.Color> ColourFunction)
        {
            foreach(var TrackPoint in Track)
            {
                TrackPoint.Update("color", ColourFunction(TrackPoint));
            }
        }

        private void _ColorByPropertyAndMinimumMaximum(Mapper.Track Track, System.String PropertyName, System.Double Minimum, System.Double Maximum)
        {
            if(Minimum == Maximum)
            {
                Maximum = Maximum + 1;
            }
            _ColorByColourFunction(Track, Record => _Mix(System.Drawing.Color.Red, System.Drawing.Color.Yellow, (Record.Get<System.Double>(PropertyName) - Minimum) / (Maximum - Minimum)));
        }

        private void _GetMinimumAndMaximum(Mapper.Track Track, System.String PropertyName, ref System.Double Minimum, ref System.Double Maximum)
        {
            Minimum = System.Double.MaxValue;
            Maximum = System.Double.MinValue;
            foreach(var TrackPoint in Track)
            {
                if(TrackPoint.Has(PropertyName) == true)
                {
                    if(TrackPoint.Get<System.Double>(PropertyName) > Maximum)
                    {
                        Maximum = TrackPoint.Get<System.Double>(PropertyName);
                    }
                    if(TrackPoint.Get<System.Double>(PropertyName) < Minimum)
                    {
                        Minimum = TrackPoint.Get<System.Double>(PropertyName);
                    }
                }
            }
        }

        private void _ColorByProperty(Mapper.Track Track, System.String PropertyName)
        {
            var Minimum = System.Double.MaxValue;
            var Maximum = System.Double.MinValue;

            _GetMinimumAndMaximum(Track, PropertyName, ref Minimum, ref Maximum);
            _ColorByPropertyAndMinimumMaximum(Track, PropertyName, Minimum, Maximum);
        }

        private void _ColorTracksByProperty(System.Collections.Generic.List<Mapper.Track> Tracks, System.String PropertyName)
        {
            foreach(var Track in _Tracks)
            {
                _ColorByProperty(Track, PropertyName);
            }
        }

        private void _OnColorBlackMenuItemClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            foreach(var Track in _Tracks)
            {
                _ColorByColourFunction(Track, Record => System.Drawing.Color.Black);
            }
            _RebuildDataMap();
        }

        private void _OnColorBySpeedMenuItemClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ColorTracksByProperty(_Tracks, "speed");
            _RebuildDataMap();
        }

        private void _OnColorByAltitudeMenuItemClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ColorTracksByProperty(_Tracks, "altitude");
            _RebuildDataMap();
        }

        private void _OnColorByAltitudeDifferenceMenuItemClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ColorTracksByProperty(_Tracks, "altitude-difference");
            _RebuildDataMap();
        }

        private void _OnOpacityTrackbarScrolled(System.Object Sender, System.EventArgs EventArguments)
        {
            _Map.Opacity = _OpacityTrackbar.Value / 100.0f;
        }

        private void _OnOpacityTrackbarFocusEntered(System.Object Sender, System.EventArgs EventArguments)
        {
            _Map.Focus();
        }
    }
}
