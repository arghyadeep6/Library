using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        private readonly LibraryContext _con;
        public StudentController(LibraryContext con)
        {
            _con = con;
            _log4net = log4net.LogManager.GetLogger(typeof(StudentController));
        }
        // GET: api/<StudentController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _con.students.ToList();
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _con.students.Find(id);
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] Student obj)
        {
            _con.students.Add(obj);
            _con.SaveChanges();
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Student ob)
        {
            var obj = _con.students.Find(id);
            if(obj!=null)
            {
                obj.cls = ob.cls;
                obj.name = ob.name;
                obj.phno = ob.phno;
                _con.Update(obj);
                _con.SaveChanges();
            }
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var obj = _con.students.Find(id);//64
            //before removing the student remove the books allocated to student
            foreach (var item in _con.allocations.ToList())//no need to consider allocation id into account 
            {
                //item is an allocation type object
                var x = item.bookid;
                int y = item.studentid;
                if (id == y)
                {
                    var m = _con.books.Find(x);
                    if (m != null)
                    {
                        m.quantity = m.quantity + 1;//returning the book to main index page
                        _con.Update(m);
                        _con.SaveChanges();
                    }
                }
            }
            _con.students.Remove(obj);//64 is removed here
            //remove the allocation of that student from allocations table
            //here the removing student from allocations will not increase book count by 1 as the book count has been increased just before
            foreach (var item in _con.allocations.ToList())//no need to consider allocation id into account 
            {
                //item is an allocation type object
                int y = item.studentid;
                if (id == y)
                {
                    _con.allocations.Remove(item);
                }
                _con.SaveChanges();
            }
        }
    }
}
