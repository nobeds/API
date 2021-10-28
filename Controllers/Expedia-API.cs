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

    public class ExpediaController : ControllerBase
    {
        saulius_nobedscomEntities _context = new saulius_nobedscomEntities();

        [HttpGet("{api_token}", Name = "ExpediaListing")]
        [SwaggerResponse(201, "The listing was selected", typeof(List<expedia>))]
        [SwaggerResponse(400, "The listing data is invalid")]

        [SwaggerOperation(
            Summary = "Get your listing info",
            Description = "Requires api token. Id - your rental id (optional). Date format yyyy-MM-dd",
            OperationId = "ExpediaListing",
            Tags = new[] { "Expedia Api" }
        )]
        public ActionResult<List<expedia>> ExpediaListing(string api_token, int? id)
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
                .Select(s => new expedia { room_id = s.room_id, expedia_hotel_id = s.expedia_hotel_id, expedia_room_id = s.expedia_room_id, expedia_rate_id = s.expedia_rate_id }).ToList();

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
        [SwaggerResponse(201, "The Expedia data was updated", typeof(expedia))]
        [SwaggerResponse(400, "The Expedia data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Updates your Expedia data",
            Description = "Requires api token",
            OperationId = "UpdateExpedia",
            Tags = new[] { "Expedia Api" }
        )]
        public IActionResult UpdateExpedia(string api_token, [Required] int id, expedia item)
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

            todo.expedia_hotel_id = item.expedia_hotel_id;
            todo.expedia_room_id = item.expedia_room_id;
            todo.expedia_rate_id = item.expedia_rate_id;

            _context.rentals.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
