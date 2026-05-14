using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public class ModificarCaratulaExpedienteUseCase (IExpedienteRepository expedienteRepository, IAutorizacionService autorizacionService)
{
    public ModificarCaratulaExpedienteResponse Ejecutar(ModificarCaratulaExpedienteRequest modificarCaratulaExpedienteRequest)
    {
        if (!autorizacionService.PoseeElPermiso(modificarCaratulaExpedienteRequest.idUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no posee los permisos suficientes");
        }
        var expediente = expedienteRepository.ObtenerPorId(modificarCaratulaExpedienteRequest.idExpediente) ?? throw new NoExisteLaEntidadException("No se encontro el expediente");
        var nuevacaratula = new CaratulaExpediente(modificarCaratulaExpedienteRequest.Caratula);
        expediente.ModificarCaratula(nuevacaratula,modificarCaratulaExpedienteRequest.idUsuario);
        expedienteRepository.Modificar(expediente);

        return new ModificarCaratulaExpedienteResponse(expediente.Id,expediente.FechaUltimaModificacion,expediente.Caratula.Valor);
    }
}
