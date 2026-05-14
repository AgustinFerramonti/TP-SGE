using System;
using System.ComponentModel.DataAnnotations;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;

namespace SGE.Infraestructura.Expedientes;

public class ExpedienteTxtRepository : IExpedienteRepository
{
    private readonly string _archivo = "archivoExpedientes.txt";
    public void Agregar(Expediente expediente)
    {
        using var sw = new StreamWriter(_archivo, true);
        sw.WriteLine(expediente.Id);
        sw.WriteLine(expediente.Caratula);
        sw.WriteLine(expediente.Estado);
        sw.WriteLine(expediente.FechaCreacion);
        sw.WriteLine(expediente.FechaUltimaModificacion);
        sw.WriteLine(expediente.UsuarioUltimoCambio);
    }

    public void Modificar(Expediente expediente)
    {
        var expedientes = this.ObtenerTodos();
        using var sw = new StreamWriter(_archivo, false);
        bool encontrado = false;
        foreach (var e in expedientes)
        {

            if (e.Id == expediente.Id)
            {
                encontrado = true;
                sw.WriteLine(expediente.Id);
                sw.WriteLine(expediente.Caratula.Valor);
                sw.WriteLine(expediente.FechaCreacion);
                sw.WriteLine(expediente.FechaUltimaModificacion);
                sw.WriteLine(expediente.UsuarioUltimoCambio);
                sw.WriteLine(expediente.Estado);
            }
            else
            {
                sw.WriteLine(e.Id);
                sw.WriteLine(e.Caratula.Valor);
                sw.WriteLine(e.FechaCreacion);
                sw.WriteLine(e.FechaUltimaModificacion);
                sw.WriteLine(e.UsuarioUltimoCambio);
                sw.WriteLine(e.Estado);
            }
        }
        if (!encontrado)
        {
            throw new RepositoryException("Expediente no encontrado");
        }
    }
    

    public void Eliminar(Guid idExpediente)
    {
        var expedientes = this.ObtenerTodos();
        using var sw = new StreamWriter(_archivo, false);
        bool encontrado = false;
        foreach (var e in expedientes)
        {
            if (e.Id == idExpediente)
            {
                encontrado = true;
            }
            else
            {
                sw.WriteLine(e.Id);
                sw.WriteLine(e.Caratula.Valor);
                sw.WriteLine(e.FechaCreacion);
                sw.WriteLine(e.FechaUltimaModificacion);
                sw.WriteLine(e.UsuarioUltimoCambio);
                sw.WriteLine(e.Estado);
            }
        }
        if (!encontrado)
        {
            throw new RepositoryException("Expediente no encontrado");
        }
    }

    public IEnumerable<Expediente> ObtenerTodos()
    {
        var resultado = new List<Expediente>();
        if (!File.Exists(_archivo))
        {
            return resultado;
        }
        using var sr = new StreamReader(_archivo);
        while (!sr.EndOfStream)
        {
            var idStr = sr.ReadLine() ?? "";
            var caratulaStr = sr.ReadLine() ?? "";
            var estadoStr = sr.ReadLine() ?? "";
            var fechaCreacionStr = sr.ReadLine() ?? "";
            var fechaModificacionStr = sr.ReadLine() ?? "";
            var usuarioCambioStr = sr.ReadLine() ?? "";

            var id = Guid.Parse(idStr);
            var caratulaVO = new CaratulaExpediente(caratulaStr);
            var fechaCreacion = DateTime.Parse(fechaCreacionStr);
            var fechaModificacion = DateTime.Parse(fechaModificacionStr);
            var usuario = Guid.Parse(usuarioCambioStr);
            var estado = Enum.Parse<EstadoExpediente>(estadoStr);

            var expediente = Expediente.reconstruir(id,caratulaVO,fechaCreacion,fechaModificacion,usuario,estado); 

            resultado.Add(expediente);
        }
        return resultado;
    }

    public Expediente? ObtenerPorId(Guid idExpediente)
    {
        var expedientes = this.ObtenerTodos();
        foreach (var e in expedientes)
        {
            if (e.Id == idExpediente)
            {
                return e;
            }
        }
        return null;
    }
}
