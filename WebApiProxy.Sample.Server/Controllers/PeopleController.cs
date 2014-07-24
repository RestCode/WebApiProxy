using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace WebApiProxy.Sample.Server.Controllers
{
    public partial class Person
    {
        public virtual Int32 Id { get; set; }
        public virtual String FirstName { get; set; }
        public virtual String LastName { get; set; }
        public virtual String PetName { get; set; }
        public IList<string> Preferences { get; set; }
    }

    /// <summary>
    /// Some docs come here
    /// </summary>
    public class PeopleController : ApiController
    {
        /// <summary>
        /// Gets all people in this API
        /// </summary>
        /// <returns></returns>
        public Person[] Get()
        {
            return new Person[]{

            };
        }

        /// <summary>
        /// Searches for people with a given name
        /// </summary>
        /// <param name="name">the search criteria</param>
        /// <returns></returns>
        public Person[] Get(string name, int id, string other)
        {
            return new Person[]{
                new Person { Id = 3, FirstName = "sss", LastName = "sqqqq"},
                new Person { Id = 3, FirstName = "sss", LastName = "sqqqq"}
            };
        }

        public IHttpActionResult Get(int id)
        {
            return Content(HttpStatusCode.OK, new Person());

        }

        /// <summary>
        /// Creates a new person
        /// </summary>
        /// <param name="person">Person object to create on server</param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody]Person person)
        {
            return Ok();
        }

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="id">id of the guy</param>
        /// <param name="person">the person object</param>
        /// <returns></returns>
        public IHttpActionResult Put(int id, [FromBody]Person person)
        {
            return Ok();
        }
    }
}
