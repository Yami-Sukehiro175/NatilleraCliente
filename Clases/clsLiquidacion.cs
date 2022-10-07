using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Diagnostics;
using libComunes.CapaDatos;
using NatilleraCliente.Modelos;


namespace NatilleraCliente.Clases
{
    public class clsLiquidacion
    {
        public Liquidacion liquidacion { get; set; }
        public string Error { get; set; }
        public bool ConsultarLiquidacion() {
            string SQL = "Select l.Nombre,l.FechaNacimiento,l.FechaIngreso,l.FechaRenuncia,l.FechaLiquidacion,l.Direccion,l.IdCiudad,l.Telefono,l.Celular,l.Correo,l.Ocupacion,l.DiasTrabajados,l.Salario,l.PrimaServicio,l.Vacaciones,l.Cesantias,l.InteresCesantia,l.CajaCompensacion,l.ICBF,l.Sena,l.Pension,l.Salud,l.TotalLiquidacion from dbo.Liquidacion l WHERE l.Documento=@prDocumento";
            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", liquidacion.Documento);
            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    //Se inicia el proceso de lectra de datos
                    oConexion.Reader.Read();
                    liquidacion.Nombre = oConexion.Reader.GetString(0);
                    liquidacion.FechaNacimiento = oConexion.Reader.GetDateTime(1);
                    liquidacion.FechaIngreso = oConexion.Reader.GetDateTime(2);
                    liquidacion.FechaRenuncia = oConexion.Reader.GetDateTime(3);
                    liquidacion.FechaLiquidacion = oConexion.Reader.GetDateTime(4);
                    liquidacion.Direccion = oConexion.Reader.GetString(5);
                    liquidacion.IdCiudad = oConexion.Reader.GetInt32(6);
                    liquidacion.Telefono = oConexion.Reader.GetString(7);
                    liquidacion.Celular = oConexion.Reader.GetString(8);
                    liquidacion.Correo = oConexion.Reader.GetString(9);
                    liquidacion.Ocupacion = oConexion.Reader.GetString(10);
                    liquidacion.DiasTrabajados = oConexion.Reader.GetInt32(11);
                    liquidacion.Salario = oConexion.Reader.GetDouble(12);
                    liquidacion.PrimaServicio = oConexion.Reader.GetDouble(13);
                    liquidacion.Vacaciones = oConexion.Reader.GetDouble(14);
                    liquidacion.Cesantias = oConexion.Reader.GetDouble(15);
                    liquidacion.InteresCesantia = oConexion.Reader.GetDouble(16);
                    liquidacion.CajaCompensacion = oConexion.Reader.GetDouble(17);
                    liquidacion.ICBF = oConexion.Reader.GetDouble(18);
                    liquidacion.Sena = oConexion.Reader.GetDouble(19);
                    liquidacion.Pension = oConexion.Reader.GetDouble(20);
                    liquidacion.Salud = oConexion.Reader.GetDouble(21);
                    liquidacion.TotalLiquidacion = oConexion.Reader.GetDouble(22);
                    return true;

                }
                else
                {
                    Error = "No se Encontraron Datos para la luquidación Deseada:" + liquidacion.Documento;
                    return false;
                }


            }
            else
            {
                Error = oConexion.Error;
                return false;
            }

        }
        public void CalcularLiquidacion() {
            liquidacion.PrimaServicio = (liquidacion.Salario*120) / 360;
            liquidacion.Vacaciones = (liquidacion.Salario * liquidacion.DiasTrabajados) / 720;
            liquidacion.Cesantias = (liquidacion.Salario * liquidacion.DiasTrabajados) / 360;
            liquidacion.InteresCesantia = (liquidacion.Salario * liquidacion.DiasTrabajados * 0.12) / 360;
            liquidacion.CajaCompensacion = liquidacion.Salario * 0.04;
            liquidacion.ICBF = liquidacion.Salario * 0.03;
            liquidacion.Sena = liquidacion.Salario * 0.02;
            liquidacion.Pension = liquidacion.Salario * 0.12;
            liquidacion.Salud = liquidacion.Salario * 0.085;
            liquidacion.TotalLiquidacion = liquidacion.PrimaServicio + liquidacion.Vacaciones + liquidacion.Cesantias + liquidacion.InteresCesantia + liquidacion.CajaCompensacion + liquidacion.ICBF + liquidacion.Sena + liquidacion.Pension + liquidacion.Salud;}
        public bool ActualizarLiquidacion() {
            CalcularLiquidacion();
            string SQL = "UPDATE Liquidacion SET Nombre=@prNombre,FechaNacimiento=@prFechaNacimiento,FechaIngreso=@prFechaIngreso,FechaRenuncia=@prFechaRenuncia,FechaLiquidacion=@prFechaLiquidacion,Direccion=@prDireccion,IdCiudad=@prIdCiudad,Telefono=@prTelefono,Celular=@prCelular,Correo=@prCorreo,Ocupacion=@prOcupacion,DiasTrabajados=@prDiasTrabajados,Salario=@prSalario,PrimaServicio=@prPrimaServicio,Vacaciones=@prVacaciones,Cesantias=@prCesantias,InteresCesantia=@prInteresCesantia,CajaCompensacion=@prCajaCompensacion,ICBF=@prICBF,Sena=@prSena,Pension=@prPension,Salud=@prSalud,TotalLiquidacion=@prTotalLiquidacion WHERE Documento=@prDocumento";
            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", liquidacion.Documento);
            oConexion.AgregarParametro("@prNombre", liquidacion.Nombre);
            oConexion.AgregarParametro("@prFechaNacimiento", liquidacion.FechaNacimiento);
            oConexion.AgregarParametro("@prFechaIngreso", liquidacion.FechaIngreso);
            oConexion.AgregarParametro("@prFechaRenuncia", liquidacion.FechaRenuncia);
            oConexion.AgregarParametro("@prFechaLiquidacion", liquidacion.FechaLiquidacion);
            oConexion.AgregarParametro("@prDireccion", liquidacion.Direccion);
            oConexion.AgregarParametro("@prIdCiudad", liquidacion.IdCiudad);
            oConexion.AgregarParametro("@prTelefono", liquidacion.Telefono);
            oConexion.AgregarParametro("@prCelular", liquidacion.Celular);
            oConexion.AgregarParametro("@prCorreo", liquidacion.Correo);
            oConexion.AgregarParametro("@prOcupacion", liquidacion.Ocupacion);
            oConexion.AgregarParametro("@prDiasTrabajados", liquidacion.DiasTrabajados);
            oConexion.AgregarParametro("@prSalario", liquidacion.Salario);
            oConexion.AgregarParametro("@prPrimaServicio", liquidacion.PrimaServicio);
            oConexion.AgregarParametro("@prVacaciones", liquidacion.Vacaciones);
            oConexion.AgregarParametro("@prCesantias", liquidacion.Cesantias);
            oConexion.AgregarParametro("@prInteresCesantia", liquidacion.InteresCesantia);
            oConexion.AgregarParametro("@prCajaCompensacion", liquidacion.CajaCompensacion);
            oConexion.AgregarParametro("@prICBF", liquidacion.ICBF);
            oConexion.AgregarParametro("@prSena", liquidacion.Sena);
            oConexion.AgregarParametro("@prPension", liquidacion.Pension);
            oConexion.AgregarParametro("@prSalud", liquidacion.Salud);
            oConexion.AgregarParametro("@prTotalLiquidacion", liquidacion.TotalLiquidacion);
            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }
        }
        public bool EliminarLiquidacion() {
            string SQL = "DELETE from Liquidacion WHERE Documento=@prDocumento";
            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", liquidacion.Documento);
            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }
        }
        public bool InsertarLiquidacion() {
            CalcularLiquidacion();
            string SQL = "INSERT INTO Liquidacion(Documento,Nombre,FechaNacimiento,FechaIngreso,FechaRenuncia,FechaLiquidacion,Direccion,IdCiudad,Telefono,Celular,Correo," +
                "Ocupacion,DiasTrabajados,Salario,PrimaServicio,Vacaciones,Cesantias,InteresCesantia,CajaCompensacion,ICBF,Sena,Pension,Salud,TotalLiquidacion) " +
                "VALUES(@prDocumento,@prNombre,@prFechaNacimiento,@prFechaIngreso,@prFechaRenuncia,@prFechaLiquidacion,@prDireccion,@prIdCiudad,@prTelefono,@prCelular," +
                "@prCorreo,@prOcupacion,@prDiasTrabajados,@prSalario,@prPrimaServicio,@prVacaciones,@prCesantias,@prInteresCesantia,@prCajaCompensacion,@prICBF,@prSena,@prPension,@prSalud,@prTotalLiquidacion)";
            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento",liquidacion.Documento);
            oConexion.AgregarParametro("@prNombre",liquidacion.Nombre);
            oConexion.AgregarParametro("@prFechaNacimiento", liquidacion.FechaNacimiento);
            oConexion.AgregarParametro("@prFechaIngreso", liquidacion.FechaIngreso);
            oConexion.AgregarParametro("@prFechaRenuncia",liquidacion.FechaRenuncia);
            oConexion.AgregarParametro("@prFechaLiquidacion",liquidacion.FechaLiquidacion);
            oConexion.AgregarParametro("@prDireccion", liquidacion.Direccion);
            oConexion.AgregarParametro("@prIdCiudad", liquidacion.IdCiudad);
            oConexion.AgregarParametro("@prTelefono", liquidacion.Telefono);
            oConexion.AgregarParametro("@prCelular", liquidacion.Celular);
            oConexion.AgregarParametro("@prCorreo", liquidacion.Correo);
            oConexion.AgregarParametro("@prOcupacion", liquidacion.Ocupacion);
            oConexion.AgregarParametro("@prDiasTrabajados", liquidacion.DiasTrabajados);
            oConexion.AgregarParametro("@prSalario", liquidacion.Salario);
            oConexion.AgregarParametro("@prPrimaServicio", liquidacion.PrimaServicio);
            oConexion.AgregarParametro("@prVacaciones", liquidacion.Vacaciones);
            oConexion.AgregarParametro("@prCesantias", liquidacion.Cesantias);
            oConexion.AgregarParametro("@prInteresCesantia", liquidacion.InteresCesantia);
            oConexion.AgregarParametro("@prCajaCompensacion", liquidacion.CajaCompensacion);
            oConexion.AgregarParametro("@prICBF", liquidacion.ICBF);
            oConexion.AgregarParametro("@prSena", liquidacion.Sena);
            oConexion.AgregarParametro("@prPension", liquidacion.Pension);
            oConexion.AgregarParametro("@prSalud", liquidacion.Salud);
            oConexion.AgregarParametro("@prTotalLiquidacion", liquidacion.TotalLiquidacion);
            if (oConexion.EjecutarSentencia())
            {
                return true;
            }
            else
            {
                Error = oConexion.Error;
                return false;
            }
        }


    }
}