using System;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ListarTodosLosTramitesPorExpedienteUseCase (ITramiteRepository tramiteRepository)
{
    public IEnumerable<ListarTodosLosTramitesPorExpedienteResponse> Ejecutar(ListarTodosLosTramitesPorExpedienteRequest listarTodosLosTramitesPorExpedienteRequest)
    {
        var tramites = tramiteRepository.ObtenerPorExpedienteId(listarTodosLosTramitesPorExpedienteRequest.idExpediente);

        var listatramites = new List<ListarTodosLosTramitesPorExpedienteResponse>();

        foreach (var tramite in tramites)
        {
            listatramites.Add(new ListarTodosLosTramitesPorExpedienteResponse(tramite.Id,tramite.Contenido._valor,tramite.Etiqueta,tramite.FechaCreacion));
        }

        return listatramites;
    }
}
