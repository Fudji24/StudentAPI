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
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Mvc;

namespace StudentAPI.Controllers
{
    //[System.Web.Http.Authorize]
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

            var students = _context.Student.FromSqlRaw($"GetStudents {id}").ToList();

            var list = new List<Tuple<Student, List<Kurs>>>();

            foreach (var student in students)
            {
                var studentKursevi = _context
                    .StudentKurs
                    .Where(x => x.StudentId == student.StudentId)
                    .Select(x => x.KursId)
                    .ToList();

                var kursevi = new List<Kurs>();
                foreach (var kurs in studentKursevi)
                {
                    var course = _context
                        .Kurs
                        .Find(kurs);
                    kursevi.Add(course);
                    
                    
                   
                }
                list.Add(new Tuple<Student, List<Kurs>>(student, kursevi));
            }



            return Request.CreateResponse(HttpStatusCode.OK, list);


            
           
        }

        public string Post(Student student)
        {
            try
            {
                
                Regex rgx = new Regex("^[A-Za-z ]+$");
                Regex numRgx = new Regex("^[0-9]+$");


                var existingStudent = _context.Student.Where(x => x.IndexNum == student.IndexNum).FirstOrDefault();
                if (existingStudent == null)
                {
                    if (rgx.IsMatch(student.Ime) && rgx.IsMatch(student.Prezime))
                    {
                        if (numRgx.IsMatch(student.Godina.ToString()) && student.Godina > 0 && student.Godina < 6)
                        {
                            _context.Student.Add(student);
                            _context.SaveChanges();
                        }
                        else
                        {
                            return "Za unos Godine dozvoljeno je samo koriscenje brojeva i to 1,2,3,4,5!";
                        }

                    }
                    else
                    {
                        return "Za unos Imena i Prezimena dozvoljeno je samo koriscenje slova!";
                    }
                    return "Student uspjesno dodat!";
                }
                else
                {
                    return "Student sa upisanim indeksom već postoji!";
                }
                 
                
                
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
