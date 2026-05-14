using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes;

public record class ModificarCaratulaExpedienteResponse (Guid idExpediente, DateTime fechaUltimaModificacion, string Caratula)
{

}
