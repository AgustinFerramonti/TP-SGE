using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Expedientes;

public record class AgregarTramiteResponse (Guid id, EtiquetaTramite etiquetaTramite, DateTime fechaCreacion, string contenidoTramite, Guid idExpediente)
{

}
