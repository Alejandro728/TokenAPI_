using System;
using System.Collections.Generic;

namespace TokenAPI.Data.TokenModels;

public partial class Cliente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Identificador { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? Correo { get; set; }
}
