using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public record class ListarTodosLosExpedientesResponse (Guid idExpediente, string CaratulaExpediente, DateTime fechaCreacion, EstadoExpediente EstadoExpediente)
{

}
