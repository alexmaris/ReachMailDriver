using System;

namespace ReachMailDriver.Util
{
    public static class ValidationExtensions
    {
        public static Validation IsNotNull<T>(this Validation validation, T theObject, string paramName)
            where T : class
        {
            if (theObject == null)
                throw new ArgumentNullException(paramName);
            else
                return validation;
        }

        public static Validation IsNotNullOrEmpty(this Validation validation, String theString, string paramName)
        {
            if (String.IsNullOrEmpty(theString))
                throw new ArgumentException(String.Format("{0} can not be null or empty", paramName));

            return validation;
        }

        public static Validation IsNotInPast(this Validation validation, DateTime theDateTime, string paramName)
        {
            if (DateTime.Now > theDateTime)
                throw new Exception(String.Format("{0} DateTime paramater must be in the future.", paramName));

            return validation;
        }
    }

    public static class Validate
    {
        public static Validation Begin()
        {
            return null;
        }
    }

    public sealed class Validation
    {
    }

}
