using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NatilleraCliente.Modelos
{
    public class Liquidacion
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaIngreso{ get; set; }
        public DateTime FechaRenuncia { get; set; }
        public DateTime FechaLiquidacion { get; set; }
        public string  Direccion { get; set; }
        public int IdCiudad { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Ocupacion { get; set; }
        public int DiasTrabajados { get; set; }
        public Double Salario { get; set; }
        public Double PrimaServicio { get; set; }
        public Double Vacaciones { get; set; }
        public Double Cesantias { get; set; }
        public Double InteresCesantia { get; set; }
        public Double CajaCompensacion { get; set; }
        public Double ICBF { get; set; }
        public Double Sena { get; set; }
        public Double Pension { get; set; }
        public Double Salud { get; set; }
        public Double TotalLiquidacion { get; set; }

        public string Comando { get; set; }
        public string Error { get; set; }
        public string Mensaje { get; set; }

    }
}