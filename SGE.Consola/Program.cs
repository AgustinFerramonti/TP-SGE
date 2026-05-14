

using SGE.Aplicacion;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Comun;
using SGE.Dominio.Tramites;
using SGE.Infraestructura.AutorizacionProvisional;
using SGE.Infraestructura.Expedientes;
using SGE.Infraestructura.Tramites;

var repositorioExpedientes = new ExpedienteTxtRepository();
var repositorioTramites = new TramiteTxtRepository();
var autorizacionService = new AutorizacionProvisionalService();
var actualizacionEstadoService = new ActualizacionEstadoExpedienteService(repositorioExpedientes, repositorioTramites);

var agregarExpedienteUseCase = new AgregarExpedienteUseCase(repositorioExpedientes, autorizacionService);
var listarExpedientesUseCase = new ListarTodosLosExpedientesUseCase(repositorioExpedientes);

var agregarTramiteUseCase = new AgregarTramiteUseCase(repositorioTramites, repositorioExpedientes, autorizacionService, actualizacionEstadoService);
var listarTramitesUseCase = new ListarTodosLosTramitesPorExpedienteUseCase(repositorioTramites);

// GENERO UN USUARIO X PARA LAS PRUEBAS
Guid usuarioPrueba = Guid.NewGuid();

try
{
    // AGREGO EXPEDIENTE 1 DE PRUEBA
    var agregarExpedienteRequest = new AgregarExpedienteRequest (usuarioPrueba, " Prueba del expediente 1");
    var responseAgregar = agregarExpedienteUseCase.Ejecutar(agregarExpedienteRequest);

    //IMPRIMO PRUEBA PARA VERIFICAR EL BUEN FUNCIONAMIENTO
    Console.WriteLine($"Se creo el expediente \n ID: {responseAgregar.Id} | Caratula: {responseAgregar.Caratula} | Fecha de creacion: {responseAgregar.FechaCreacion} | Estado: {responseAgregar.Estado}");
    Guid idExpediente1 = responseAgregar.Id;
    //AGREGO EXPEDIENTE 2 DE PRUEBA
    agregarExpedienteRequest = new AgregarExpedienteRequest (usuarioPrueba, " Prueba del expediente 2");
    responseAgregar = agregarExpedienteUseCase.Ejecutar(agregarExpedienteRequest);

    //IMPRIMO PRUEBA PARA VERIFICAR EL BUEN FUNCIONAMIENTO
    Console.WriteLine($"Se creo el expediente \n ID: {responseAgregar.Id} | Caratula: {responseAgregar.Caratula} | Fecha de creacion: {responseAgregar.FechaCreacion} | Estado: {responseAgregar.Estado}");

    // AGREGO UN TRAMITE AL EXPEDIENTE 1
    var agregarTramiteRequest = new AgregarTramiteRequest (usuarioPrueba, " Contenido de prueba tramite 1", idExpediente1, EtiquetaTramite.Notificacion);
    var responseTramite = agregarTramiteUseCase.Ejecutar(agregarTramiteRequest);

    //IMPRIMO PRUEBA PARA VERIFICAR EL BUEN FUNCIONAMIENTO
    Console.WriteLine($"Se creo el tramite \n ID: {responseTramite.id} | Contenido: {responseTramite.contenidoTramite} | Fecha de creacion: {responseTramite.fechaCreacion} | Etiqueta: {responseTramite.etiquetaTramite} | Para el expediente: {responseTramite.idExpediente}");
}
catch (AutorizacionException ex)
{
    Console.WriteLine($"[Error de Autorizacion]: {ex.Message}");
}
catch (DomainException ex)
{
    Console.WriteLine($"[Error de Dominio]: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de sistema]: {ex.Message}");
}

