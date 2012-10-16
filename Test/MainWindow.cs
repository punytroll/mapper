namespace Test
{
    internal class MainWindow : System.Windows.Forms.Form
    {
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label _WorldLocationLabel;
        private System.Windows.Forms.MapControl _MapControl;
        private System.Windows.Forms.Label _TileLocationLabel;
        private System.Windows.Forms.Label _ScreenLocationLabel;
        private System.Windows.Forms.Label _PixelLocationLabel;
        private System.Windows.Forms.Label _GeoLocationLabel;
        private System.Drawing.Point? _MapControlDragPoint;

        public MainWindow()
        {
            InitializeComponent();
            _MapControl.MapProvider = new System.Windows.Forms.MapnikDownloader();
            _MapControlDragPoint = null;
            MouseWheel += _OnMapControlMouseWheel;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.Button _ZoomInButton;
            System.Windows.Forms.Button _ZoomOutButton;
            System.Windows.Forms.Button _TranslateUpButton;
            System.Windows.Forms.Button _TranslateDownButton;
            System.Windows.Forms.Button _TranslateLeftButton;
            System.Windows.Forms.Button _TranslateRightButton;
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._PixelLocationLabel = new System.Windows.Forms.Label();
            this._ScreenLocationLabel = new System.Windows.Forms.Label();
            this._TileLocationLabel = new System.Windows.Forms.Label();
            this._WorldLocationLabel = new System.Windows.Forms.Label();
            this._MapControl = new System.Windows.Forms.MapControl();
            this._GeoLocationLabel = new System.Windows.Forms.Label();
            _ZoomInButton = new System.Windows.Forms.Button();
            _ZoomOutButton = new System.Windows.Forms.Button();
            _TranslateUpButton = new System.Windows.Forms.Button();
            _TranslateDownButton = new System.Windows.Forms.Button();
            _TranslateLeftButton = new System.Windows.Forms.Button();
            _TranslateRightButton = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ZoomInButton
            // 
            _ZoomInButton.Location = new System.Drawing.Point(66, 12);
            _ZoomInButton.Name = "_ZoomInButton";
            _ZoomInButton.Size = new System.Drawing.Size(75, 23);
            _ZoomInButton.TabIndex = 0;
            _ZoomInButton.Text = "Zoom In";
            _ZoomInButton.UseVisualStyleBackColor = true;
            _ZoomInButton.Click += new System.EventHandler(this._OnZoomInButtonClicked);
            // 
            // _ZoomOutButton
            // 
            _ZoomOutButton.Location = new System.Drawing.Point(66, 41);
            _ZoomOutButton.Name = "_ZoomOutButton";
            _ZoomOutButton.Size = new System.Drawing.Size(75, 23);
            _ZoomOutButton.TabIndex = 1;
            _ZoomOutButton.Text = "Zoom Out";
            _ZoomOutButton.UseVisualStyleBackColor = true;
            _ZoomOutButton.Click += new System.EventHandler(this._OnZoomOutButtonClicked);
            // 
            // _TranslateUpButton
            // 
            _TranslateUpButton.Location = new System.Drawing.Point(66, 100);
            _TranslateUpButton.Name = "_TranslateUpButton";
            _TranslateUpButton.Size = new System.Drawing.Size(75, 23);
            _TranslateUpButton.TabIndex = 2;
            _TranslateUpButton.Text = "Up";
            _TranslateUpButton.UseVisualStyleBackColor = true;
            _TranslateUpButton.Click += new System.EventHandler(this._OnTranslateUpButtonClicked);
            // 
            // _TranslateDownButton
            // 
            _TranslateDownButton.Location = new System.Drawing.Point(66, 158);
            _TranslateDownButton.Name = "_TranslateDownButton";
            _TranslateDownButton.Size = new System.Drawing.Size(75, 23);
            _TranslateDownButton.TabIndex = 3;
            _TranslateDownButton.Text = "Down";
            _TranslateDownButton.UseVisualStyleBackColor = true;
            _TranslateDownButton.Click += new System.EventHandler(this._OnTranslateDownButtonClicked);
            // 
            // _TranslateLeftButton
            // 
            _TranslateLeftButton.Location = new System.Drawing.Point(27, 129);
            _TranslateLeftButton.Name = "_TranslateLeftButton";
            _TranslateLeftButton.Size = new System.Drawing.Size(75, 23);
            _TranslateLeftButton.TabIndex = 4;
            _TranslateLeftButton.Text = "Left";
            _TranslateLeftButton.UseVisualStyleBackColor = true;
            _TranslateLeftButton.Click += new System.EventHandler(this._OnTranslateLeftButtonClicked);
            // 
            // _TranslateRightButton
            // 
            _TranslateRightButton.Location = new System.Drawing.Point(108, 129);
            _TranslateRightButton.Name = "_TranslateRightButton";
            _TranslateRightButton.Size = new System.Drawing.Size(75, 23);
            _TranslateRightButton.TabIndex = 5;
            _TranslateRightButton.Text = "Right";
            _TranslateRightButton.UseVisualStyleBackColor = true;
            _TranslateRightButton.Click += new System.EventHandler(this._OnTranslateRightButtonClicked);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._GeoLocationLabel);
            this.splitContainer1.Panel1.Controls.Add(this._PixelLocationLabel);
            this.splitContainer1.Panel1.Controls.Add(this._ScreenLocationLabel);
            this.splitContainer1.Panel1.Controls.Add(this._TileLocationLabel);
            this.splitContainer1.Panel1.Controls.Add(this._WorldLocationLabel);
            this.splitContainer1.Panel1.Controls.Add(_TranslateRightButton);
            this.splitContainer1.Panel1.Controls.Add(_TranslateLeftButton);
            this.splitContainer1.Panel1.Controls.Add(_TranslateDownButton);
            this.splitContainer1.Panel1.Controls.Add(_TranslateUpButton);
            this.splitContainer1.Panel1.Controls.Add(_ZoomOutButton);
            this.splitContainer1.Panel1.Controls.Add(_ZoomInButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._MapControl);
            this.splitContainer1.Size = new System.Drawing.Size(844, 530);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 2;
            // 
            // _PixelLocationLabel
            // 
            this._PixelLocationLabel.AutoSize = true;
            this._PixelLocationLabel.Location = new System.Drawing.Point(12, 439);
            this._PixelLocationLabel.Margin = new System.Windows.Forms.Padding(3);
            this._PixelLocationLabel.Name = "_PixelLocationLabel";
            this._PixelLocationLabel.Size = new System.Drawing.Size(70, 13);
            this._PixelLocationLabel.TabIndex = 9;
            this._PixelLocationLabel.Text = "PixelLocation";
            // 
            // _ScreenLocationLabel
            // 
            this._ScreenLocationLabel.AutoSize = true;
            this._ScreenLocationLabel.Location = new System.Drawing.Point(12, 420);
            this._ScreenLocationLabel.Margin = new System.Windows.Forms.Padding(3);
            this._ScreenLocationLabel.Name = "_ScreenLocationLabel";
            this._ScreenLocationLabel.Size = new System.Drawing.Size(82, 13);
            this._ScreenLocationLabel.TabIndex = 8;
            this._ScreenLocationLabel.Text = "ScreenLocation";
            // 
            // _TileLocationLabel
            // 
            this._TileLocationLabel.AutoSize = true;
            this._TileLocationLabel.Location = new System.Drawing.Point(12, 477);
            this._TileLocationLabel.Margin = new System.Windows.Forms.Padding(3);
            this._TileLocationLabel.Name = "_TileLocationLabel";
            this._TileLocationLabel.Size = new System.Drawing.Size(65, 13);
            this._TileLocationLabel.TabIndex = 7;
            this._TileLocationLabel.Text = "TileLocation";
            // 
            // _WorldLocationLabel
            // 
            this._WorldLocationLabel.AutoSize = true;
            this._WorldLocationLabel.Location = new System.Drawing.Point(12, 458);
            this._WorldLocationLabel.Margin = new System.Windows.Forms.Padding(3);
            this._WorldLocationLabel.Name = "_WorldLocationLabel";
            this._WorldLocationLabel.Size = new System.Drawing.Size(76, 13);
            this._WorldLocationLabel.TabIndex = 6;
            this._WorldLocationLabel.Text = "WorldLocation";
            // 
            // _MapControl
            // 
            this._MapControl.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this._MapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MapControl.Location = new System.Drawing.Point(0, 0);
            this._MapControl.MapProvider = null;
            this._MapControl.Margin = new System.Windows.Forms.Padding(0);
            this._MapControl.Name = "_MapControl";
            this._MapControl.Size = new System.Drawing.Size(640, 530);
            this._MapControl.TabIndex = 1;
            this._MapControl.TranslateX = 0;
            this._MapControl.TranslateY = 0;
            this._MapControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseMoved);
            this._MapControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseDown);
            this._MapControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this._OnMapControlMouseUp);
            // 
            // _GeoLocationLabel
            // 
            this._GeoLocationLabel.AutoSize = true;
            this._GeoLocationLabel.Location = new System.Drawing.Point(13, 497);
            this._GeoLocationLabel.Margin = new System.Windows.Forms.Padding(3);
            this._GeoLocationLabel.Name = "_GeoLocationLabel";
            this._GeoLocationLabel.Size = new System.Drawing.Size(68, 13);
            this._GeoLocationLabel.TabIndex = 10;
            this._GeoLocationLabel.Text = "GeoLocation";
            // 
            // MainWindow
            // 
            this.ClientSize = new System.Drawing.Size(844, 530);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this._OnMainWindowLoaded);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void _OnZoomInButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _MapControl.SetZoom(_MapControl.Zoom + 1);
        }

        private void _OnZoomOutButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _MapControl.SetZoom(_MapControl.Zoom  - 1);
        }

        private void _OnTranslateUpButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _MapControl.TranslateY += 100;
        }

        private void _OnTranslateDownButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _MapControl.TranslateY -= 100;
        }

        private void _OnTranslateLeftButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _MapControl.TranslateX += 100;
        }

        private void _OnTranslateRightButtonClicked(System.Object Sender, System.EventArgs EventArguments)
        {
            _MapControl.TranslateX -= 100;
        }

        private void _OnMapControlMouseMoved(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            _ScreenLocationLabel.Text = "Screen: " + EventArguments.X + " / " + EventArguments.Y;

            var PixelLocation = _MapControl.GetPixelLocationFromScreenLocation(EventArguments.Location);

            _PixelLocationLabel.Text = "Pixel: " + PixelLocation.X + " / " + PixelLocation.Y;

            var TileLocation = _MapControl.GetTileLocationFromScreenLocation(EventArguments.Location);

            _TileLocationLabel.Text = "Tile: " + TileLocation.X + " / " + TileLocation.Y;

            var WorldLocation = _MapControl.GetWorldLocationFromScreenLocation(EventArguments.Location);

            _WorldLocationLabel.Text = "World: " + WorldLocation.X + " / " + WorldLocation.Y;

            var GeoLocation = _MapControl.GetGeoLocationFromWorldLocation(WorldLocation);

            _GeoLocationLabel.Text = "Geo: " + GeoLocation.X + " / " + GeoLocation.Y;
            if(_MapControlDragPoint != null)
            {
                _MapControl.TranslateX += EventArguments.X - _MapControlDragPoint.Value.X;
                _MapControl.TranslateY += EventArguments.Y - _MapControlDragPoint.Value.Y;
                _MapControlDragPoint = EventArguments.Location;
            }
        }

        private void _OnMapControlMouseWheel(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            var MapControlPoint = _MapControl.PointToClient(PointToScreen(EventArguments.Location));

            _MapControl.SetZoom(_MapControl.Zoom + EventArguments.Delta / 120, MapControlPoint.X, MapControlPoint.Y);
        }

        private void _OnMapControlMouseDown(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            if(EventArguments.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                _MapControl.Capture = true;
                _MapControlDragPoint = EventArguments.Location;
            }
        }

        private void _OnMapControlMouseUp(System.Object Sender, System.Windows.Forms.MouseEventArgs EventArguments)
        {
            if(EventArguments.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                _MapControl.Capture = false;
                _MapControlDragPoint = null;
            }
        }

        private void _OnMainWindowLoaded(System.Object Sender, System.EventArgs EventArguments)
        {
            _MapControl.TranslateX = _MapControl.Width / 2 - 128;
            _MapControl.TranslateY = _MapControl.Height / 2 - 128;
        }
    }
}
