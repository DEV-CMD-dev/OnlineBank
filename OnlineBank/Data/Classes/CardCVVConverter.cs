using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineBank.Data.Structs;

namespace OnlineBank.Data.Classes
{
    public class CardCVVConverter : ValueConverter<CardCVV, string>
    {
        public CardCVVConverter() : base(
            v => v.Value,
            v => new CardCVV(v))
        { }
    }
}
