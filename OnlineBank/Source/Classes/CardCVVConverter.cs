using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineBank.Source.Structs;

namespace OnlineBank.Source.Classes
{
    public class CardCVVConverter : ValueConverter<CardCVV, string>
    {
        public CardCVVConverter() : base(
            v => v.Value,
            v => new CardCVV(v))
        { }
    }
}
