using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssessmentTest_SoftwareEngineer.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;


namespace AssessmentTest_SoftwareEngineer.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient client = new HttpClient();

        // GET: Load Login Page
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Verify Login
        [HttpPost]
        public ActionResult Login(Login login)
        {
            string loginJson = JsonConvert.SerializeObject(login);
            var contentData = new StringContent(loginJson, Encoding.UTF8, "application/json");
            contentData.Headers.Remove("Content-Type");
            contentData.Headers.Add("Content-Type", "application/json");

            HttpResponseMessage response = client.PostAsync("https://reqres.in/api/login", contentData).Result;
            var responseData = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return View("Users");
            }

            ViewBag.Message = "Invalid Username or Password";
            return View();
        }

        // GET:  Get list user page
        [HttpGet]
        public ActionResult Users()
        {
            return View();
        }

        // GET:  Get list user page
        [HttpPost]
        public JsonResult GetUsers(JqueryDatatableParam param)
        {
            var page = param.start <= 0 ? 1 : (param.start/param.length) +1;
            HttpResponseMessage response = client.GetAsync("https://reqres.in/api/users?page=" + page).Result;
            var responseData = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData);

                var totalRecords = (string)item.total;

                //List<User> displayResult = item["data"].ToList();
                var deptObj = JsonConvert.DeserializeObject<UserRes>(responseData);

                return Json(new
                {
                    draw = param.draw,
                    recordsFiltered = (string)item.total,
                    recordsTotal = (string)item.per_page,
                    data = deptObj.data
                }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.Message = "Error Retriving Data";
            return Json(new { status = false });
        }



        // GET:  Get detail user page
        [HttpGet]
        public JsonResult DetailUser(string id)
        {
            HttpResponseMessage response = client.GetAsync("https://reqres.in/api/users/" + id).Result;
            var responseData = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(responseData);

                return Json(new
                {
                    id = (string)item.data.id,
                    name = (string)item.data.email,
                    firstName = (string)item.data.first_name,
                    lastName = (string)item.data.last_name,
                    avatar = (string)item.data.avatar
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false });
        }
    }
}