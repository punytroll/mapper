namespace System
{
    public struct Point
    {
        private Double _X;
        private Double _Y;

        public Double X
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

        public Double Y
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

        public Point(Double X, Double Y)
        {
            _X = X;
            _Y = Y;
        }
    }
}
