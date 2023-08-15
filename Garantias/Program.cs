using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garantias
{
    class Program
    {
        static void Main(string[] args)
        {
            //prueba de cambio
            DateTime fecha = System.DateTime.Now;

            //string fechaVuelo = DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper();
            //string fechaProceso = DateTime.Now.ToString("yyyyMMdd").ToUpper();
            ValidaRegistros.valida_registro();
            
        }
    }
}
