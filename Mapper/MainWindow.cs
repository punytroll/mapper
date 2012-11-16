using Records;

namespace Mapper
{
    internal class MainWindow : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ToolStripStatusLabel _CoordinatesLabel;
        private System.Windows.Forms.DataMap _Map;
        private System.Windows.Forms.ToolStripStatusLabel _ZoomLabel;
        private System.Windows.Forms.ToolStripMenuItem blackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bySpeedToolStripMenuItem;
        private System.Drawing.Point? _MapControlDragPoint;
        private System.Windows.Forms.ToolStripMenuItem _ColourByElevationDifferenceMenuItem;
        private readonly System.Collections.Generic.List<Records.Records> _Records;

        public MainWindow()
        {
            InitializeComponent();
            _Records = new System.Collections.Generic.List<Records.Records>();
            _Map.MapProvider = new System.Windows.Forms.MapnikDownloader();
            _MapControlDragPoint = null;
            MouseWheel += _OnMouseWheel;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.StatusStrip _StatusBar;
            System.Windows.Forms.ToolStripButton _OpenButton;
            System.Windows.Forms.ToolStrip _MenuBar;
            System.Windows.Forms.ToolStripDropDownButton _ColouringMenuItem;
            System.Windows.Forms.ToolStripMenuItem _ColourByHeightMenuItem;
            this._ZoomLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._CoordinatesLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.blackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bySpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._Map = new System.Windows.Forms.DataMap();
            this._ColourByElevationDifferenceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            _StatusBar = new System.Windows.Forms.StatusStrip();
            _OpenButton = new System.Windows.Forms.ToolStripButton();
            _MenuBar = new System.Windows.Forms.ToolStrip();
            _ColouringMenuItem = new System.Windows.Forms.ToolStripDropDownButton();
            _ColourByHeightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            _StatusBar.SuspendLayout();
            _MenuBar.SuspendLayout();
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
            _ColouringMenuItem});
            _MenuBar.Location = new System.Drawing.Point(0, 0);
            _MenuBar.Name = "_MenuBar";
            _MenuBar.Size = new System.Drawing.Size(844, 25);
            _MenuBar.TabIndex = 5;
            _MenuBar.Text = "toolStrip1";
            // 
            // _ColouringMenuItem
            // 
            _ColouringMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            _ColouringMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blackToolStripMenuItem,
            this.bySpeedToolStripMenuItem,
            _ColourByHeightMenuItem,
            this._ColourByElevationDifferenceMenuItem});
            _ColouringMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            _ColouringMenuItem.Name = "_ColouringMenuItem";
            _ColouringMenuItem.Size = new System.Drawing.Size(73, 22);
            _ColouringMenuItem.Text = "Colouring";
            // 
            // blackToolStripMenuItem
            // 
            this.blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            this.blackToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.blackToolStripMenuItem.Text = "Black";
            this.blackToolStripMenuItem.Click += new System.EventHandler(this._ColourBlackMenuItemClicked);
            // 
            // bySpeedToolStripMenuItem
            // 
            this.bySpeedToolStripMenuItem.Name = "bySpeedToolStripMenuItem";
            this.bySpeedToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.bySpeedToolStripMenuItem.Text = "by Speed";
            this.bySpeedToolStripMenuItem.Click += new System.EventHandler(this._ColourBySpeedMenuItemClicked);
            // 
            // _ColourByHeightMenuItem
            // 
            _ColourByHeightMenuItem.Name = "_ColourByHeightMenuItem";
            _ColourByHeightMenuItem.Size = new System.Drawing.Size(194, 22);
            _ColourByHeightMenuItem.Text = "by Elevation";
            _ColourByHeightMenuItem.Click += new System.EventHandler(this._ColourByHeightMenuItem_Click);
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
            // _ColourByElevationDifferenceMenuItem
            // 
            this._ColourByElevationDifferenceMenuItem.Name = "_ColourByElevationDifferenceMenuItem";
            this._ColourByElevationDifferenceMenuItem.Size = new System.Drawing.Size(194, 22);
            this._ColourByElevationDifferenceMenuItem.Text = "by Elevation difference";
            this._ColourByElevationDifferenceMenuItem.Click += new System.EventHandler(this._ColourByElevationDifferenceMenuItem_Click);
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(844, 530);
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
                    var Records = new Records.Records();

                    foreach(var Track in GPX.Tracks)
                    {
                        foreach(var TrackSegment in Track.TrackSegments)
                        {
                            foreach(var TrackPoint in TrackSegment.TrackPoints)
                            {
                                var Record = new Records.Record();

                                Record.Add("geo-location", System.Windows.Forms.Map.GetGeoLocationFromGeoCoordinates(TrackPoint.Latitude, TrackPoint.Longitude));
                                Record.Add("elevation", TrackPoint.Elevation);
                                Record.Add("speed", TrackPoint.Speed);
                                Records.Append(Record);
                            }
                        }
                    }
                    _Records.Add(Records);
                    Records.Map((One, Two) => Two.Add("elevation-difference-before", Two.Get<System.Double>("elevation") - One.Get<System.Double>("elevation")));
                    Records.First.Add("elevation-difference-before", 0.0);
                    Records.Map((One, Two) => One.Add("elevation-difference-after", Two.Get<System.Double>("elevation") - One.Get<System.Double>("elevation")));
                    Records.Last.Add("elevation-difference-after", 0.0);
                    Records.AddField("elevation-difference", "elevation-difference-before", "elevation-difference-after", (System.Double Before, System.Double After) => (Before + After) / 2.0);
                    foreach(var Record in Records)
                    {
                        var Point = new System.Windows.Forms.DataMap.Point();

                        Point.Object = Record;
                        Point.Color = System.Drawing.Color.Black;
                        Point.GeoLocation = Record.Get<System.Point>("geo-location");
                        _Map.Points.Add(Point);
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

        private void _ColourByColourFunction(System.Func<Records.Record, System.Drawing.Color> ColourFunction)
        {
            foreach(var Records in _Records)
            {
                foreach(var Record in Records)
                {
                    var Point = new System.Windows.Forms.DataMap.Point();

                    Point.Object = Record;
                    Point.Color = ColourFunction(Record);
                    Point.GeoLocation = Record.Get<System.Point>("geo-location");
                    _Map.Points.Add(Point);
                }
            }
        }

        private void _ColourByPropertyAndMinimumMaximum(System.String PropertyName, System.Double Minimum, System.Double Maximum)
        {
            _ColourByColourFunction(Record => _Mix(System.Drawing.Color.Red, System.Drawing.Color.Yellow, (Record.Get<System.Double>(PropertyName) - Minimum) / (Maximum - Minimum)));
        }

        private void _ColourBlackMenuItemClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _Map.Points.Clear();
            _ColourByColourFunction(Record => System.Drawing.Color.Black);
            _Map.Refresh();
        }

        private void _GetMinimumAndMaximum(System.String PropertyName, ref System.Double Minimum, ref System.Double Maximum)
        {
            Minimum = System.Double.MaxValue;
            Maximum = System.Double.MinValue;
            foreach(var Records in _Records)
            {
                foreach(var Record in Records)
                {
                    if(Record.Get<System.Double>(PropertyName) > Maximum)
                    {
                        Maximum = Record.Get<System.Double>(PropertyName);
                    }
                    if(Record.Get<System.Double>(PropertyName) < Minimum)
                    {
                        Minimum = Record.Get<System.Double>(PropertyName);
                    }
                }
            }
        }

        private void _ColourByProperty(System.String PropertyName)
        {
            var Minimum = System.Double.MaxValue;
            var Maximum = System.Double.MinValue;

            _GetMinimumAndMaximum(PropertyName, ref Minimum, ref Maximum);
            _Map.Points.Clear();
            _ColourByPropertyAndMinimumMaximum(PropertyName, Minimum, Maximum);
            _Map.Refresh();
        }

        private void _ColourBySpeedMenuItemClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _ColourByProperty("speed");
        }

        private void _ColourByHeightMenuItem_Click(System.Object Sender, System.EventArgs EventArguments)
        {
            _ColourByProperty("elevation");
        }

        private void _ColourByElevationDifferenceMenuItem_Click(System.Object Sender, System.EventArgs EventArguments)
        {
            _ColourByProperty("elevation-difference");
        }
    }
}
