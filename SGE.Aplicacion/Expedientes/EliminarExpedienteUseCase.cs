using System;
using System.Net.Cache;
using System.Security.Cryptography.X509Certificates;
using SGE.Aplicacion.Autorizacion;

namespace SGE.Aplicacion.Expedientes;

public class EliminarExpedienteUseCase (IExpedienteRepository expedienteRepository, ITramiteRepository tramiteRepository, IAutorizacionService autorizacionService)
{
    public EliminarExpedienteResponse Ejecutar(EliminarExpedienteRequest eliminarExpedienteRequest)
    {
        if (!autorizacionService.PoseeElPermiso(eliminarExpedienteRequest.idUsuario, Permiso.ExpedienteBaja))
        {
            throw new AutorizacionException("El usuario no cuenta con los permisos suficientes ");
        }
        var expediente = expedienteRepository.ObtenerPorId(eliminarExpedienteRequest.idExpediente) ?? throw new NoExisteLaEntidadException("No se encontro el expediente");
        
        var idExpediente = eliminarExpedienteRequest.idExpediente;
        
        tramiteRepository.EliminarPorExpedienteId(idExpediente);
        expedienteRepository.Eliminar(idExpediente);

        return new EliminarExpedienteResponse (eliminarExpedienteRequest.idExpediente);
    }   
}



