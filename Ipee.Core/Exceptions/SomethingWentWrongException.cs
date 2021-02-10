using System;

namespace Ipee.Core.Exceptions
{
    internal class SomethingWentWrongException : Exception
    {
        public SomethingWentWrongException(string message) : base(message)
        {
        }
    }
}