using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public record class ModificarTramiteResponse (Guid idTramite, Guid idExpediente, EtiquetaTramite etiquetaTramite, string contenido, DateTime fechaModificacion)
{

}
