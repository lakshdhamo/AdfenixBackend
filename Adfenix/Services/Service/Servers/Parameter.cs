using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfenix.Services.Service.Servers
{
    public abstract class Parameter
    {
        private string name;
        public string GetName()
        { return name; }
        public Parameter(string name)
        { this.name = name; }
    }

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
