using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public record class ListarTodosLosTramitesPorExpedienteResponse (Guid idTramite, string contenido, EtiquetaTramite EtiquetaTramite, DateTime fechaCreacion)
{

}
