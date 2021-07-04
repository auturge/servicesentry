using System;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static int GetNewInteger(int oldValue)
        {
            var newValue = (new Random()).Next(10000);
            if (newValue == oldValue)
            {
                if (newValue == 0)
                {
                    newValue = 1;
                }
                else
                {
                    newValue = -newValue;
                }
            }
            return newValue;
        }
    }
}