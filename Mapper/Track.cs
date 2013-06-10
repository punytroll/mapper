namespace Mapper
{
    public class Track : Records.Records
    {
        public System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public System.Boolean DrawLines
        {
            get
            {
                return _DrawLines;
            }
            set
            {
                _DrawLines = value;
            }
        }

        private System.Boolean _DrawLines;
        private System.String _Name;

        public Track()
        {
        }
    }
}
