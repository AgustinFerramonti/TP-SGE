
using SGE.Aplicacion;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;
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
var eliminarExpedienteUseCase = new EliminarExpedienteUseCase(repositorioExpedientes,repositorioTramites,autorizacionService);
var modificarCaratulaExpediente = new ModificarCaratulaExpedienteUseCase(repositorioExpedientes,autorizacionService);

var agregarTramiteUseCase = new AgregarTramiteUseCase(repositorioTramites, repositorioExpedientes, autorizacionService, actualizacionEstadoService);
var listarTramitesUseCase = new ListarTodosLosTramitesPorExpedienteUseCase(repositorioTramites);

// GENERO UN USUARIO X PARA LAS PRUEBAS
Guid usuarioPrueba = Guid.NewGuid();
//VARIABLE PARA GUARDAR ID DE UN EXPEDIENTE
Guid idExpediente1,idExpediente2;

// CAMINO FELIZ
try
{
    // AGREGO EXPEDIENTE 1 DE PRUEBA
    Console.WriteLine("AGREGANDO EXPEDIENTE...");
    var agregarExpedienteRequest = new AgregarExpedienteRequest (usuarioPrueba, " Prueba del expediente 1");
    var responseAgregar = agregarExpedienteUseCase.Ejecutar(agregarExpedienteRequest);

    //IMPRIMO PRUEBA PARA VERIFICAR EL BUEN FUNCIONAMIENTO
    Console.WriteLine($"Se creo el expediente \n ID: {responseAgregar.Id} | Caratula: {responseAgregar.Caratula} | Fecha de creacion: {responseAgregar.FechaCreacion} | Estado: {responseAgregar.Estado}");

    idExpediente1 = responseAgregar.Id; // GUARDO ESTA ID PARA PROBAR DESPUES

    //AGREGO EXPEDIENTE 2 DE PRUEBA
    Console.WriteLine("AGREGANDO EXPEDIENTE...");
    agregarExpedienteRequest = new AgregarExpedienteRequest (usuarioPrueba, " Prueba del expediente 2");
    responseAgregar = agregarExpedienteUseCase.Ejecutar(agregarExpedienteRequest);

    idExpediente2 = responseAgregar.Id; // GUARDO ESTA ID PARA PROBAR DESPUES

    //IMPRIMO PRUEBA PARA VERIFICAR EL BUEN FUNCIONAMIENTO
    Console.WriteLine($"Se creo el expediente \n ID: {responseAgregar.Id} | Caratula: {responseAgregar.Caratula} | Fecha de creacion: {responseAgregar.FechaCreacion} | Estado: {responseAgregar.Estado}");

    // AGREGO UN TRAMITE AL EXPEDIENTE 1
    Console.WriteLine($"AGREGANDO TRAMITE AL EXPEDIENTE {idExpediente1}...");
    var agregarTramiteRequest = new AgregarTramiteRequest (usuarioPrueba, " Contenido de prueba tramite 1", idExpediente1, EtiquetaTramite.PaseAEstudio);
    var responseTramite = agregarTramiteUseCase.Ejecutar(agregarTramiteRequest);

    //IMPRIMO PRUEBA PARA VERIFICAR EL BUEN FUNCIONAMIENTO
    Console.WriteLine($"Se creo el tramite \n ID: {responseTramite.id} | Contenido: {responseTramite.contenidoTramite} | Fecha de creacion: {responseTramite.fechaCreacion} | Etiqueta: {responseTramite.etiquetaTramite} | Para el expediente: {responseTramite.idExpediente}");

    // MODIFICAR CARATULA EXPEDIENTE EN ESTE CASO, EXPEDIENTE 2
    Console.WriteLine($"MODIFICANDO EL EXPEDIENTE {idExpediente2}..."); 
    var modificarCaratulaExpedienteRequest = new ModificarCaratulaExpedienteRequest(usuarioPrueba,idExpediente2," Nueva caratula del expediente 2");
    var responseModificarCaratula = modificarCaratulaExpediente.Ejecutar(modificarCaratulaExpedienteRequest);

    // LISTAR TODOS LOS EXPEDIENTES
    Console.WriteLine("LISTANDO TODOS LOS EXPEDIENTES...");
    var listaExpedientes = listarExpedientesUseCase.Ejecutar();

    Console.WriteLine($"------------------ \n LISTA DE EXPEDIENTES \n --------------------" );
    foreach (ListarTodosLosExpedientesResponse  e in listaExpedientes)
    {
        Console.WriteLine($"ID: {e.idExpediente} | CARATULA: {e.CaratulaExpediente} | ESTADO: {e.EstadoExpediente} | FECHA CREACION: {e.fechaCreacion}");
    }

    //LISTAR TRAMITES 
    Console.WriteLine($"LISTANDO TRAMITES DEL EXPEDIENTE {idExpediente1}...");
    var listaTramites = listarTramitesUseCase.Ejecutar(new ListarTodosLosTramitesPorExpedienteRequest(idExpediente1));
    
    Console.WriteLine($"------------------ \n LISTA DE TRAMITES DEL EXPEDIENTE {idExpediente1} \n --------------------" );
    foreach (ListarTodosLosTramitesPorExpedienteResponse  t in listaTramites)
    {
        Console.WriteLine($"ID: {t.idTramite} | FECHA CREACIO: {t.fechaCreacion} | CONTENIDO: {t.contenido} | ETIQUETA: {t.EtiquetaTramite} ");
    }

    //ELIMINAR EXPEDIENTE
    Console.WriteLine($"ELIMINANDO EL EXPEDIENTE {idExpediente1}...");
    var eliminarExpedienteRequest = new EliminarExpedienteRequest(usuarioPrueba, idExpediente1);
    var eliminarExpedienteResponse = eliminarExpedienteUseCase.Ejecutar(eliminarExpedienteRequest);

    // LISTAR TODOS LOS EXPEDIENTES DESPUES DE ELIMINAR
    Console.WriteLine("ACTUALIZANDO EL LISTADO DE EXPEDIENTES...");
    listaExpedientes = listarExpedientesUseCase.Ejecutar();

    Console.WriteLine($"------------------ \n LISTA DE EXPEDIENTES TRAS ELIMINACION DE UNO \n --------------------" );
    foreach (ListarTodosLosExpedientesResponse  e in listaExpedientes)
    {
        Console.WriteLine($"ID: {e.idExpediente} | CARATULA: {e.CaratulaExpediente} | ESTADO: {e.EstadoExpediente} | FECHA CREACION: {e.fechaCreacion}");
    }

    //LISTA TRAMITES TRAS ELIMINACION EN CASCADA 
    Console.WriteLine($"LISTANDO TRAMITES DEL EXPEDIENTE {idExpediente1}...");
    listaTramites = listarTramitesUseCase.Ejecutar(new ListarTodosLosTramitesPorExpedienteRequest(idExpediente1));
    
    Console.WriteLine($"------------------ \n LISTA DE TRAMITES DEL EXPEDIENTE {idExpediente1} TRAS SU ELIMINACION \n --------------------" );
    foreach (ListarTodosLosTramitesPorExpedienteResponse  t in listaTramites)
    {
        Console.WriteLine($"ID: {t.idTramite} | FECHA CREACIO: {t.fechaCreacion} | CONTENIDO: {t.contenido} | ETIQUETA: {t.EtiquetaTramite} ");
    }
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


//PARA PROBAR ERROR DE AUTORIZACION, CAMBIAR A FALSE EN AutorizacionProvisionalService.cs en SGE.Infraestructura

// CAMINO ERROR CARATULA VACIA 
try
{
    Console.WriteLine("AGREGANDO EXPEDIENTE...");
    var agregarExpedienteRequest = new AgregarExpedienteRequest (usuarioPrueba, " ");
    var responseAgregar = agregarExpedienteUseCase.Ejecutar(agregarExpedienteRequest);

    Console.WriteLine($"Se creo el expediente \n ID: {responseAgregar.Id} | Caratula: {responseAgregar.Caratula} | Fecha de creacion: {responseAgregar.FechaCreacion} | Estado: {responseAgregar.Estado}");
}
catch (DomainException ex)
{
    Console.WriteLine($"[Error de Dominio]: {ex.Message}");
}
catch (AutorizacionException ex)
{
    Console.WriteLine($"[Error de Autorizacion]: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de sistema]: {ex.Message}");
}

// CAMINO ERROR EXPEDIENTE NO ENCONTRADO
try
{
    Console.WriteLine("AGREGANDO TRAMITE...");
    var agregarTramiteRequest = new AgregarTramiteRequest (usuarioPrueba, " Contenido de prueba tramite 1", Guid.NewGuid(), EtiquetaTramite.PaseAEstudio);
    var responseTramite = agregarTramiteUseCase.Ejecutar(agregarTramiteRequest);

    Console.WriteLine($"Se creo el tramite \n ID: {responseTramite.id} | Contenido: {responseTramite.contenidoTramite} | Fecha de creacion: {responseTramite.fechaCreacion} | Etiqueta: {responseTramite.etiquetaTramite} | Para el expediente: {responseTramite.idExpediente}");
}
catch (DomainException ex)
{
    Console.WriteLine($"[Error de Dominio]: {ex.Message}");
}
catch (AutorizacionException ex)
{
    Console.WriteLine($"[Error de Autorizacion]: {ex.Message}");
}
catch (NoExisteLaEntidadException ex)
{
    Console.WriteLine($"[Error de Entidad]: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"[Error de Sistema]: {ex.Message}");
}