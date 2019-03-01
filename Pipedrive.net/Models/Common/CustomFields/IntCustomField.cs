namespace Pipedrive.CustomFields
{
    // int
    public class IntCustomField : ICustomField
    {
        public int Value { get; set; }

        public IntCustomField(int value)
        {
            Value = value;
        }
    }
    // DONNNNNNN
    public class LongCustomField : ICustomField
    {
        public long Value { get; set; }

        public LongCustomField(long value)
        {
            Value = value;
        }
    }
}
