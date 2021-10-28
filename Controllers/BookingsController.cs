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

    public class BookingsController : ControllerBase
    {
        saulius_nobedscomEntities _context = new saulius_nobedscomEntities();

        [HttpGet("{api_token}", Name = "ListBookings")]
        [SwaggerResponse(201, "The bookings was selected", typeof(List<order>))]
        [SwaggerResponse(400, "The booking data is invalid")]

        [SwaggerOperation(
            Summary = "Get your rentals bookings",
            Description = "Requires api token. Id - your rental id (optional). Date format yyyy-MM-dd",
            OperationId = "ListAvailability"
        )]
        public ActionResult<List<order>> ListBookings(string api_token, int? id, DateTime? fromdate, DateTime? todate)
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

            var item = new List<order>();


            if (fromdate != null && todate != null)
            {
                item = _context.orders.Where(w => w.hotel_id == hotel_info.hotel_id && w.checkin >= fromdate && w.checkout <= todate).ToList();
            }
            else
            {
                item = _context.orders.Where(w => w.hotel_id == hotel_info.hotel_id).ToList();
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
        [SwaggerResponse(201, "The booking was created", typeof(order))]
        [SwaggerResponse(400, "The booking data is invalid")]

        [SwaggerOperation(
            Summary = "Creates a new booking",
            Description = "Requires api token. Id - your listing id",
            OperationId = "CreateBooking"
        )]
        public ActionResult<rentals> CreateBooking(string api_token, int id, order item)
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

            _context.orders.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBooking", new { api_token = api_token, id = item.room_id }, item);
        }

        [HttpPut("{api_token}")]
        [SwaggerResponse(201, "The booking was updated", typeof(order))]
        [SwaggerResponse(400, "The booking data is invalid")]

        [SwaggerOperation(
            Summary = "Updates rental booking",
            Description = "Requires api token. DateTime format yyyy-MM-dd",
            OperationId = "UpdateBooking"
        )]
        public IActionResult Update(string api_token, int id, string date, order item)
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

            var todo = _context.orders.Where(w => w.hotel_id == hotel_info.hotel_id && w.room_id == id && w.checkin == Convert.ToDateTime(date)).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            //todo.hotel_id = hotel_info.hotel_id;
            //todo.room_id = id;
            //todo.quantity = item.quantity;
            //todo.price = item.price;
            //todo.airbnb = null;
            //todo.booking = null;
            //todo.expedia = null;
            //todo.hostelworld = null;

            _context.orders.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{api_token}")]
        [SwaggerResponse(201, "The booking was deleted", typeof(order))]
        [SwaggerResponse(400, "The booking data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Deletes specific rental booking",
            Description = "Requires api token",
            OperationId = "DeleteBooking"
        )]
        public IActionResult DeleteBooking(string api_token, int id, string date)
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

            var todo = _context.orders.Where(w => w.hotel_id == hotel_info.hotel_id && w.room_id == id && w.checkin == Convert.ToDateTime(date)).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            if (hotel_info.hotel_id == 0)
            {
                return Forbid();
            }

            _context.orders.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
