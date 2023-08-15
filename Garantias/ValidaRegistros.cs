using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garantias
{
    class ValidaRegistros
    {
        public static void valida_registro()
        {
            //verifica conexion a la base de datos

            iDB2Connection con = new iDB2Connection(ConexionAs400.Conexion);
            con.Open();

            iDB2Command cm = new iDB2Command();
            cm.Connection = con;
            //peso sobrevuelo
            string cadena = "select FGARUC as RUC, CONCAT (FGACOM,FGACO3) as  COMPANIA_CONTRATISTA,FGANUM as NUMERO_GARANTIA, FGAFEC as  FECHA_EXPEDICION, FGAADM as ADMINISTRADOR_CONTRATO," +
                "case when FGATIP = '1' then 'GARANTIAS POR CONVENIOS DE PAGO'  when FGATIP = '2' then 'REPONSABILIDAD CIVIL'" +
                " when FGATIP = '3' then 'GARANTÍAS TÉCNICAS'  when FGATIP = '4' then 'CONTRATOS ARRIENDOS'     when FGATIP = '5' then 'CONTRATOS Y SERVICIOS' when FGATIP = '6' then 'CONTRATOS Y OBRAS'" +
                "END AS  TIPO_GARANTIA,  CONCAT (FGACON,FGACO2) as  CONCEPTO_GARANTIA,FGARU1 as RUC_CIA_SEGUROS,FCONOM as RAZON_SOCIAL_ASEGURADORA,FGAVIG as VIGENCIA_DESDE,   " +
                "FGAVI1 as VIGENCIA_HASTA,FGADIA as DIAS_A_CADUCAR,   case when FGASEG = '1' then 'FIEL CUMPLIMIENTO DE CONTRATO'" +
                "     when FGASEG = '2' then 'BUEN USO DE ANTICIPO'     when FGASEG = '3' then 'RESPONSABILIDAD CIVIL'" +
                "     when FGASEG = '4' then 'RESPONSABILIDAD CIVIL SEGURIDAD'  END AS  SEGURO_DE, case when FGAEST='1' then 'VIGENTE' " +
                "   when FGAEST = '2' then 'VENCIDA'  when FGAEST = '3' then 'EJECUTADA'    when FGAEST = '4' then 'FINALIZADA' END AS ESTADO, " +
                "FGAVAL AS VALOR_GARANTIA, CONCAT(FGAOBS, CONCAT(FGAOB1, CONCAT(FGAOB2, FGAOB3))) AS OBSERVACIONES " +
                "from dgacdat.FGAARC INNER JOIN DGACDAT.FCOARC ON FGARU1 = FCORUC WHERE FGADIA <=20 AND FGAEST IN ('1','2') ";

            iDB2DataReader drDB2;
            cm.CommandText = cadena;
            drDB2 = cm.ExecuteReader();

            // RetornaValores objetovalores = new RetornaValores();


            DataTable DtRegistros = new DataTable();

            DtRegistros.Load(drDB2);

            string ResRegistros = string.Join(Environment.NewLine, DtRegistros.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
            string DatosRegistros = "";
            if (ResRegistros != "")
            {
                DatosRegistros = ConvertToHtmlFile(DtRegistros);
                EnvioCorreo.EnviarCorreo( DatosRegistros);
            }

            //  xe.Close();
            con.Close();


            // return FechaVuelo;
            //return null;
        }

 
        public static string ConvertToHtmlFile(DataTable targetTable)
        {
            string myHtmlFile = "";


            if (targetTable == null)
            {
                throw new System.ArgumentNullException("targetTable");
            }
            else
            {
                //Continue.
            }


            //Get a worker object.
            StringBuilder myBuilder = new StringBuilder();


            //Open tags and write the top portion.
            myBuilder.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            myBuilder.Append("<head>");
            myBuilder.Append("<title>");
            myBuilder.Append("Page-");
            myBuilder.Append(Guid.NewGuid().ToString());
            myBuilder.Append("</title>");
            myBuilder.Append("</head>");
            myBuilder.Append("<body>");
            myBuilder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            myBuilder.Append("style='border: solid 1px Silver; font-size: x-small;'>");


            //Add the headings row.


            myBuilder.Append("<tr align='left' valign='top'>");


            foreach (DataColumn myColumn in targetTable.Columns)
            {
                myBuilder.Append("<td align='left' valign='top'>");
                myBuilder.Append(myColumn.ColumnName);
                myBuilder.Append("</td>");
            }


            myBuilder.Append("</tr>");


            //Add the data rows.
            foreach (DataRow myRow in targetTable.Rows)
            {
                myBuilder.Append("<tr align='left' valign='top'>");


                foreach (DataColumn myColumn in targetTable.Columns)
                {
                    myBuilder.Append("<td align='left' valign='top'>");
                    myBuilder.Append(myRow[myColumn.ColumnName].ToString());
                    myBuilder.Append("</td>");
                }


                myBuilder.Append("</tr>");
            }


            //Close tags.
            myBuilder.Append("</table>");
            myBuilder.Append("</body>");
            myBuilder.Append("</html>");


            //Get the string for return.
            myHtmlFile = myBuilder.ToString();


            return myHtmlFile;
        }
    }
}
