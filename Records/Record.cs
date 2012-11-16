namespace Records
{
    public class Record
    {
        private readonly System.Collections.Generic.Dictionary<System.String, System.Object> _Fields;

        public Record()
        {
            _Fields = new System.Collections.Generic.Dictionary<System.String, System.Object>();
        }

        public void Add<FieldType>(System.String FieldName, FieldType FieldValue)
        {
            if(_Fields.ContainsKey(FieldName) == false)
            {
                _Fields.Add(FieldName, FieldValue);
            }
            else
            {
                throw new System.ArgumentException("Fields '" + FieldName + "' already exists.");
            }
        }

        public FieldType Get<FieldType>(System.String FieldName)
        {
            if(_Fields.ContainsKey(FieldName) == true)
            {
                if(_Fields[FieldName].GetType() == typeof(FieldType))
                {
                    return (FieldType)_Fields[FieldName];
                }
                else
                {
                    throw new System.InvalidCastException("Field '" + FieldName + "' does not contain a value of type '" + typeof(FieldType).FullName + "'.");
                }
            }
            else
            {
                throw new System.ArgumentException("Field '" + FieldName + "' does not exist.");
            }
        }

        public System.Boolean Has(System.String FieldName)
        {
            return _Fields.ContainsKey(FieldName);
        }

        public void Remove(System.String FieldName)
        {
            if(_Fields.ContainsKey(FieldName) == true)
            {
                _Fields.Remove(FieldName);
            }
            else
            {
                throw new System.ArgumentException("Field '" + FieldName + "' does not exist.");
            }
        }

        public void Rename(System.String OldFieldName, System.String NewFieldName)
        {
            if(_Fields.ContainsKey(OldFieldName) == true)
            {
                if(_Fields.ContainsKey(NewFieldName) == false)
                {
                    var FieldValue = _Fields[OldFieldName];

                    _Fields.Remove(OldFieldName);
                    _Fields.Add(NewFieldName, FieldValue);
                }
                else
                {
                    throw new System.ArgumentException("Fields '" + NewFieldName + "' already exists.");
                }
            }
            else
            {
                throw new System.ArgumentException("Field '" + OldFieldName + "' does not exist.");
            }
        }

        public void Replace<FieldType>(System.String FieldName, FieldType FieldValue)
        {
            if(_Fields.ContainsKey(FieldName) == true)
            {
                _Fields[FieldName] = FieldValue;
            }
            else
            {
                throw new System.ArgumentException("Field '" + FieldName + "' does not exist.");
            }
        }

        public void Update<FieldType>(System.String FieldName, FieldType FieldValue)
        {
            if(_Fields.ContainsKey(FieldName) == true)
            {
                if(typeof(FieldType) == _Fields[FieldName].GetType())
                {
                    _Fields[FieldName] = FieldValue;
                }
                else
                {
                    throw new System.ArgumentException("Field '" + FieldName + "' has not the type '" + typeof(FieldType).FullName + "'.");
                }
            }
            else
            {
                throw new System.ArgumentException("Field '" + FieldName + "' does not exist.");
            }
        }
    }
}
