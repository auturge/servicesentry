using System;
using System.Collections.Generic;
using Moq.Language.Flow;

namespace ServiceSentry.Testing
{
    public static class MoqExtensions
    {
        public static void ReturnsInOrder<T, TResult>(this ISetup<T, TResult> setup,
                                                      params TResult[] results) where T : class
        {
            setup.Returns(new Queue<TResult>(results).Dequeue);
        }

        public static bool Contains(this string[] array, string stringName)
        {
            var position = Array.IndexOf(array, stringName);
            return position > -1;
        }
    }
}