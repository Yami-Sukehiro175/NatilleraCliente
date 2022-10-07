using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using NatilleraCliente.Clases;
using NatilleraCliente.Modelos;

namespace NatilleraCliente.Controladores
{
    /// <summary>
    /// Descripción breve de ControladorLiquidacion
    /// </summary>
    public class ControladorLiquidacion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string DatosLiquidacion;
            StreamReader reader = new StreamReader(context.Request.InputStream);
            DatosLiquidacion = reader.ReadToEnd();
            Liquidacion _liquidacion = JsonConvert.DeserializeObject<Liquidacion>(DatosLiquidacion);
            //context.Response.Write(_cliente.Comando);
            switch (_liquidacion.Comando.ToUpper())
            {
                case "INSERTAR":
                    context.Response.Write(Insertar(_liquidacion));
                    break;
                case "ACTUALIZAR":
                    context.Response.Write(Actualizar(_liquidacion));
                    break;
                case "ELIMINAR":
                    context.Response.Write(Eliminar(_liquidacion));

                    break;
                case "CONSULTAR":
                    context.Response.Write(Consultar(_liquidacion));
                    break;


            }
        }
        public string Consultar(Liquidacion liquidacion)
        {
            //Invocar la clase que hace el registro en la base de datos
            clsLiquidacion oLiquidacion = new clsLiquidacion();
            oLiquidacion.liquidacion = liquidacion;
            if (oLiquidacion.ConsultarLiquidacion())
            {
                liquidacion = oLiquidacion.liquidacion;
                liquidacion.Mensaje = "SI";
                liquidacion.Error = "Consulta Exitosa";
            }
            else
            {
                liquidacion.Mensaje = "NO";
                liquidacion.Error = oLiquidacion.Error;
            }


            return JsonConvert.SerializeObject(liquidacion);
        }
        public string Eliminar(Liquidacion liquidacion)
        {
            //Invocar la clase que hace el registro en la base de datos
            clsLiquidacion oLiquidacion = new clsLiquidacion();
            oLiquidacion.liquidacion = liquidacion;
            if (oLiquidacion.EliminarLiquidacion())
            {
                //liquidacion = oLiquidacion.liquidacion;
                liquidacion.Mensaje = "SI";
                liquidacion.Error = "Consulta Exitosa";
            }
            else
            {
                liquidacion.Mensaje = "NO";
                liquidacion.Error = oLiquidacion.Error;
            }


            return JsonConvert.SerializeObject(liquidacion);
        }
        public string Insertar(Liquidacion liquidacion)
        {
            //Invocar la clase que hace el registro en la base de datos
            clsLiquidacion oLiquidacion = new clsLiquidacion();
            oLiquidacion.liquidacion = liquidacion;
            if (oLiquidacion.InsertarLiquidacion())
            {
                //liquidacion = oLiquidacion.liquidacion;
                liquidacion.Mensaje = "SI";
                liquidacion.Error = "Consulta Exitosa";
            }
            else
            {
                liquidacion.Mensaje = "NO";
                liquidacion.Error = oLiquidacion.Error;
            }


            return JsonConvert.SerializeObject(liquidacion);
        }
        public string Actualizar(Liquidacion liquidacion)
        {
            //Invocar la clase que hace el registro en la base de datos
            clsLiquidacion oLiquidacion = new clsLiquidacion();
            oLiquidacion.liquidacion = liquidacion;
            if (oLiquidacion.ActualizarLiquidacion())
            {
                //liquidacion = oLiquidacion.liquidacion;
                liquidacion.Mensaje = "SI";
                liquidacion.Error = "Consulta Exitosa";
            }
            else
            {
                liquidacion.Mensaje = "NO";
                liquidacion.Error = oLiquidacion.Error;
            }


            return JsonConvert.SerializeObject(liquidacion);
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