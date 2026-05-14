using System;
using SGE.Dominio.Comun;

namespace SGE.Dominio.Tramites;

public record class ContenidoTramite
{
    public String _valor {get;}

    public ContenidoTramite (String valor)
    {
        if (String.IsNullOrWhiteSpace(valor))
        {
            throw new DomainException("¡El contenido no puede estar vacio!");
        }
        _valor = valor;
    }
}
