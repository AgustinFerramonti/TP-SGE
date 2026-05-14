using System;

namespace SGE.Infraestructura;

public class RepositoryException : Exception
{
    public RepositoryException (string message) : base(message)
    {
        
    }
}
