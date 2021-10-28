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

    public class AirbnbController : ControllerBase
    {
        saulius_nobedscomEntities _context = new saulius_nobedscomEntities();

        [HttpGet("{api_token}", Name = "AirbnbListing")]
        [SwaggerResponse(201, "The listing was selected", typeof(List<airbnb>))]
        [SwaggerResponse(400, "The listing data is invalid")]

        [SwaggerOperation(
            Summary = "Get your listing info",
            Description = "Requires api token. Id - your rental id (optional). Date format yyyy-MM-dd",
            OperationId = "AirbnbListing",
            Tags = new[] { "Airbnb Api" }
        )]
        public ActionResult<List<airbnb>> AirbnbListing(string api_token, int? id)
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
                .Select(s => new airbnb { room_id = s.room_id, airbnb_import = s.airbnb_import, airbnb_listing_id = s.airbnb_listing_id, airbnb_token = s.airbnb_token}).ToList();

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
        [SwaggerResponse(201, "The airbnb data was updated", typeof(airbnb))]
        [SwaggerResponse(400, "The airbnb data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Updates your airbnb data",
            Description = "Requires api token",
            OperationId = "UpdateAirbnb",
            Tags = new[] { "Airbnb Api" }
        )]
        public IActionResult UpdateAirbnb(string api_token, [Required]int id, airbnb item)
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

            todo.airbnb_import = item.airbnb_import;
            todo.airbnb_listing_id = item.airbnb_listing_id;
            todo.airbnb_token = item.airbnb_token;

            _context.rentals.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
