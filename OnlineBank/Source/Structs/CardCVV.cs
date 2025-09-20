using System.Text.RegularExpressions;

namespace OnlineBank.Source.Structs
{
    public readonly struct CardCVV
    {
        public string Value { get; }

        public CardCVV(string value)
        {
            if (!Regex.IsMatch(value, @"^\d{3}$"))
                throw new Exception("Wrong CVV format!");
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
