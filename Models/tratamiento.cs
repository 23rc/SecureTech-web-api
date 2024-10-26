using System;
using System.Collections.Generic;

namespace SecureTech_web_api.Models;

public partial class tratamiento
{
    public int codigoTratamiento { get; set; }

    public int codigoPaciente { get; set; }

    public string descripcion { get; set; } = null!;

    public DateOnly fechaInicio { get; set; }

    public byte estatus { get; set; }

    public virtual paciente codigoPacienteNavigation { get; set; } = null!;
}
