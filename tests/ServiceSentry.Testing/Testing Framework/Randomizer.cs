using System;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static readonly Random Randomizer = new Random((int) DateTime.Now.Ticks);
    }
}