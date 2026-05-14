using System;
using Microsoft.Win32.SafeHandles;
using SGE.Aplicacion;
using SGE.Dominio.Tramites;


namespace SGE.Infraestructura.Tramites;

public class TramiteTxtRepository : ITramiteRepository
{
    private readonly string _archivo = "archivoTramites.txt";
    public void Agregar(Tramite tramite)
    {
        using var sw = new StreamWriter(_archivo, true);
        sw.WriteLine(tramite.Id);
        sw.WriteLine(tramite.FechaUltimaModificacion);
        sw.WriteLine(tramite.FechaCreacion);
        sw.WriteLine(tramite.ExpedienteId);
        sw.WriteLine(tramite.Contenido._valor);
        sw.WriteLine(tramite.Etiqueta);
        sw.WriteLine(tramite.UsuarioUltimoCambio);
    }
    public Tramite? ObtenerPorId(Guid idTramite)
    {
        var tramites = this.ObtenerTodos();
        foreach (Tramite t in tramites)
        {
            if (t.Id == idTramite)
            {
                return t;
            }
        }
        return null;
    }

    public void Modificar (Tramite tramite)
    {
        var tramites = this.ObtenerTodos();
        using var sw = new StreamWriter(_archivo,false);
        bool encontrado = false;
        foreach (Tramite t in tramites)
        {
            if (t.Id == tramite.Id)
            {
                encontrado = true;
                sw.WriteLine(tramite.Id);
                sw.WriteLine(tramite.FechaUltimaModificacion);
                sw.WriteLine(tramite.FechaCreacion);
                sw.WriteLine(tramite.ExpedienteId);
                sw.WriteLine(tramite.Contenido._valor);
                sw.WriteLine(tramite.Etiqueta);
                sw.WriteLine(tramite.UsuarioUltimoCambio);
            }
            else{
                sw.WriteLine(t.Id);
                sw.WriteLine(t.FechaUltimaModificacion);
                sw.WriteLine(t.FechaCreacion);
                sw.WriteLine(t.ExpedienteId);
                sw.WriteLine(t.Contenido);
                sw.WriteLine(t.Etiqueta);
                sw.WriteLine(t.UsuarioUltimoCambio);
            }
        }
        if (!encontrado)
        {
            throw new RepositoryException("El tramite no existe");
        }
    }
    public void Eliminar (Guid idTramite)
    {
        var tramites = this.ObtenerTodos();
        bool encontrado = false;
        using var sw = new StreamWriter(_archivo,false);
        foreach (Tramite t in tramites)
        {
            if (t.Id == idTramite)
            {
                encontrado = true;
            }
            else
            {
                sw.WriteLine(t.Id);
                sw.WriteLine(t.FechaUltimaModificacion);
                sw.WriteLine(t.FechaCreacion);
                sw.WriteLine(t.ExpedienteId);
                sw.WriteLine(t.Contenido);
                sw.WriteLine(t.Etiqueta);
                sw.WriteLine(t.UsuarioUltimoCambio);
            }
        }
        if (!encontrado)
        {
            throw new RepositoryException("El tramite no existe");
        } 
    }

    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid IdExpediente)
    {
        var tramites = this.ObtenerTodos();
        var nuevalista = new List<Tramite>();

        if (!File.Exists(_archivo))
        {
            return nuevalista;
        }

        foreach (Tramite t in tramites)
        {
            if (t.ExpedienteId == IdExpediente)
            {
                nuevalista.Add(t);
            }
        }
        return nuevalista;
    }

    public void EliminarPorExpedienteId(Guid idExpediente)
    {
        var tramites = this.ObtenerTodos();
        using var sw = new StreamWriter(_archivo,false);
        bool encontrado = false;

        foreach (Tramite t in tramites)
        {
            if (t.ExpedienteId == idExpediente)
            {
                encontrado = true;
            }
            else
            {
                sw.WriteLine(t.Id);
                sw.WriteLine(t.FechaUltimaModificacion);
                sw.WriteLine(t.FechaCreacion);
                sw.WriteLine(t.ExpedienteId);
                sw.WriteLine(t.Contenido._valor);
                sw.WriteLine(t.Etiqueta);
                sw.WriteLine(t.UsuarioUltimoCambio);
            }
        }
        if (!encontrado)
        {
            throw new RepositoryException("No existe este expediente");
        }
    }   

    public IEnumerable<Tramite> ObtenerTodos()
    {
        var listaTramites = new List<Tramite>();

        if (!File.Exists(_archivo))
        {
            return listaTramites;
        }
        using var sr = new StreamReader (_archivo);

        while (!sr.EndOfStream)
        {
            var idSr = sr.ReadLine() ?? "";
            var fechaUltModSr = sr.ReadLine() ?? "";
            var fechaCreacionSr = sr.ReadLine() ?? "";
            var idExpedienteSr = sr.ReadLine() ?? "";
            var contenidoSr = sr.ReadLine() ?? "";
            var etiquetaSr = sr.ReadLine() ?? "";
            var usuarioUltCambioSr = sr.ReadLine() ?? "";

            var id = Guid.Parse(idSr);
            var fechaCreacion = DateTime.Parse(fechaCreacionSr);
            var fechaUltimaModificacion = DateTime.Parse(fechaUltModSr);
            var idExpediente = Guid.Parse(idExpedienteSr);
            var contenidoVO = new ContenidoTramite(contenidoSr);
            var etiquetaEnum = Enum.Parse<EtiquetaTramite>(etiquetaSr);
            var usuarioId = Guid.Parse(usuarioUltCambioSr);

            var tramite = Tramite.reconstruir(id,idExpediente,etiquetaEnum,contenidoVO,fechaCreacion,fechaUltimaModificacion,usuarioId);

            listaTramites.Add(tramite);
        }
        return listaTramites;
    }
}
