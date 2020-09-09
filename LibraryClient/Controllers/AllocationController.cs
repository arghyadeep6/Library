using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LibraryClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryClient.Controllers
{
    public class AllocationController : Controller
    {
        Uri baseaddress = new Uri("https://localhost:44348/api");
        HttpClient client;
        public AllocationController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }
        public IActionResult Index()
        {
            List<Allocation> ls = new List<Allocation>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Allocation").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<Allocation>>(data);
            }
            return View(ls);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Allocation obj)
        {
            string data = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Allocation", content).Result;
            var txt=response.Content.ReadAsStringAsync();
            var isSuccess = txt.Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    return RedirectToAction("Index");
            //}
            if (isSuccess=="SUCCESSFUL")
            {
                return RedirectToAction("Index");
            }
         
            return View();
        }
        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Allocation/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
    }
}
