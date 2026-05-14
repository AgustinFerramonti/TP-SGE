using System;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Expedientes;

public class ActualizacionEstadoExpedienteService (IExpedienteRepository expedienteRepository, ITramiteRepository tramiteRepository)
{
    public void Ejecutar(Guid idExpediente, Guid idUsuario)
    {
        var expediente = expedienteRepository.ObtenerPorId(idExpediente) ?? throw new NoExisteLaEntidadException("No existe ese expediente");

        var tramites = tramiteRepository.ObtenerPorExpedienteId(idExpediente);

        Tramite? ultimoTramite = null;
        foreach (var tramite in tramites)
        {
            if (ultimoTramite == null || tramite.FechaCreacion > ultimoTramite.FechaCreacion)
            {
                ultimoTramite = tramite;
            }
        }
        EtiquetaTramite? ultimaEtiqueta = ultimoTramite?.Etiqueta;

        bool cambio = expediente.ActualizarEstado(ultimaEtiqueta,idUsuario);
        if (cambio)
        {
            expedienteRepository.Modificar(expediente);
        }
    }
}
