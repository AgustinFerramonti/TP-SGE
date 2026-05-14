using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public class CambiarEstadoExpedienteUseCase (IExpedienteRepository expedienteRepository, IAutorizacionService autorizacionService)
{
    public CambiarEstadoExpedienteResponse Ejecutar(CambiarEstadoExpedienteRequest cambiarEstadoExpedienteRequest)
    {
        if (!autorizacionService.PoseeElPermiso(cambiarEstadoExpedienteRequest.idUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no posee los permisos suficientes");
        }

        
        var expediente = expedienteRepository.ObtenerPorId(cambiarEstadoExpedienteRequest.idExpediente) ?? throw new NoExisteLaEntidadException("No se encontro el expediente");
        EstadoExpediente estadoExpediente = cambiarEstadoExpedienteRequest.estado;

        expediente.CambiarEstado(estadoExpediente,cambiarEstadoExpedienteRequest.idUsuario);
        expedienteRepository.Modificar(expediente);

        return new CambiarEstadoExpedienteResponse(expediente.Id,expediente.FechaUltimaModificacion,expediente.Estado);
    }
}
