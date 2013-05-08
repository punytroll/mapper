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

        private readonly System.Object _LocksLock;
        private readonly System.Collections.Generic.Dictionary<System.String, System.Object> _Locks;
        private System.String _RootDirectory;

        public ImageHarddriveCache()
        {
            _LocksLock = new System.Object();
            _Locks = new System.Collections.Generic.Dictionary<System.String, System.Object>();
            _RootDirectory = null;
        }

        public System.Drawing.Image LoadTileImage(System.Int32 Zoom, System.Int32 X, System.Int32 Y)
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
                            var Entry = Y.ToString(System.Globalization.CultureInfo.InvariantCulture) + ".png";

                            if(IsExpired(Path, Entry) == false)
                            {
                                Path = System.IO.Path.Combine(Path, Entry);
                                if(System.IO.File.Exists(Path) == true)
                                {
                                    Result = new System.Drawing.Bitmap(Path);
                                }
                            }
                        }
                    }
                }
            }

            return Result;
        }

        private void LockDatabase(System.String Path)
        {
            System.Object Lock;

            lock(_LocksLock)
            {
                if(_Locks.ContainsKey(Path) == false)
                {
                    Lock = new System.Object();
                    _Locks[Path] = Lock;
                }
                else
                {
                    Lock = _Locks[Path];
                }
            }
            System.Threading.Monitor.Enter(Lock);
        }

        private void ReleaseDatabase(System.String Path)
        {
            System.Object Lock = null;

            lock(_LocksLock)
            {
                if(_Locks.ContainsKey(Path) == true)
                {
                    Lock = _Locks[Path];
                }
            }
            if(Lock != null)
            {
                System.Threading.Monitor.Exit(Lock);
            }
        }

        public System.Boolean IsExpired(System.String Path, System.String Entry)
        {
            LockDatabase(Path);

            var Result = true;
            var DatabasePath = System.IO.Path.Combine(Path, "expire.db");

            if(System.IO.File.Exists(DatabasePath) == true)
            {
                using(var File = new System.IO.FileStream(DatabasePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None))
                {
                    using(var Reader = new System.IO.StreamReader(File))
                    {
                        System.String Line;

                        while((Line = Reader.ReadLine()) != null)
                        {
                            var Parts = Line.Split('=');

                            if(Parts.Length == 2)
                            {
                                if(Parts[0] == Entry)
                                {
                                    Result = System.DateTime.Parse(Parts[1], System.Globalization.CultureInfo.InvariantCulture) < System.DateTime.Now;
                                }
                            }
                        }
                    }
                }
            }
            ReleaseDatabase(Path);

            return Result;
        }

        public void StoreTileImage(System.Int32 Zoom, System.Int32 X, System.Int32 Y, System.Drawing.Image Image, System.DateTime ExpireDateTime)
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

                var Entry = Y.ToString(System.Globalization.CultureInfo.InvariantCulture) + ".png";
                
                Image.Save(System.IO.Path.Combine(Path, Entry), System.Drawing.Imaging.ImageFormat.Png);
                SaveExpireDateTime(Path, Entry, ExpireDateTime);
            }
        }

        private void SaveExpireDateTime(System.String Path, System.String Entry, System.DateTime ExpireDateTime)
        {
            LockDatabase(Path);

            var DatabasePath = System.IO.Path.Combine(Path, "expire.db");
            var Lines = new System.Collections.Generic.List<System.String>();
            var Exists = false;

            using(var File = new System.IO.FileStream(DatabasePath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read, System.IO.FileShare.None))
            {
                using(var Reader = new System.IO.StreamReader(File))
                {
                    System.String Line;

                    while((Line = Reader.ReadLine()) != null)
                    {
                        var Parts = Line.Split('=');

                        if(Parts.Length == 2)
                        {
                            if(Parts[0] == Entry)
                            {
                                Lines.Add(Entry + "=" + ExpireDateTime.ToString(System.Globalization.CultureInfo.InvariantCulture));
                                Exists = true;
                            }
                            else
                            {
                                Lines.Add(Line);
                            }
                        }
                    }
                }
            }
            if(Exists == false)
            {
                Lines.Add(Entry + "=" + ExpireDateTime.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            using(var File = new System.IO.FileStream(DatabasePath, System.IO.FileMode.Truncate, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                using(var Writer = new System.IO.StreamWriter(File))
                {
                    foreach(var Line in Lines)
                    {
                        Writer.WriteLine(Line);
                    }
                }
            }
            ReleaseDatabase(Path);
        }
    }
}
