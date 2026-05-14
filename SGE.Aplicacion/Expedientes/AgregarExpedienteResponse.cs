using SGE.Dominio.Expedientes;
namespace SGE.Aplicacion.Expedientes;

public record class AgregarExpedienteResponse (Guid Id, string Caratula, DateTime FechaCreacion, EstadoExpediente Estado);
