using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NatilleraCliente.Controladores
{
    /// <summary>
    /// Descripción breve de ControladorLiquidacion
    /// </summary>
    public class ControladorLiquidacion : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hola a todos");
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