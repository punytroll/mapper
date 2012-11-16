using System.Linq;

namespace Records
{
    public class Records : System.Collections.Generic.IEnumerable<Record>
    {
        public System.Int32 Count
        {
            get
            {
                return _Records.Count;
            }
        }

        public Record First
        {
            get
            {
                return _Records.FirstOrDefault();
            }
        }

        public Record Last
        {
            get
            {
                return _Records.LastOrDefault();
            }
        }

        private readonly System.Collections.Generic.List<Record> _Records;

        public Records()
        {
            _Records = new System.Collections.Generic.List<Record>();
        }

        public void Append(Record Record)
        {
            _Records.Add(Record);
        }

        public void Map(System.Action<Record> Action)
        {
            foreach(var Record in _Records)
            {
                Action(Record);
            }
        }

        public void Map(System.Action<Record, Record> Action)
        {
            if(_Records.Count > 1)
            {
                for(var Index = 1; Index < _Records.Count; ++Index)
                {
                    Action(_Records[Index - 1], _Records[Index]);
                }
            }
        }

        public AggregateType Fold<AggregateType>(AggregateType Seed, System.Func<AggregateType, Record, AggregateType> Action)
        {
            return _Records.Aggregate(Seed, Action);
        }

        public System.Collections.Generic.IEnumerator<Record> GetEnumerator()
        {
            return _Records.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
