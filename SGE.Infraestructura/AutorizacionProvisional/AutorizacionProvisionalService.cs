using System;
using System.Text.RegularExpressions;
using SGE.Aplicacion.Autorizacion;

namespace SGE.Infraestructura.AutorizacionProvisional;

public class AutorizacionProvisionalService : IAutorizacionService
{
    public bool PoseeElPermiso(Guid IdUsuario, Permiso permiso)
    {
        return true;
    }
}
