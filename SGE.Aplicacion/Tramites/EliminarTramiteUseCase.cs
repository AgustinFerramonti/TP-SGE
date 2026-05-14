using System;
using System.Net;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;

namespace SGE.Aplicacion.Tramites;

public class EliminarTramiteUseCase (ITramiteRepository tramiteRepository, IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService actualizacionEstadoExpedienteService)
{
    public EliminarTramiteResponse Ejecutar(EliminarTramiteRequest eliminarTramiteRequest)
    {
        if (!autorizacionService.PoseeElPermiso(eliminarTramiteRequest.idUsuario, Permiso.TramiteBaja))
        {
            throw new AutorizacionException("El usuario no posee los permisos suficientes");
        }

        var tramite = tramiteRepository.ObtenerPorId(eliminarTramiteRequest.idTramite) ?? throw new NoExisteLaEntidadException("El tramite no existe");

        tramiteRepository.Eliminar(tramite.Id);
        actualizacionEstadoExpedienteService.Ejecutar(tramite.ExpedienteId,eliminarTramiteRequest.idUsuario);
        
        return new EliminarTramiteResponse(tramite.Id);
    }
}
