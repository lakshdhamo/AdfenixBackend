namespace Adfenix.Services.Service.Servers
{
    /// <summary>
    /// Parent Parameter class
    /// </summary>
    public abstract class Parameter
    {
        private string name;
        public string GetName()
        { return name; }
        public Parameter(string name)
        { this.name = name; }
    }

    /// <summary>
    /// Boolean parameter class
    /// </summary>
    public class BoolParameter : Parameter
    {
        private bool Value;
        public bool GetValue()
        { return Value; }
        public void SetValue(bool value)
        { Value = value; }
        public BoolParameter(string name, bool defaultvalue)
        : base(name)
        {
            Value = defaultvalue;
        }
    }

    /// <summary>
    /// String parameter class
    /// </summary>
    public class StrParameter : Parameter
    {
        private string Value;
        public string GetValue()
        { return Value; }
        public void SetValue(string value)
        { Value = value; }
        public StrParameter(string name, string defaultvalue)
        : base(name)
        {
            Value = defaultvalue;
        }
    }

    /// <summary>
    /// Int parameter class
    /// </summary>
    public class IntParameter : Parameter
    {
        private int min;
        private int max;
        private int Value;

        public int GetValue()
        { return Value; }
        public void SetValue(int value)
        {
            if (value < min)
                throw new ArgumentOutOfRangeException(GetName() + " can’t be less than " + min);
            if (value > max)
                throw new ArgumentOutOfRangeException(GetName() + " can’t be greater than " + max);
            Value = value;
        }
        public IntParameter(string name, int min, int max, int defaultvalue) : base(name)
        {
            this.min = min;
            this.max = max;
            Value = defaultvalue;
        }
    }

    /// <summary>
    /// Double parameter class
    /// </summary>
    public class DoubleParameter : Parameter
    {
        private double min;
        private double max;

        private double Value;
        public double GetValue()
        { return Value; }
        public void SetValue(double value)
        {
            if (value < min)
                throw new ArgumentOutOfRangeException(GetName() + " can’t be less than " + min);
            if (value > max)
                throw new ArgumentOutOfRangeException(GetName() + " can’t be greater than " + max);
            Value = value;
        }
        public DoubleParameter(string name, double min, double max, double defaultvalue)
        : base(name)
        {
            this.min = min;
            this.max = max;
            Value = defaultvalue;
        }
    }




}
