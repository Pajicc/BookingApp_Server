using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookingApp.Models;
using System.Web.Http.OData;

namespace BookingApp.Controllers
{
    [RoutePrefix("api")]
    public class RoomReservationsController : ApiController
    {
        private BAContext db = new BAContext();

        // GET: api/RoomReservations
        [HttpGet]
        [EnableQuery]
        [Route("RoomReservations", Name = "RRes")]
        public IQueryable<RoomReservations> GetRoomReservations()
        {
            return db.RoomReservations;
        }

        // GET: api/RoomReservations/5
        [HttpGet]
        [Route("RoomReservations/{id}")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult GetRoomReservations(int id)
        {
            RoomReservations roomReservations = db.RoomReservations.Find(id);
            if (roomReservations == null)
            {
                return NotFound();
            }

            return Ok(roomReservations);
        }

        // PUT: api/RoomReservations/5
        [HttpPut]
        [Route("RoomReservations/{id}")]
        [Authorize(Roles = "AppUser")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoomReservations(int id, RoomReservations roomReservations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomReservations.Id)
            {
                return BadRequest();
            }

            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));

            if (user == null)
            {
                return BadRequest("You're not log in.");
            }

            if (roomReservations == null || !roomReservations.AppUserId.Equals(user.appUserId))
            {
                return BadRequest();
            }


            db.Entry(roomReservations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomReservationsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RoomReservations
        [HttpPost]
        [Route("RoomReservations")]
        [Authorize(Roles = "AppUser")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult PostRoomReservations(RoomReservations roomReservations)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (roomReservations.StartDate > roomReservations.EndDate)  
            {
                return BadRequest(ModelState);
            }

            List<RoomReservations> reservations = db.RoomReservations.Where(x => x.RoomId.Equals(roomReservations.RoomId)).ToList();
            bool alreadyReserved = false;

            foreach (RoomReservations roomRes in reservations)
            {
                if (roomRes.Canceled != null)
                {
                    if (!roomRes.Canceled)  //ako nije cancelovana
                    {
                        if (roomRes.EndDate != null && roomRes.StartDate != null)   //vidi u kom datumu je slobodna
                        {
                            if ((DateTime)roomRes.EndDate >= (DateTime)roomReservations.StartDate && (DateTime)roomRes.StartDate <= (DateTime)roomReservations.StartDate
                                || (DateTime)roomRes.EndDate <= (DateTime)roomReservations.EndDate && (DateTime)roomRes.StartDate >= (DateTime)roomReservations.EndDate)
                            {
                                alreadyReserved = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (alreadyReserved)    
            {
                return BadRequest("The room is already reserved by someone");
            }

            db.RoomReservations.Add(roomReservations);
            db.SaveChanges();

            return CreatedAtRoute("RRes", new { id = roomReservations.Id }, roomReservations);
        }

        // DELETE: api/RoomReservations/5
        [HttpDelete]
        [Route("RoomReservations/{id}")]
        [Authorize(Roles = "AppUser")]
        [ResponseType(typeof(RoomReservations))]
        public IHttpActionResult DeleteRoomReservations(int id)
        {
            RoomReservations roomReservations = db.RoomReservations.Find(id);
            if (roomReservations == null)
            {
                return NotFound();
            }

            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name)); //samo ako pokusava taj isti user da canceluje

            if (user == null)
            {
                return BadRequest("You're not logged in.");
            }

            if (roomReservations == null || !roomReservations.AppUserId.Equals(user.appUserId))
            {
                return BadRequest();
            }

            /*if (roomReservations.StartDate <= DateTime.Now)
            {
                return BadRequest("You are supposed to be in your accommodation right now, can not cancel reservation!");
            }*/

            //roomReservations.Canceled = true;
            //db.Entry(roomReservations).State = System.Data.Entity.EntityState.Modified;

            db.RoomReservations.Remove(roomReservations);
            db.SaveChanges();

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                bool exist = db.RoomReservations.Count(e => (e.Id == id)) > 0;

                if (!exist)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(roomReservations);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomReservationsExists(int id)
        {
            return db.RoomReservations.Count(e => e.Id == id) > 0;
        }
    }
}