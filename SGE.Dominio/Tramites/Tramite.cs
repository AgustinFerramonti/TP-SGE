using System;
using System.Net.Mime;
using SGE.Dominio.Comun;

namespace SGE.Dominio.Tramites;

public class Tramite
{
    public Guid Id {get; private set;}
    public Guid ExpedienteId {get;private set;}
    public EtiquetaTramite Etiqueta {get;private set;}
    public ContenidoTramite Contenido {get;private set;}
    public DateTime FechaCreacion {get; private set;}
    public DateTime FechaUltimaModificacion {get; private set;}
    public Guid UsuarioUltimoCambio {get; private set;}

    private Tramite (Guid id, Guid expedienteid, EtiquetaTramite etiqueta, ContenidoTramite contenido, DateTime fechacreacion, DateTime fechaactualizacion, Guid Usuario)
    {
        if (id == Guid.Empty)
        {
            throw new DomainException("El id no puede estar vacio!");
        }
        if (expedienteid == Guid.Empty)
        {
            throw new DomainException("El id del expediente no puede estar vacio!");
        }
        if (fechacreacion > fechaactualizacion)
        {
            throw new DomainException("La fecha de actualizacion no puede ser menor a la fecha de creacion!");
        }
        Contenido = contenido ?? throw new DomainException("El contenido no puede estar vacio!");
        Id = id; 
        ExpedienteId = expedienteid; 
        Etiqueta = etiqueta; 
        FechaCreacion = fechacreacion; 
        FechaUltimaModificacion = fechaactualizacion;
        UsuarioUltimoCambio = Usuario;
    }
   
    public Tramite(Guid expedienteid,ContenidoTramite contenido,EtiquetaTramite etiqueta, Guid Usuario) : this (Guid.NewGuid(),expedienteid,etiqueta,contenido,DateTime.Now,DateTime.Now,Usuario)
    {}
    public static Tramite reconstruir(Guid id, Guid expedienteid, EtiquetaTramite etiqueta, ContenidoTramite contenido, DateTime fechacreacion, DateTime fechaactualizacion, Guid Usuario)
    {
        return new Tramite (id,expedienteid,etiqueta,contenido,fechacreacion,fechaactualizacion,Usuario);
    }

    public void Modificar(ContenidoTramite contenidoTramite,EtiquetaTramite etiquetaTramite,  Guid usuario)
    {
        Contenido = contenidoTramite; 
        Etiqueta = etiquetaTramite; 
        UsuarioUltimoCambio = usuario; 
        FechaUltimaModificacion = DateTime.Now;
        
    }

    
}
