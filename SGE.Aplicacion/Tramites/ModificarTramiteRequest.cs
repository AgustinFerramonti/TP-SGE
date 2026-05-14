using SGE.Dominio.Tramites;

namespace SGE.Aplicacion;

public record class ModificarTramiteRequest (Guid idUsuario, Guid idTramite, EtiquetaTramite etiquetaTramite, string contenido)
{

}
