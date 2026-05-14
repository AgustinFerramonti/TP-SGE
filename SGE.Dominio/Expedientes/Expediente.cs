using System;
using SGE.Dominio.Comun;
using SGE.Dominio.Tramites;

namespace SGE.Dominio.Expedientes;

public class Expediente
{
    public Guid Id {get; private set;}
    public CaratulaExpediente Caratula {get; private set;}
    public DateTime FechaCreacion {get;private set;}
    public DateTime FechaUltimaModificacion {get; private set;}
    public Guid UsuarioUltimoCambio {get; private set;}
    public EstadoExpediente Estado {get; private set;}

    private Expediente (Guid id, CaratulaExpediente caratula, DateTime fechaCreacion,DateTime fechaUltimaModificacion,Guid Usuario, EstadoExpediente estado)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("¡El id no puede estar vacio!");
        }
        if (fechaUltimaModificacion < fechaCreacion) 
        {
            throw new DomainException("¡La Fecha de modificacion no puede ser menor a la fecha de creacion!"); 
        }

        Caratula = caratula ?? throw new DomainException("¡La caratula no puede estar vacia!");
        Id = id;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
        UsuarioUltimoCambio = Usuario;
        Estado = estado;

       
    }

    public Expediente (CaratulaExpediente caratula, Guid Usuario) : this(Guid.NewGuid(),caratula,DateTime.Now,DateTime.Now,Usuario,EstadoExpediente.RecienIniciado)
    {}
    public static Expediente reconstruir (Guid id,CaratulaExpediente caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid Usuario, EstadoExpediente estado) 
    {
        return new Expediente(id,caratula,fechaCreacion,fechaUltimaModificacion,Usuario,estado);
    }

    public void ModificarCaratula (CaratulaExpediente caratulaNueva, Guid Usuario)
    {
        Caratula = caratulaNueva;
        UsuarioUltimoCambio = Usuario;
        FechaUltimaModificacion = DateTime.Now;
    }
    
    public bool ActualizarEstado(EtiquetaTramite? ultimaEtiqueta, Guid Usuario)
    {
            EstadoExpediente estado_anterior = Estado; 
            switch (ultimaEtiqueta)
        {
            case EtiquetaTramite.PaseAEstudio:  Estado = EstadoExpediente.ParaResolver; break;
            case EtiquetaTramite.Resolucion: Estado = EstadoExpediente.ConResolucion; break;
            case EtiquetaTramite.PaseAArchivo : Estado = EstadoExpediente.Finalizado; break;
            case null: Estado = EstadoExpediente.RecienIniciado; break;
            
        }
        if (Estado != estado_anterior)
        {
            UsuarioUltimoCambio = Usuario;
            FechaUltimaModificacion = DateTime.Now;
            return true;
        }
        return false;
    }
    
    public void CambiarEstado (EstadoExpediente nuevoEstado, Guid IdUsuario)
    {
        Estado = nuevoEstado;
        UsuarioUltimoCambio = IdUsuario;
        FechaUltimaModificacion = DateTime.Now;
    }

    
}
