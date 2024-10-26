using System;
using System.Collections.Generic;

namespace SecureTech_web_api.Models;

public partial class paciente
{
    public int codigoPaciente { get; set; }

    public string nombreCompleto { get; set; } = null!;

    public DateOnly fechaNacimiento { get; set; }

    public byte estatus { get; set; }

    public virtual ICollection<resultadoDePrueba> resultadoDePrueba { get; set; } = new List<resultadoDePrueba>();

    public virtual ICollection<tratamiento> tratamiento { get; set; } = new List<tratamiento>();
}
