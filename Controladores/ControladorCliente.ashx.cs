using NatilleraCliente.Clases;
using NatilleraCliente.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NatilleraCliente.Controladores
{
    /// <summary>
    /// Descripción breve de ControladorCliente
    /// </summary>
    public class ControladorCliente : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DatosCliente;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            DatosCliente = reader.ReadToEnd();
            Cliente _cliente = JsonConvert.DeserializeObject<Cliente>(DatosCliente);
            //context.Response.Write(_cliente.Comando);
            switch (_cliente.Comando.ToUpper())
            {
                case "INSERTAR":
                    context.Response.Write(Insertar(_cliente));
                    break;
                case "ACTUALIZAR":
                    context.Response.Write(Actualizar(_cliente));
                    break;
                case "ELIMINAR":
                    context.Response.Write(Eliminar(_cliente));

                    break;
                case "CONSULTAR":
                    context.Response.Write(Consultar(_cliente));
                    break;


            }
        }
        public string Consultar(Cliente cliente)
        {
            //Invocar la clase que hace el registro en la base de datos
            clsCliente oCliente = new clsCliente();
            oCliente.cliente = cliente;
            if (oCliente.ConsultarCliente())
            {
                cliente = oCliente.cliente;
                cliente.Mensaje = "SI";
                cliente.Error = "La consulta se realizó exitosamente.";
            }
            else
            {
                cliente.Mensaje = "NO";
                cliente.Error = oCliente.Error;
            }


            return JsonConvert.SerializeObject(cliente);
        }
        public string Insertar(Cliente cliente)
        {
            //Invocar la clase que hace el registro en la base de datos
            clsCliente oCliente = new clsCliente();
            oCliente.cliente = cliente;
            if (oCliente.InsertarCliente())
            {
                //cliente = oCliente.cliente;
                cliente.Mensaje = "SI";
                cliente.Error = "Se ha Insertado Correctamente.";
            }
            else
            {
                cliente.Mensaje = "NO";
                cliente.Error = oCliente.Error;
            }


            return JsonConvert.SerializeObject(cliente);
        }
        public string Eliminar(Cliente cliente)
        {
            //Invocar la clase que hace el registro en la base de datos
            clsCliente oCliente = new clsCliente();
            oCliente.cliente = cliente;
            if (oCliente.EliminarCliente())
            {
                //cliente = oCliente.cliente;
                cliente.Mensaje = "SI";
                cliente.Error = "Se ha eliminado de forma correcta.";
            }
            else
            {
                cliente.Mensaje = "NO";
                cliente.Error = oCliente.Error;
            }


            return JsonConvert.SerializeObject(cliente);
        }
        public string Actualizar(Cliente cliente)
        {
            //Invocar la clase que hace el registro en la base de datos
            clsCliente oCliente = new clsCliente();
            oCliente.cliente = cliente;
            if (oCliente.ActualizarCliente())
            {
                //cliente = oCliente.cliente;
                cliente.Mensaje = "SI";
                cliente.Error = "La actualizacion se ha realizó correctamente." +
                    "";
            }
            else
            {
                cliente.Mensaje = "NO";
                cliente.Error = oCliente.Error;
            }


            return JsonConvert.SerializeObject(cliente);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}