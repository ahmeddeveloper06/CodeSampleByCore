using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CodeTset.Models;
using CodeTset.MyClasses;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeTset.Controllers
{
    [Authorize]
    public class Members : Controller
    {
        public CodeTestContext testContext = new CodeTestContext();
        public IActionResult Index()
        {

            if (testContext.Country.Count() == 0)
            {
                var item1 = new Country();
                var item2 = new Country();
                var item3 = new Country();

                item1.Name = "Palestine";
                item1.CreatedAt = DateTime.Now;

                item2.Name = "USA";
                item2.CreatedAt = DateTime.Now;

                item3.Name = "Egypt";
                item3.CreatedAt = DateTime.Now;

                testContext.Country.Add(item1);
                testContext.Country.Add(item2);
                testContext.Country.Add(item3);
                testContext.SaveChanges();


            }
            if (testContext.City.Count() == 0)
            {
                var city1 = new City();
                var city2 = new City();
                var city3 = new City();
                var city4 = new City();
                var city5 = new City();
                var city6 = new City();
                var city7 = new City();

                city1.Name = "Gaza";
                city1.CountryId = 1;
                city1.CreatedAt = DateTime.Now;

                city2.Name = "Ramallah";
                city2.CountryId = 1;
                city2.CreatedAt = DateTime.Now;

                city3.Name = "Nablus";
                city3.CountryId = 1;
                city3.CreatedAt = DateTime.Now;

                city4.Name = "New York";
                city4.CountryId = 2;
                city4.CreatedAt = DateTime.Now;

                city5.Name = "Washington";
                city5.CountryId = 2;
                city5.CreatedAt = DateTime.Now;

                city6.Name = "Cairo";
                city6.CountryId = 3;
                city6.CreatedAt = DateTime.Now;

                city7.Name = "Alexandria";
                city7.CountryId = 3;
                city7.CreatedAt = DateTime.Now;

                testContext.City.Add(city1);
                testContext.City.Add(city2);
                testContext.City.Add(city3);
                testContext.City.Add(city4);
                testContext.City.Add(city5);
                testContext.City.Add(city6);
                testContext.City.Add(city7);
                testContext.SaveChanges();

            }


            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.Members members, IFormFile ImageFile)
        {

            try
            {
                
                
                if (ModelState.IsValid)
                {
                    if (members.Email != null)
                    {
                        var IsExists = testContext.Members.Count(x => x.Email == members.Email) > 0;
                        if (IsExists)
                        {
                            return Json(new { status = 0, msg = "w: Email is already Exist" });
                        }
                        if (ImageFile != null)
                        {
                            var filename = ContentDispositionHeaderValue.Parse(ImageFile.ContentDisposition).FileName.Trim('"');
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "imgs", ImageFile.FileName);
                            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                            {
                                ImageFile.CopyToAsync(stream);
                            }
                            members.Image = filename;

                        }
                       

                        testContext.Members.Add(members);
                        testContext.SaveChanges();
                        return Json(new {  status = 0, msg = "s: The Member was Added " });
                    }
                }
                return View(members);

            }
            catch (Exception ex)
            {
                return Json(new { status = 0, msg = "e:" + ex.Message });
            }

        }

        public IActionResult getCountries()
        {
            var Countries = (from s in testContext.Country
                              select new
                              {
                                  s.Id,
                                  s.Name,

                              }).ToList();

            return Json(Countries);
        }

        public JsonResult getCities(int id)
        {
            var Cities = testContext.City.Where(w => w.CountryId == id).ToList().Select(s => new
            {
                s.Id,
                s.Name,


            }).ToList();

            return Json(Cities);
        }
     


         [HttpPost]
        public IActionResult GetMembers()
        {
            try
            {
                
                 var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var customerData = (from tempcustomer in testContext.Members select tempcustomer);
               
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(s=>sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.FirstName.Contains(searchValue) 
                                                || m.LastName.Contains(searchValue)
                                                
                                                || m.Email.Contains(searchValue));
                }
                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Json(jsonData);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IActionResult getCountry(int id)
        {
            var Country = testContext.Country.Find(id);
            return Json(Country.Name);
        }
    }


}

