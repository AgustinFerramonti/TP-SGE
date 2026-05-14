using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public record class CambiarEstadoExpedienteResponse (Guid idExpediente, DateTime fechaModificacion, EstadoExpediente estado)
{

}
