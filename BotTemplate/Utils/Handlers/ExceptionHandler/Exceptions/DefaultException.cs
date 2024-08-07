using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BotTemplate.Utils.Handlers.ExceptionHandler.Exceptions;
internal class DefaultException : Exception
{
    public DefaultException()
    {
    }

    public DefaultException(string? message) : base(message)
    {
    }

    public DefaultException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DefaultException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
