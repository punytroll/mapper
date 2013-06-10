﻿namespace System.Windows.Forms
{
    public class MapTile
    {
        public event System.Action ImageChanged;

        private System.DateTime? _ExpireDateTime;
        private System.Drawing.Image _Image;
        private System.String _SetIdentifier;
        private readonly System.Int32 _X;
        private readonly System.Int32 _Y;
        private readonly System.Int32 _Zoom;

        public System.DateTime? ExpireDateTime
        {
            get
            {
                return _ExpireDateTime;
            }
        }

        public System.Drawing.Image Image
        {
            get
            {
                return _Image;
            }
        }

        public System.String SetIdentifier
        {
            get
            {
                return _SetIdentifier;
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
            _ExpireDateTime = null;
            _Image = null;
            _SetIdentifier = null;
            _X = X;
            _Y = Y;
            _Zoom = Zoom;
        }

        public void SetImage(System.Drawing.Image Image)
        {
            if(Image != null)
            {
                _Image = Image.ConvertToPixelFormat(System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }
            else
            {
                _Image = null;
            }

            var Delegate = ImageChanged;

            if(Delegate != null)
            {
                Delegate();
            }
        }

        public void SetExpireDateTime(System.DateTime ExpireDateTime)
        {
            _ExpireDateTime = ExpireDateTime;
        }

        public void SetSetIdentifier(System.String SetIdentifier)
        {
            _SetIdentifier = SetIdentifier;
        }
    }
}
