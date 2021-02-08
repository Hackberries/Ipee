using System;

namespace Ipee.Core.Exceptions
{
    class EntryNotFoundException : Exception
    {
        public string ExceptionText { get; set; }

        public EntryNotFoundException(string text)
        {
            ExceptionText = text;
        }
    }
}
