﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.IO;
using System.Web.Helpers;


namespace AjaxAndWebApi.Controllers
{

    public class DataController : ApiController
    {

        PersonFac pf = new PersonFac();
        Person person = new Person();
        BrugerFac bf = new BrugerFac();

        [Route("api/data/GetString")]
        [HttpGet]
        public string GetString()
        {
            return "Her er en tekststring!!!";
        }

        [Route("api/Data/Plus/{tal1?}/{tal2?}")]
        [HttpGet]
        public int Plus(int tal1 = 0, int tal2 = 0)
        {
            return tal1 + tal2;
        }

        [Route("api/Data/GetPerson/{id}")]
        [HttpGet]
        public Person GetPerson(int id)
        {
            return pf.Get(id);
        }

        [Route("api/Data/GetPersons")]
        [HttpGet]
        public IEnumerable<Person> GetPersons()
        {
            return pf.GetAll();
        }


        [Route("api/Data/AddPerson/")]
        [HttpPost]
        public string AddPerson(Person p)
        {
            pf.Insert(p);

            return p.Name;
        }

        [Route("api/Data/DeletePerson/{id}")]
        [HttpGet]
        public void DeletePerson(int id)
        {
            pf.Delete(id);
        }

        [Route("api/Data/UpdatePerson/")]
        [HttpPost]
        public bool UpdatePerson(Person p)
        {
            bool valid = true;
            if (ModelState.IsValid)
            {
                pf.Update(p);
            }
            else
            {
                valid = false;
            }

            return valid;
        }

        [Route("api/Data/UploadFile/")]
        [HttpPost()]
        public string UploadFiles()
        {
            Uploader u = new Uploader();

            int iUploadedCnt = 0;

            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        string filen = Path.GetFileName(hpf.FileName);
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + filen);
                        u.ReSizeImage(sPath + filen, sPath + "Thump/", 120);

                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }

            // RETURN A MESSAGE.
            if (iUploadedCnt > 0)
            {
                return iUploadedCnt + " billeder uploadet!";
            }
            else
            {
                return "Upload Failed!!!";
            }

        }

        
        [Route("api/data/GetSeacretData")]
        [HttpGet]
        public string GetSeacretData()
        {
             //ValidateToken();
            return "En hemmelig tekst!!!";
        }


        public void ValidateToken()
        {
            var token = Request.Headers.GetValues("_token").FirstOrDefault();
            if (token != "Test123")
            {
                throw new System.Web.HttpException(403, "You must be logged in to access this resource.");
            }
        }


        [Route("api/Data/GetPersons/{from}/{to}")]
        [HttpGet]
        public IEnumerable<Person> GetPersons(int from, int to)
        {
            return pf.GetPersons(from, to);
        }

        [Route("api/Data/Login/{email}/{pass}")]
        [HttpGet]
        public Bruger Login(string email, string pass)
        {                        
            Bruger b = bf.Login(email.Trim(), Crypto.Hash(pass.Trim()));            
            return b;
        }
    }   

}
