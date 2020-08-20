using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.Infra.Assertions
{
    public static class Assert
    {
        public static void IsGreaterThanOrEqualTo0(int value, string name)
        {
            if (value < 0) { throw new ArgumentException($"{name} should be greater than 0 or equal to 0.");}
        }
        
        public static void IsGreaterThan0(int value, string name)
        {
            if (value <= 0) { throw new ArgumentException($"{name} should be greater than 0.");}
        }
        
        public static void IsNotNull(object value, string name)
        {
            if (value is null) { throw new ArgumentNullException(name);}
        }

        public static void IsNotNullOrWhitespace(string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentException($"{name} should not be null or whitespace.");}
        }
    }
}
