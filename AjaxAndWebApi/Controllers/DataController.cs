using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AjaxAndWebApi.Controllers
{
    public class DataController : ApiController
    {
        PersonFac pf = new PersonFac();
        Person person = new Person();

        [Route("api/data/GetString")]
        [HttpGet]
        public string GetString()
        {
            return "Her er en tekststring!!!";
        }

        [Route("api/Data/Plus/{tal1?}/{tal2?}")]
        [HttpGet]
        public int Plus(int tal1=0, int tal2=0)
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
    }
}
