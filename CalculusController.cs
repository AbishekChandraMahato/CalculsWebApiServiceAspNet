using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Calculuswebservice.Controllers
{
    public class CalculusController : ApiController
    {

        public bool error { set; get; }
        public string message { set; get; }
        public double result { set; get; }


        public double Evaluate(string expression)
        {
            try
            {

                var loDataTable = new DataTable();
                var loDataColumn = new DataColumn("Eval", typeof(double), expression);
                loDataTable.Columns.Add(loDataColumn);
                loDataTable.Rows.Add(0);

                result = (double)(loDataTable.Rows[0]["Eval"]);
                error = false;
                return result;
            }
            catch (Exception e)
            {

                error = true;
                message = expression;
                return 1.0;

            }


        }
        [HttpGet]
        public HttpResponseMessage GetEvaluate(string query= "2 * (23/(33))- 23 * (23)")
        {


            double final = Evaluate(query);

            var response1 = new
            {
                error = false,
                result = final

            };

            var response2 = new
            {
                error = true,
                message = query

            };

            if (final == 1.0)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, response2.ToString());
            }

            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, response1.ToString());

            }





        }
    }
}


