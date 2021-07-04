using System;
using System.Windows.Threading;
using NUnit.Framework;
using ServiceSentry.Extensibility.Extensions;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Extensibility.UnitTests.Extensions
{
    [TestFixture]
    internal class TimerExtensionTests
    {
        internal class FakeTimerExtension : TimerExtension
        {
            public override string ExtensionName => throw new NotImplementedException();

            public override DispatcherTimer Timer(Logger logger)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void Test_Defaults()
        {
            // Arrange
            var expected = TimeSpan.FromSeconds(.5);

            // Act
            var pdq = new FakeTimerExtension();

            // Assert
            Assert.That(pdq != null);
            Assert.IsFalse(pdq.CanExecute);
        }
    }
}