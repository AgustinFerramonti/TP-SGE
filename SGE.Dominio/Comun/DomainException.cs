using System;

namespace SGE.Dominio.Comun;

public class DomainException : Exception // CLASE SACADA DE LA TEORIA 7
{
    public DomainException()
    {
        
    }
    public DomainException (string? message) : base(message)
    {
        
    }
    public DomainException(string? message, Exception? innerException) : base (message, innerException)
    {
        
    }
}
