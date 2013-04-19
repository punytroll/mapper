using System.Drawing;

namespace System
{
    public class ImageHarddriveCache
    {
        public System.String RootDirectory
        {
            get
            {
                return _RootDirectory;
            }
            set
            {
                _RootDirectory = value;
            }
        }

        private System.String _RootDirectory;

        public ImageHarddriveCache()
        {
            _RootDirectory = null;
        }

        public System.Drawing.Image LoadTile(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
        {
            System.Drawing.Image Result = null;

            if(_RootDirectory != null)
            {
                var Path = _RootDirectory;

                if(System.IO.Directory.Exists(Path) == true)
                {
                    Path = System.IO.Path.Combine(Path, Zoom.ToString(System.Globalization.CultureInfo.InvariantCulture));
                    if(System.IO.Directory.Exists(Path) == true)
                    {
                        Path = System.IO.Path.Combine(Path, X.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        if(System.IO.Directory.Exists(Path) == true)
                        {
                            Path = System.IO.Path.Combine(Path, Y.ToString(System.Globalization.CultureInfo.InvariantCulture) + ".png");
                            if(System.IO.File.Exists(Path) == true)
                            {
                                Result = new Bitmap(Path);
                            }
                        }
                    }
                }
            }

            return Result;
        }

        public void StoreTile(System.Int32 Zoom, System.Int32 X, System.Int32 Y, System.Drawing.Image Image)
        {
            if(_RootDirectory != null)
            {
                var Path = _RootDirectory;

                if(System.IO.Directory.Exists(Path) == false)
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
                Path = System.IO.Path.Combine(Path, Zoom.ToString(System.Globalization.CultureInfo.InvariantCulture));
                if(System.IO.Directory.Exists(Path) == false)
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
                Path = System.IO.Path.Combine(Path, X.ToString(System.Globalization.CultureInfo.InvariantCulture));
                if(System.IO.Directory.Exists(Path) == false)
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
                Path = System.IO.Path.Combine(Path, Y.ToString(System.Globalization.CultureInfo.InvariantCulture) + ".png");
                if(System.IO.File.Exists(Path) ==
                    false)
                {
                    Image.Save(Path, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
}
