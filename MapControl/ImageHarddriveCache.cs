using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;

namespace System
{
    public class ImageHarddriveCache
    {
        public String RootDirectory
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

        private readonly Object _LocksLock;
        private readonly Dictionary<String, Object> _Locks;
        private String _RootDirectory;

        public ImageHarddriveCache()
        {
            _LocksLock = new Object();
            _Locks = new Dictionary<String, Object>();
            _RootDirectory = null;
        }
		
		public String GetEntryPath(Int32 Zoom, Int32 X, Int32 Y)
		{
            if(_RootDirectory != null)
            {
				return Path.Combine(Path.Combine(Path.Combine(_RootDirectory, Zoom.ToString(CultureInfo.InvariantCulture)), X.ToString(CultureInfo.InvariantCulture)), Y.ToString(CultureInfo.InvariantCulture) + ".png");
            }
			
			return null;
		}

        public Image LoadTileImage(Int32 Zoom, Int32 X, Int32 Y)
        {
			var EntryPath = GetEntryPath(Zoom, X, Y);
			
			if((EntryPath != null) && (File.Exists(EntryPath) == true))
			{
				return new Bitmap(EntryPath);
			}
			else
			{
				return null;
			}
        }

        private void LockDatabase(String Path)
        {
            Object Lock;

            lock(_LocksLock)
            {
                if(_Locks.ContainsKey(Path) == false)
                {
                    Lock = new Object();
                    _Locks[Path] = Lock;
                }
                else
                {
                    Lock = _Locks[Path];
                }
            }
            Monitor.Enter(Lock);
        }

        private void ReleaseDatabase(String Path)
        {
            Object Lock = null;

            lock(_LocksLock)
            {
                if(_Locks.ContainsKey(Path) == true)
                {
                    Lock = _Locks[Path];
                }
            }
            if(Lock != null)
            {
                Monitor.Exit(Lock);
            }
        }
		
		public Boolean IsExpired(Int32 Zoom, Int32 X, Int32 Y)
		{
			var Path = GetEntryPath(Zoom, X, Y);
			
			return IsExpired(Path);
		}
		
		public Boolean IsExpired(String EntryPath)
		{
			return IsExpired(Path.GetDirectoryName(EntryPath), Path.GetFileName(EntryPath));
		}

        public Boolean IsExpired(String DirectoryPath, String Entry)
        {
            LockDatabase(DirectoryPath);

            var Result = true;
            var DatabasePath = Path.Combine(DirectoryPath, "expire.db");

            if(File.Exists(DatabasePath) == true)
            {
                using(var FileStream = new FileStream(DatabasePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    using(var Reader = new StreamReader(FileStream))
                    {
                        String Line;

                        while((Line = Reader.ReadLine()) != null)
                        {
                            var Parts = Line.Split('=');

                            if(Parts.Length == 2)
                            {
                                if(Parts[0] == Entry)
                                {
                                    Result = DateTime.Parse(Parts[1], CultureInfo.InvariantCulture) < DateTime.Now;
                                }
                            }
                        }
                    }
                }
            }
            ReleaseDatabase(DirectoryPath);

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
