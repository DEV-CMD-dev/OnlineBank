using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineBank.Src.Structs;

namespace OnlineBank.Src.Classes
{
    public class CardCVVConverter : ValueConverter<CardCVV, string>
    {
        public CardCVVConverter() : base(
            v => v.Value,
            v => new CardCVV(v))
        { }
    }
}
