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
    public class AllocationController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        private readonly LibraryContext _con;
        public AllocationController(LibraryContext con)
        {
            _con = con;
            _log4net = log4net.LogManager.GetLogger(typeof(AllocationController));
        }
        // GET: api/<AllocationController>
        [HttpGet]
        public IEnumerable<Allocation> Get()
        {
            return _con.allocations.ToList();
        }

        // GET api/<AllocationController>/5
        [HttpGet("{id}")]
        public Allocation Get(int id)
        {
            return _con.allocations.Find(id);
        }

        // POST api/<AllocationController>
        [HttpPost]
        public string Post([FromBody] Allocation obj)
        {
            var ob = _con.books.Find(obj.bookid);
            var stu = _con.students.Find(obj.studentid);
            if (ob != null && ob.quantity > 0 && stu!=null)
            {
                ob.quantity = ob.quantity - 1;
                _con.Update(ob);
                _con.SaveChanges();
                _con.allocations.Add(obj);
                _con.SaveChanges();
                return "SUCCESSFUL";
            }
            else
                return "ERROR";   
    
        }
        //public string Post([FromBody] Allocation obj)
        //{
        //    var ob = _con.books.Find(obj.bookid);
        //    if (ob != null)
        //    {
        //        var x = obj.bookid;
        //        var m = _con.books.Find(x);
        //        if (m != null && m.quantity > 0)
        //        {
        //            m.quantity = m.quantity - 1;
        //            _con.Update(m);
        //            _con.SaveChanges();
        //        }
        //        _con.allocations.Add(obj);
        //        _con.SaveChanges();
        //        return "SUCCESSFUL";
        //    }
        //    else
        //    {
        //        return "BOOK IS NOT PRESENT IN THE MAIN TABLE";
        //    }
        //}

        // PUT api/<AllocationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Allocation ob)
        {
            var obj = _con.allocations.Find(id);
            if(obj!=null)
            {
                obj.bookid = ob.bookid;
                obj.studentid = ob.studentid;
                _con.Update(obj);
                _con.SaveChanges();
            }
        }

        // DELETE api/<AllocationController>/5
        [HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    var obj = _con.allocations.Find(id);
        //    _con.allocations.Remove(obj);
        //    _con.SaveChanges();
        //}
        public void Delete(int id)
        {
            var obj = _con.allocations.Find(id);
            var x = obj.bookid;
            var m = _con.books.Find(x);
            if(m!=null)
            {
                m.quantity = m.quantity + 1;
                _con.Update(m);
                _con.SaveChanges();
            }
            _con.allocations.Remove(obj);
            _con.SaveChanges();
        }
    }
}
