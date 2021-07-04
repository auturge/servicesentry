using System;
using System.Collections.Generic;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static Exception RandomException()
        {
            var types = new List<Type>
                {
                    typeof (Exception),
                    typeof (AccessViolationException),
                    typeof (AggregateException),
                    typeof (ApplicationException),
                    typeof (ArgumentException),
                    typeof (ArgumentNullException),
                    typeof (ArgumentOutOfRangeException),
                    typeof (ArrayTypeMismatchException),
                    typeof (InvalidOperationException),
                    typeof (InvalidTimeZoneException),
                };

            var type = types[Randomizer.Next(types.Count)];
            var exception = (Exception) Activator.CreateInstance(type);
            return exception;
        }
    }
}