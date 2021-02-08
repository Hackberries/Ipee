using System;

namespace Ipee.Core.Exceptions
{
    internal class SomethingWentWrongException : Exception
    {
        public string ExceptionText { get; set; }

        public SomethingWentWrongException(string text)
        {
            ExceptionText = text;
        }
    }
}
