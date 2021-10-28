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

    public class RentalsController: ControllerBase
    {
        saulius_nobedscomEntities _context = new saulius_nobedscomEntities();

        [HttpGet("{api_token}", Name = "ListRental")]
        [SwaggerResponse(201, "The rental was found", typeof(rentals))]
        [SwaggerResponse(400, "The rental data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Get your rentals",
            Description = "Requires api token",
            OperationId = "ListRental"
        )]
        public ActionResult<List<rentals>> ListRentals(string api_token, int? id)
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

            var item = _context.rentals.Where(w => w.hotel_id == hotel_info.hotel_id).ToList();

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
        [SwaggerResponse(201, "The rental was created", typeof(rentals))]
        [SwaggerResponse(400, "The rental data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Creates a new rental",
            Description = "Requires api token",
            OperationId = "CreateRental"
        )]
        public ActionResult<rentals> CreateRental(string api_token, rentals item)
        {
            if (api_token == null || api_token == "" || api_token == " ")
            {
                return NotFound();
            }

            _context.rentals.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("ListRental", new {api_token = api_token, id = item.room_id }, item);
        }

        [HttpPut("{api_token}")]
        [SwaggerResponse(201, "The rental was updated", typeof(rentals))]
        [SwaggerResponse(400, "The rental data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Updates your rental data",
            Description = "Requires api token",
            OperationId = "UpdateRental"
        )]
        public IActionResult UpdateRental(string api_token, int id, rentals item)
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

            var todo = _context.rentals.Where(w => w.hotel_id == hotel_info.hotel_id && w.room_id == id).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            todo.title = item.title;
            todo.description = item.description;

            _context.rentals.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{api_token}")]
        [SwaggerResponse(201, "The rental was deleted", typeof(rentals))]
        [SwaggerResponse(400, "The rental data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Delete specific rental",
            Description = "Requires api token",
            OperationId = "DeleteRental"
        )]
        public IActionResult DeleteRental(string api_token, int id)
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

            var todo = _context.rentals.Where(w => w.hotel_id == hotel_info.hotel_id && w.room_id == id).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            if (hotel_info.hotel_id == 0)
            {
                return Forbid();
            }

            _context.rentals.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
