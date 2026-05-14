using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;
public class AgregarExpedienteUseCase (IExpedienteRepository expedienteRepository,IAutorizacionService autorizacion)
{
    public AgregarExpedienteResponse Ejecutar(AgregarExpedienteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteAlta))
        {
            throw new AutorizacionException("El Usuario no tiene los permisos suficientes");
        }

        var caratula = new CaratulaExpediente(request.Caratula);
        var expediente = new Expediente (caratula, request.IdUsuario);

        expedienteRepository.Agregar(expediente);

        return new AgregarExpedienteResponse(expediente.Id,expediente.Caratula.Valor,expediente.FechaCreacion,expediente.Estado);
    }
}