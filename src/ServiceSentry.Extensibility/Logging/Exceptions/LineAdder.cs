using System.Collections.Generic;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace ServiceSentry.Extensibility.Logging.Exceptions
{
    internal abstract class LineAdder
    {
        /// <summary>
        ///     Creates a new instance of the class, using the default values.
        /// </summary>
        public static LineAdder Default => new LineAdderImplementation();

        /// <summary>
        ///     Adds the string to the list of strings, broken across line-breaks.
        /// </summary>
        /// <param name="strings">The list of strings.</param>
        /// <param name="message">The string to add to the list.</param>
        internal abstract void AddLines(List<string> strings, string message);

        private sealed class LineAdderImplementation : LineAdder
        {
            internal override void AddLines(List<string> strings, string message)
            {
                var lines = message.Split('\n');

                strings.Add(lines[0].Trim('\r'));

                strings.AddRange(lines.Skip(1).Select(line => line.Trim('\r')));
            }
        }
    }
}