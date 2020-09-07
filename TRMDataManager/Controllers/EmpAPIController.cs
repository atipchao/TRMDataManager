using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TRMDataManager.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
    public class EmpAPIController : System.Web.Http.ApiController
    {
        // GET: EmpAPI - return Emp
        public HttpResponseMessage Get()
        {

            List<Employee> employees = new List<Employee>();
            TRMDbEntities _db = new TRMDbEntities();
            using (_db)
            {
                employees = _db.Employees.OrderBy(s => s.Name).ToList();
                HttpResponseMessage data;
                data = Request.CreateResponse(HttpStatusCode.OK, employees);
                return (data);
            }
        }
    }
}