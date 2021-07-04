using System.Linq;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        private static readonly string[] ParamSymbols = { " ", "=", ":" };
        private static readonly string[] SwitchSymbols = { "-", "/", "--" };
        private static readonly string[] QuoteSymbols = { "\"", "'" };

        public static string RandomParamSymbol()
        {
            return ParamSymbols[Randomizer.Next(0, ParamSymbols.Count())];
        }

        public static string RandomSwitch()
        {
            return SwitchSymbols[Randomizer.Next(0, SwitchSymbols.Count())];
        }

        public static string RandomQuote()
        {
            return QuoteSymbols[Randomizer.Next(0, QuoteSymbols.Count())];
        }
    }
}