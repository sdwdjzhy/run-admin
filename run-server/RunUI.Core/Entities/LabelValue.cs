namespace RunUI
{

    public class LabelValue<TValue, TLabel>
    {
        public LabelValue()
        { }

        public LabelValue(TValue value, TLabel label)
        {
            Value = value;
            Label = label;
        }

        public TValue Value { get; set; }
        public TLabel Label { get; set; }
    }

    public class LabelValue<TValue> : LabelValue<TValue, string>
    {
        public LabelValue()
        { }

        public LabelValue(TValue key, string text)
        {
            Value = key;
            Label = text;
        }
    }

    public class LabelValue : LabelValue<string, string>
    {
        public LabelValue()
        { }

        public LabelValue(string key, string text)
        {
            Value = key;
            Label = text;
        }
    }
}