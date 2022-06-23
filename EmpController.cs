using CoreAPITest.Models;
using CoreAPITest.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;



namespace CoreAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public EmpController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Route("Get/Emp")]
        [HttpGet]
        public List<Employee> GetlIST()
        {
            var list = _db.Employees.ToList();
            return list;
        }
        [Route("Post/Emp/add")]
        [HttpPost]
        public HttpResponseMessage Addemp(Employee obj)
        {
            if (obj.id == 0)
            {
                _db.Employees.Add(obj);

                _db.SaveChanges();

            }
            else
            {


                _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
            }
            HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            return result;
        }
        [Route("Post/Emp/delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var delete = _db.Employees.Where(f => f.id == id).FirstOrDefault();
            _db.Employees.Remove(delete);
            _db.SaveChanges();
            HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            return result;
        }
        [Route("Post/Emp/Edit")]
        [HttpGet]
        public Employee Edit(int id)
        {
            var edit = _db.Employees.Where(f => f.id == id).FirstOrDefault();
            //_db.Employees.Remove(Edit);

            // HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            return edit;
        }
        [Route("/Emp/Login")]
        [HttpPost]
        public Login Login(Login ui)
        {
            Login lgn = new Login();
            var res = _db.Logins.Where(m => m.gmail == ui.gmail).First();
            if(res==null)
            {
                lgn.gmail = "gmail does not match";
            }
            else
            {
                if(res.gmail == ui.gmail && res.password == ui.password)
                {
                    lgn.gmail = res.gmail;
                    lgn.password = res.password;
                }
                else
                {
                    lgn.password = "Invalid name";
                }
            }
            return lgn;
        }

    }
}


