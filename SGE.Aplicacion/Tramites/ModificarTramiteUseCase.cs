using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ModificarTramiteUseCase (ITramiteRepository tramiteRepository, IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService actualizacionEstadoExpedienteService)
{
    public ModificarTramiteResponse Ejecutar(ModificarTramiteRequest modificarTramiteRequest)
    {
        if (!autorizacionService.PoseeElPermiso(modificarTramiteRequest.idUsuario, Permiso.TramiteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene los permisos suficientes");
        }

        var nuevocontenido = new ContenidoTramite(modificarTramiteRequest.contenido);
        var nuevaetiqueta = modificarTramiteRequest.etiquetaTramite;
        var tramite = tramiteRepository.ObtenerPorId(modificarTramiteRequest.idTramite);

        if (tramite == null)
        {
            throw new NoExisteLaEntidadException("No existe el tramite");
        }
        tramite.Modificar(nuevocontenido,nuevaetiqueta, modificarTramiteRequest.idUsuario);
        tramiteRepository.Modificar(tramite);
        actualizacionEstadoExpedienteService.Ejecutar(tramite.ExpedienteId, modificarTramiteRequest.idUsuario);

        return new ModificarTramiteResponse (tramite.Id,tramite.ExpedienteId,tramite.Etiqueta,tramite.Contenido._valor,tramite.FechaUltimaModificacion);
    }
}
