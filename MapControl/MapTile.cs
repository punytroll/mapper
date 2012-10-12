namespace System.Windows.Forms
{
    public class MapTile
    {
        public enum ImageStatus
        {
            Available,
            NotAvailable,
            Fetching
        }

        public event System.Action StatusChanged;

        private System.Drawing.Image _Image;
        private System.Windows.Forms.MapTile.ImageStatus _Status;
        private readonly System.Int32 _X;
        private readonly System.Int32 _Y;
        private readonly System.Int32 _Zoom;

        public System.Drawing.Image Image
        {
            get
            {
                return _Image;
            }
        }

        public System.Windows.Forms.MapTile.ImageStatus Status
        {
            get
            {
                return _Status;
            }
        }

        public System.Int32 X
        {
            get
            {
                return _X;
            }
        }

        public System.Int32 Y
        {
            get
            {
                return _Y;
            }
        }

        public System.Int32 Zoom
        {
            get
            {
                return _Zoom;
            }
        }

        public MapTile(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
        {
            _Image = null;
            _Status = System.Windows.Forms.MapTile.ImageStatus.Fetching;
            _X = X;
            _Y = Y;
            _Zoom = Zoom;
        }

        public void SetImage(System.Drawing.Image Image)
        {
            _Image = Image;
            if(_Image == null)
            {
                _SetStatus(System.Windows.Forms.MapTile.ImageStatus.NotAvailable);
            }
            else
            {
                _SetStatus(System.Windows.Forms.MapTile.ImageStatus.Available);
            }
        }

        private void _SetStatus(System.Windows.Forms.MapTile.ImageStatus Status)
        {
            if(_Status != Status)
            {
                _Status = Status;

                var Delegate = StatusChanged;

                if(Delegate != null)
                {
                    Delegate();
                }
            }
        }
    }
}
