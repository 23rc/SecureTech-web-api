using System;
using System.Collections.Generic;

namespace SecureTech_web_api.Models;

public partial class resultadoDePrueba
{
    public int codigoResultado { get; set; }

    public int codigoPaciente { get; set; }

    public string tipoPrueba { get; set; } = null!;

    public string resultado { get; set; } = null!;

    public DateOnly fechaPrueba { get; set; }

    public byte estatus { get; set; }

    public virtual paciente codigoPacienteNavigation { get; set; } = null!;
}
