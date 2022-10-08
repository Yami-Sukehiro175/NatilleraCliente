using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NatilleraCliente.Modelos
{
    public class Cliente
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Direccion { get; set; }
        public int IdCiudad { get; set; }
        public int IdTipoCuenta { get; set; }
        public string NumeroCuenta { get; set; }
        public int  IdBanco { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Ocupacion { get; set; }
        public string Comando { get; set; }
        public string Error { get; set; }
        public string Mensaje { get; set; }

    }
}