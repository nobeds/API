using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class BookingController : ControllerBase
    {
        saulius_nobedscomEntities _context = new saulius_nobedscomEntities();

        [HttpGet("{api_token}", Name = "BookingListing")]
        [SwaggerResponse(201, "The listing was selected", typeof(List<booking>))]
        [SwaggerResponse(400, "The listing data is invalid")]

        [SwaggerOperation(
            Summary = "Get your listing info",
            Description = "Requires api token. Id - your rental id (optional). Date format yyyy-MM-dd",
            OperationId = "BookingListing",
            Tags = new[] { "Booking Api" }
        )]
        public ActionResult<List<booking>> BookingListing(string api_token, int? id)
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

            var item = _context.rentals.Where(w => w.hotel_id == hotel_info.hotel_id)
                .Select(s => new booking { room_id = s.room_id, booking_hotel_id = s.booking_hotel_id, booking_room_id = s.booking_room_id, booking_rate_id = s.booking_rate_id }).ToList();

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
        [SwaggerResponse(201, "The Booking data was updated", typeof(booking))]
        [SwaggerResponse(400, "The Booking data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Updates your Booking data",
            Description = "Requires api token",
            OperationId = "UpdateBooking",
            Tags = new[] { "Booking Api" }
        )]
        public IActionResult UpdateBooking(string api_token, [Required] int id, booking item)
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

            var todo = _context.rentals.Where(w => w.room_id == id).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            todo.booking_hotel_id = item.booking_hotel_id;
            todo.booking_room_id = item.booking_room_id;
            todo.booking_rate_id = item.booking_rate_id;

            _context.rentals.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
