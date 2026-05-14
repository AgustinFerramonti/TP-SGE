using System;
using SGE.Aplicacion.Expedientes;

namespace SGE.Aplicacion;

public class ListarTodosLosExpedientesUseCase (IExpedienteRepository expedienteRepository)
{
    public IEnumerable<ListarTodosLosExpedientesResponse> Ejecutar()
    {
        var expedientes = expedienteRepository.ObtenerTodos();
        var listaExpedientes = new List<ListarTodosLosExpedientesResponse>();

        foreach  (var expediente in expedientes)
        {
            listaExpedientes.Add(new ListarTodosLosExpedientesResponse(expediente.Id,expediente.Caratula.Valor,expediente.FechaCreacion,expediente.Estado));
        }

        return listaExpedientes;
    }
}
