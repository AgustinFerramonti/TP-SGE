using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public record class AgregarTramiteRequest (Guid idUsuario, string Contenido, Guid idExpediente, EtiquetaTramite etiqueta)
{

}