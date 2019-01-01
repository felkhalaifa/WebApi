using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.DAL;
namespace WebApi.Controllers
{
   // [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
 
        ClickItDB db = new ClickItDB();

        public HttpResponseMessage GetUsers(string gender = "all")
        {
            int Gender = 0;
            IEnumerable<Users> oUsers;
            switch (gender.ToLower())
            {
                case "all":
                    Gender = 0;
                    break;
                case "male":
                    Gender = 1;
                    break;
                case "female":
                    Gender = 2;
                    break;
            }
            if (Gender == 0)
                oUsers = db.Users.Where(a => a.Gender == 1 || a.Gender == 2);
            else
                oUsers = db.Users.Where(a => a.Gender == Gender);
            var message = Request.CreateResponse(HttpStatusCode.OK, oUsers);
            return message;
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            Users oUser= db.Users.Find(id);
            if (oUser != null)
            {
                var message = Request.CreateResponse(HttpStatusCode.OK, oUser);
                return message;
            }
            var badMessage = Request.CreateResponse(HttpStatusCode.NotFound,"The Employee With ID: "+id+" Not Found");
            return badMessage;
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]Users oUsers)
        {
            try
            {
                db.Users.Add(oUsers);
                db.SaveChanges();
                var message = Request.CreateResponse(HttpStatusCode.Created, oUsers);
                message.Headers.Location = new Uri(Request.RequestUri + "/" + oUsers.ID);
                return message;
            }
            catch (Exception ex) {
                var badMessage = Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                return badMessage;
            }
        }

        // PUT api/values/5
        public HttpResponseMessage Put([FromBody]Users oUsers)
        {
            try
            {
                Users oUser = db.Users.Find(oUsers.ID);
                if (oUser != null)
                {
                    oUser.ID = oUsers.ID;
                    oUser.Username = oUsers.Username;
                    oUser.Email = oUsers.Email;
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.OK, oUser);
                    return message;
                }
                var Badmessage = Request.CreateResponse(HttpStatusCode.NotFound, "Not Found User ID:" + oUsers.ID);
                return Badmessage;
            }
            catch (Exception ex)
            {
                var badMessage = Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                return badMessage;
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                Users oUser = db.Users.Find(id);
                if (oUser != null)
                {
                    db.Users.Remove(oUser);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.OK);
                    return message;
                }
                var Badmessage = Request.CreateResponse(HttpStatusCode.NotFound, "Not Found User ID:" + id);
                return Badmessage;
            }
            catch (Exception ex)
            {
                var badMessage = Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                return badMessage;
            }
        }
    }
}
