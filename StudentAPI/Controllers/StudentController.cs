using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace StudentAPI.Controllers
{
    [System.Web.Http.Authorize]
    public class StudentController : ApiController
    {
        StudentDBContext _context = new StudentDBContext();
        public HttpResponseMessage Get()
        {
            var result = from n in _context.Student select n;
            

            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        
        public HttpResponseMessage Get(int id)
        {

            var student = _context.Student.FromSqlRaw($"GetStudents {id}").ToList();
         
          
            
            
            return Request.CreateResponse(HttpStatusCode.OK, student);
           
        }

        public string Post(Student student)
        {
            try
            {

                _context.Student.Add(student);
                _context.SaveChanges();
                return "Student uspjesno dodat!";
                
            }
            catch (Exception)
            {

                return "Dogodila se greška!";
            }
        }

        public string Put(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return "Student nije pronadjen!";
            }
            
            try
            {
                _context.Student.Update(student);
                _context.SaveChanges();
                return "Student uspjesno izmijenjen!";
            }
            catch (DbUpdateConcurrencyException)
            {

                if (id != student.StudentId)
                {
                    return "Student nije pronadjen!";
                }
                else
                {
                    throw;
                }
            }
           

        }


        public string Delete(int ID)
        {
            try
            {
                var student = from n in _context.Student.Where(s => s.StudentId == ID) select n;
                
                _context.StudentKurs.RemoveRange(_context.StudentKurs.Where(s => s.StudentId == ID));
                _context.Student.Remove(student.SingleOrDefault());
                _context.SaveChanges();
                

                return "Student uspješno izbrisan!";
            }
            catch (Exception)
            {

                return "Dogodila se greška!";
            }
        }
    }
}
