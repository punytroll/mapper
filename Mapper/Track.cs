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

        private System.String _Name;

        public Track()
        {
        }
    }
}
