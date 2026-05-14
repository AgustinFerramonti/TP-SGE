using System;

namespace SGE.Aplicacion;

public class NoExisteLaEntidadException : Exception
{
    public NoExisteLaEntidadException()
    {
        
    }
    public NoExisteLaEntidadException (string? message) : base(message)
    {
        
    }
    public NoExisteLaEntidadException(string? message, Exception? innerException) : base(message, innerException)
    {
        
    }
}
