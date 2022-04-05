
using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace StudentAPI.Controllers
{
    //[System.Web.Http.Authorize]
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
            var kursCtx = _context.Kurs.Where(k => k.KursId == id).ToList();

            var list = new List<Tuple<Kurs, List<Student>>>();

            foreach (var kurs in kursCtx)
            {
                var studentKurs = _context.StudentKurs.Where(x => x.KursId == kurs.KursId).Select(x => x.StudentId).ToList();

                var studenti = new List<Student>();
                foreach (var student in studentKurs)
                {
                    var stu = _context.Student.Find(student);
                    studenti.Add(stu);
                }
                list.Add(new Tuple<Kurs, List<Student>>(kurs, studenti));
            }

            
            return Request.CreateResponse(HttpStatusCode.OK, list);
          
           
        }

        public string Post(Kurs kurs)
        {
            try
            {
                Regex rgx = new Regex("^[A-Za-z ]+$");
                if (rgx.IsMatch(kurs.KursName))
                {
                    _context.Kurs.Add(kurs);

                    _context.SaveChanges();


                    return "Kurs uspješno dodat!";

                }
                else
                {
                    return "Dozvoljeno je koristiti samo slova za kreiranje naziva kursa!";
                }

            }
            catch (Exception)
            {

                return "Dogodila se greška!";
            }
        }
    }
}
