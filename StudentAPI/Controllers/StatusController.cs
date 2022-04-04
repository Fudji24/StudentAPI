using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace StudentAPI.Controllers
{
    [System.Web.Http.Authorize]
    public class StatusController : ApiController
    {

        StudentDBContext _context = new StudentDBContext();
        public HttpResponseMessage Get()
        {
            var statuses = from n in _context.Status select n;

            return Request.CreateResponse(HttpStatusCode.OK, statuses.ToList());
        }
    }
}
