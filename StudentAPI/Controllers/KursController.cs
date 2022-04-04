
using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentAPI.Controllers
{
    [System.Web.Http.Authorize]
    public class KursController : ApiController
    {
        StudentDBContext _context = new StudentDBContext();
        public HttpResponseMessage Get()
        {
            var kurs = from n in _context.Kurs select n;

            return Request.CreateResponse(HttpStatusCode.OK, kurs);
        }

        public HttpResponseMessage Get(int id)
        {
            var kurs = from n in _context.Kurs.Where(k => k.KursId == id) select n;

            return Request.CreateResponse(HttpStatusCode.OK, kurs);
          
           
        }

        public string Post(Kurs kurs)
        {
            try
            {
                _context.Kurs.Add(kurs);
                _context.SaveChanges();

                return "Kurs uspješno dodat!";
            }
            catch (Exception)
            {

                return "Dogodila se greška!";
            }
        }
    }
}
