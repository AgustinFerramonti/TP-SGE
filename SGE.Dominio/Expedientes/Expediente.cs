using System;
using System.Data.Common;
using System.Dynamic;

namespace SGE.Dominio.Expedientes;

public class Expediente
{
    public Guid _Id {get; private set;}
    public Caratula _caratula {get; private set;}
    public DateTime _fechaCreacion {get;private set;}
    public DateTime _fechaUltimaModificacion {get; private set;}
    public Guid _usuarioUltimoCambio {get; private set;}
    public Estado _estado {get; private set;}

    public Expediente (Caratula caratula, Guid usuarioUltimoCambio)
    {
        _Id = Guid.NewGuid();
        _caratula = caratula;
        _fechaCreacion = DateTime.Now;
        _fechaUltimaModificacion = _fechaCreacion;
        _usuarioUltimoCambio = usuarioUltimoCambio;
        _estado = Estado.RecienIniciado;
    }
}
