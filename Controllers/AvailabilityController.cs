using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class AvailabilityController : ControllerBase
    {
        saulius_nobedscomEntities _context = new saulius_nobedscomEntities();

        [HttpGet("{api_token}", Name = "ListAvailability")]
        [SwaggerResponse(201, "The availability was selected", typeof(List<availability>))]
        [SwaggerResponse(400, "The availability data is invalid")]

        [SwaggerOperation(
            Summary = "Get your rentals availability",
            Description = "Requires api token. Id - your rental id (optional). Date format yyyy-MM-dd",
            OperationId = "ListAvailability"
        )]
        public ActionResult<List<availability>> ListAvailability(string api_token, int? id, DateTime? fromdate, DateTime? todate)
        {
            if (api_token == null || api_token == "" || api_token == " ")
            {
                return NotFound();
            }

            var hotel_info = _context.hotel.Where(w => w.api_token == api_token).FirstOrDefault();

            if (hotel_info == null)
            {
                return NotFound();
            }

            var item = new List<availability>();


            if (fromdate != null && todate != null)
            {
                item = _context.availability.Where(w => w.hotel_id == hotel_info.hotel_id && w.date >= fromdate && w.date <= todate).ToList();
            }
            else
            {
                item = _context.availability.Where(w => w.hotel_id == hotel_info.hotel_id).ToList();
            }


            if (id != null)
            {
                item = item.Where(w => w.room_id == id).ToList();
            }

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }


        [HttpPost("{api_token}")]
        [SwaggerResponse(201, "The availability was created", typeof(availability))]
        [SwaggerResponse(400, "The availability data is invalid")]

        [SwaggerOperation(
            Summary = "Creates a new availability",
            Description = "Requires api token. DateTime format yyyy-MM-dd",
            OperationId = "CreateAvailability"
        )]
        public ActionResult<rentals> CreateAvailability(string api_token, int id, availability item)
        {
            if (api_token == null || api_token == "" || api_token == " ")
            {
                return NotFound();
            }

            var hotel_info = _context.hotel.Where(w => w.api_token == api_token).FirstOrDefault();

            if (hotel_info == null)
            {
                return NotFound();
            }

            item.hotel_id = hotel_info.hotel_id;
            item.room_id = id;

            _context.availability.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetAvailability", new { api_token = api_token, id = item.room_id }, item);
        }

        [HttpPut("{api_token}")]
        [SwaggerResponse(201, "The availability was updated", typeof(availability))]
        [SwaggerResponse(400, "The availability data is invalid")]

        [SwaggerOperation(
            Summary = "Updates rental availability. NOBEDS.COM channel manager will update all channels automaticaly (Syncing rentals must have channels IDs)",
            Description = "Requires api token. DateTime format yyyy-MM-dd",
            OperationId = "UpdateAvailability"
        )]
        public IActionResult Update(string api_token, int id, string date, availability item)
        {
            if (api_token == null || api_token == "" || api_token == " ")
            {
                return NotFound();
            }

            var hotel_info = _context.hotel.Where(w => w.api_token == api_token).FirstOrDefault();

            if (hotel_info == null)
            {
                return NotFound();
            }

            if (item == null || item.room_id != id)
            {
                return BadRequest();
            }

            var todo = _context.availability.Where(w => w.hotel_id == hotel_info.hotel_id && w.room_id == id && w.date == Convert.ToDateTime(date)).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            todo.hotel_id = hotel_info.hotel_id;
            todo.room_id = id;
            todo.quantity = item.quantity;
            todo.price = item.price;
            todo.airbnb = null;
            todo.booking = null;
            todo.expedia = null;
            todo.hostelworld = null;

            _context.availability.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{api_token}")]
        [SwaggerResponse(201, "The rental was deleted", typeof(availability))]
        [SwaggerResponse(400, "The rental data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Deletes specific rental date availability",
            Description = "Requires api token",
            OperationId = "DeleteAvailability"
        )]
        public IActionResult DeleteAvailability(string api_token, int id, string date)
        {
            if (api_token == null || api_token == "" || api_token == " ")
            {
                return NotFound();
            }

            var hotel_info = _context.hotel.Where(w => w.api_token == api_token).FirstOrDefault();

            if (hotel_info == null)
            {
                return NotFound();
            }

            var todo = _context.availability.Where(w => w.hotel_id == hotel_info.hotel_id && w.room_id == id && w.date == Convert.ToDateTime(date)).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            if (hotel_info.hotel_id == 0)
            {
                return Forbid();
            }

            _context.availability.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
