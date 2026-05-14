using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public record class CambiarEstadoExpedienteRequest (Guid idExpediente, Guid idUsuario, EstadoExpediente estado)
{

}
