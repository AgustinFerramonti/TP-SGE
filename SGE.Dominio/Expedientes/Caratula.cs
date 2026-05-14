using System;
using SGE.Dominio.Comun;

namespace SGE.Dominio.Expedientes;

public record class CaratulaExpediente
{
    public String Valor {get;} 

    public CaratulaExpediente (String valor)
    {
        if (String.IsNullOrWhiteSpace(valor)) // VERIFICA QUE NO SEA NULL NI " " 
        {
            throw new DomainException("¡La Caratula no puede estar vacia!"); // LANZA EXCEPTION
        }
        Valor = valor; // si pasa, asigna valor
    }
}
