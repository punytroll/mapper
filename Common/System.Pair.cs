namespace System
{
    public class Pair<Type1, Type2>
    {
        public Type1 Value1
        {
            get
            {
                return _Value1;
            }
            set
            {
                _Value1 = value;
            }
        }

        public Type2 Value2
        {
            get
            {
                return _Value2;
            }
            set
            {
                _Value2 = value;
            }
        }

        private Type1 _Value1;
        private Type2 _Value2;

        public Pair(Type1 Value1, Type2 Value2)
        {
            _Value1 = Value1;
            _Value2 = Value2;
        }
    }
}
