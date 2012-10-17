namespace System
{
    public struct Point
    {
        private System.Double _X;
        private System.Double _Y;

        public System.Double X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
            }
        }

        public System.Double Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = value;
            }
        }

        public Point(System.Double X, System.Double Y)
        {
            _X = X;
            _Y = Y;
        }
    }
}
