using libComunes.CapaDatos;
using NatilleraCliente.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NatilleraCliente.Clases
{
    public class clsCliente
    {
        public Cliente cliente { get; set; }
        public string Error { get; set; }
        public bool EliminarCliente()
        {
            string SQL = "DELETE from Cliente WHERE Documento=@prDocumento";
            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", cliente.Documento);
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
        public bool InsertarCliente()
        {
            string SQL = "INSERT INTO Cliente(Documento,Nombre,FechaNacimiento,FechaIngreso,Direccion,IdCiudad,IdTipoCuenta,NumeroCuenta,IdBanco,Telefono,Celular,Correo,Ocupacion) " +
                "VALUES(@prDocumento,@prNombre,@prFechaNacimiento,@prFechaIngreso,@prDireccion,@prIdCiudad,@prIdTipoCuenta,@prNumeroCuenta,@prIdBanco,@prTelefono,@prCelular,@prCorreo,@prOcupacion)";
            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", cliente.Documento);
            oConexion.AgregarParametro("@prNombre", cliente.Nombre);
            oConexion.AgregarParametro("@prFechaNacimiento", cliente.FechaNacimiento);
            oConexion.AgregarParametro("@prFechaIngreso", cliente.FechaIngreso);
            oConexion.AgregarParametro("@prDireccion", cliente.Direccion);
            oConexion.AgregarParametro("@prIdCiudad", cliente.IdCiudad);
            oConexion.AgregarParametro("@prIdTipoCuenta", cliente.IdTipoCuenta);
            oConexion.AgregarParametro("@prNumeroCuenta", cliente.NumeroCuenta);
            oConexion.AgregarParametro("@prIdBanco", cliente.IdBanco);
            oConexion.AgregarParametro("@prTelefono", cliente.Telefono);
            oConexion.AgregarParametro("@prCelular", cliente.Celular);
            oConexion.AgregarParametro("@prCorreo", cliente.Correo);
            oConexion.AgregarParametro("@prOcupacion", cliente.Ocupacion);
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
        public bool ActualizarCliente()
        {
            string SQL = "UPDATE Cliente SET Nombre=@prNombre,FechaNacimiento=@prFechaNacimiento,FechaIngreso=@prFechaIngreso,Direccion=@prDireccion,IdCiudad=@prIdCiudad,IdTipoCuenta=@prIdTipoCuenta,NumeroCuenta=@prNumeroCuenta,IdBanco=@prIdBanco,Telefono=@prTelefono,Celular=@prCelular,Correo=@prCorreo," +
                "Ocupacion=@prOcupacion WHERE Documento=@prDocumento";
            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", cliente.Documento);
            oConexion.AgregarParametro("@prNombre", cliente.Nombre);
            oConexion.AgregarParametro("@prFechaNacimiento", cliente.FechaNacimiento);
            oConexion.AgregarParametro("@prFechaIngreso", cliente.FechaIngreso);
            oConexion.AgregarParametro("@prDireccion", cliente.Direccion);
            oConexion.AgregarParametro("@prIdCiudad", cliente.IdCiudad);
            oConexion.AgregarParametro("@prIdTipoCuenta", cliente.IdTipoCuenta);
            oConexion.AgregarParametro("@prNumeroCuenta", cliente.NumeroCuenta);
            oConexion.AgregarParametro("@prIdBanco", cliente.IdBanco);
            oConexion.AgregarParametro("@prTelefono", cliente.Telefono);
            oConexion.AgregarParametro("@prCelular", cliente.Celular);
            oConexion.AgregarParametro("@prCorreo", cliente.Correo);
            oConexion.AgregarParametro("@prOcupacion", cliente.Ocupacion);
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
        public bool ConsultarCliente()
        {
            string SQL = "Select Nombre,FechaNacimiento,FechaIngreso,Direccion,IdCiudad,IdTipoCuenta,NumeroCuenta,IdBanco,Telefono,Celular,Correo,Ocupacion from dbo.Cliente l WHERE l.Documento=@prDocumento";
            clsConexion oConexion = new clsConexion();
            oConexion.SQL = SQL;
            oConexion.AgregarParametro("@prDocumento", cliente.Documento);
            if (oConexion.Consultar())
            {
                if (oConexion.Reader.HasRows)
                {
                    //Se inicia el proceso de lectra de datos
                    oConexion.Reader.Read();
                    cliente.Nombre = oConexion.Reader.GetString(0);
                    cliente.FechaNacimiento = oConexion.Reader.GetDateTime(1);
                    cliente.FechaIngreso = oConexion.Reader.GetDateTime(2);
                    cliente.Direccion = oConexion.Reader.GetString(3);
                    cliente.IdCiudad = oConexion.Reader.GetInt32(4);
                    cliente.IdTipoCuenta = oConexion.Reader.GetInt32(5);
                    cliente.NumeroCuenta = oConexion.Reader.GetString(6);
                    cliente.IdBanco = oConexion.Reader.GetInt32(7);
                    cliente.Telefono = oConexion.Reader.GetString(8);
                    cliente.Celular = oConexion.Reader.GetString(9);
                    cliente.Correo = oConexion.Reader.GetString(10);
                    cliente.Ocupacion = oConexion.Reader.GetString(11);
                    return true;

                }
                else
                {
                    Error = "No se Encontraron datos para el CLiente: " + cliente.Documento;
                    return false;
                }


            }
            else
            {
                Error = oConexion.Error;
                return false;
            }

        }


    }
}