using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class AgregarTramiteUseCase (ITramiteRepository tramiteRepository, IExpedienteRepository expedienteRepository, IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService actualizacionEstadoExpedienteService)
{
    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest agregarTramiteRequest)
    {
        if (!autorizacionService.PoseeElPermiso(agregarTramiteRequest.idUsuario, Permiso.TramiteAlta))
        {
            throw new AutorizacionException("El usuario no tiene permisos suficientes");
        }

        var expediente = expedienteRepository.ObtenerPorId(agregarTramiteRequest.idExpediente) ?? throw new NoExisteLaEntidadException("No existe una entidad con ese id");
        var contenido = new ContenidoTramite(agregarTramiteRequest.Contenido);
        var tramite = new Tramite(expediente.Id,contenido, agregarTramiteRequest.etiqueta, agregarTramiteRequest.idUsuario);

        tramiteRepository.Agregar(tramite);
        actualizacionEstadoExpedienteService.Ejecutar(tramite.ExpedienteId, tramite.UsuarioUltimoCambio);

        return new AgregarTramiteResponse(tramite.Id,tramite.Etiqueta,tramite.FechaCreacion,tramite.Contenido._valor,tramite.ExpedienteId);
    }
}
