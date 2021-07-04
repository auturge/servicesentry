using JetBrains.Annotations;
using NUnit.Framework;

namespace ServiceSentry.Testing
{
    [TestFixture]
    public partial class Tests
    {
        [AssertionMethod]
        public static void TestEquality(object item1, object item2, object item3)
        {
            TestEquals(item1, item2, item3);
            TestEqualityOperator(item1, item2, item3);
        }

        private static void TestEquals(object item1, object item2, object item3)
        {
            // x.Equals(x)  (always true)
            
            // x.Equals(y) == y.Equals(x)
            Assert.That(item1.Equals(item2) == item2.Equals(item1));
            Assert.That(item1.Equals(item3) == item3.Equals(item1));
            Assert.That(item2.Equals(item3) == item3.Equals(item2));

            // x.Equals(null) == false
            Assert.IsFalse(item1.Equals(null));
            Assert.IsFalse(item2.Equals(null));
            Assert.IsFalse(item3.Equals(null));

            // If x.equals(y) and y.equals(z) then x.equals(z)
            if (item1.Equals(item2) && item2.Equals(item3))
                Assert.That(item1.Equals(item3));
        }

        private static void TestEqualityOperator(object item1, object item2, object item3)
        {
            // x.Equals(x)  (always true)

            // x.Equals(y) == y.Equals(x)
            Assert.That((item1 == item2) == (item2 == item1));
            Assert.That((item1 == item3) == (item3 == item1));
            Assert.That((item3 == item2) == (item2 == item3));

            // x.Equals(null) == false
            Assert.IsFalse(item1 == null);
            Assert.IsFalse(item2 == null);
            Assert.IsFalse(item3 == null);

            // If x.equals(y) and y.equals(z) then x.equals(z)
            if (item1 == item2 && item2 == item3)
                Assert.That(item1 == item3);
        }
    }
}