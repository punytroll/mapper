public static class Extensions
{
    public static void UpdateField<FieldType>(this Records.Records Records, System.String FieldName, System.Func<FieldType, FieldType> Action)
    {
        Records.Map(Record => Record.Update(FieldName, Action(Record.Get<FieldType>(FieldName))));
    }

    public static void AddField<ResultType, InputType1, InputType2>(this Records.Records Records, System.String ResultFieldName, System.String InputFieldName1, System.String InputFieldName2, System.Func<InputType1, InputType2, ResultType> Action)
    {
        Records.Map(Record => Record.Add(ResultFieldName, Action(Record.Get<InputType1>(InputFieldName1), Record.Get<InputType2>(InputFieldName2))));
    }

    public static void AddField<ResultType, InputType>(this Records.Records Records, System.String ResultFieldName, System.String InputFieldName, System.Func<InputType, InputType, ResultType> Action)
    {
        if(Records.Count > 0)
        {
            Records.First.Add(ResultFieldName, default(ResultType));
            Records.Map((Record1, Record2) => Record2.Add(ResultFieldName, Action(Record1.Get<InputType>(InputFieldName), Record2.Get<InputType>(InputFieldName))));
        }
    }

    public static void AddField<ResultType, InputType>(this Records.Records Records, System.String ResultFieldName, System.String InputFieldName, System.Func<InputType, ResultType> Action)
    {
        Records.Map(Record => Record.Add(ResultFieldName, Action(Record.Get<InputType>(InputFieldName))));
    }

    public static AggregateType Aggregate<AggregateType>(this Records.Records Records, System.String InputFileName, System.Func<AggregateType, AggregateType, AggregateType> Aggregator)
    {
        if(Records.Count > 0)
        {
            return Records.Fold(Records.First.Get<AggregateType>(InputFileName), (Value, Record) => Aggregator(Value, Record.Get<AggregateType>(InputFileName)));
        }
        else
        {
            throw new System.InvalidOperationException();
        }
    }
}
