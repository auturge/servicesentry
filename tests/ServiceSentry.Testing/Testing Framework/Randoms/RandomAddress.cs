namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static string RandomAddress()
        {
            var output = "";

            // Local-part
            //=================================================================
            // Quoted Pair?
            var isLocalQuoted = Random<bool>();
            if (isLocalQuoted)
            {
                output += "\"";

                // Add some random crap, can include all of US ascii except " and \
                for (var atom = 0; atom < Randomizer.Next(1, 4); atom++)
                {
                    // Generate some random crap, with restrictions 
                    output += RandomEmailString(Tests.AtomChars, Randomizer.Next(8));
                    output += ".";
                }

                output += "\"";
            }
            else
            {
                for (var atom = 0; atom < Randomizer.Next(1, 3); atom++)
                {
                    // Generate some random crap, with restrictions 
                    output += RandomEmailString(Tests.AlphaNumeric, Randomizer.Next(1, 12));
                    output += ".";
                }
                output += RandomEmailString(Tests.AlphaNumeric, Randomizer.Next(1, 12));
            }


            // Separator
            //=================================================================
            output += "@";


            // Domain-part
            //=================================================================
            for (var subdomain = 0; subdomain < Randomizer.Next(1, 4); subdomain++)
            {
                // Generate some random crap, with restrictions 
                output += RandomEmailString(Tests.AlphaNumeric, Randomizer.Next(1, 12));
                output += ".";
            }

            // top-level domain
            //=================================================================
            output += RandomEmailString(Tests.Alpha, Randomizer.Next(2, 6));


            return output;
        }


        private static string RandomEmailString(string charset, int size)
        {
            var buffer = new char[size];

            for (var i = 0; i < size; i++)
            {
                buffer[i] = charset[Randomizer.Next(charset.Length)];
            }
            return new string(buffer);
        }
    }
}