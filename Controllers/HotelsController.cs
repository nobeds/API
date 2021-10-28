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

    public class HotelsController: ControllerBase
    {
        saulius_nobedscomEntities _context = new saulius_nobedscomEntities();

        [HttpGet("{api_token}", Name = "ListHotel")]
        [SwaggerResponse(201, "The hotel was found", typeof(hotel))]
        [SwaggerResponse(400, "The hotel data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Get your hotel data",
            Description = "Requires api token",
            OperationId = "ListHotel"
        )]
        public ActionResult<hotel> ListHotel(string api_token)
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

            return hotel_info;
        }

   
        [HttpPost("{api_token}")]
        [SwaggerResponse(201, "The hotel was created", typeof(hotel))]
        [SwaggerResponse(400, "The hotel data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Creates a new hotel",
            Description = "Requires api token",
            OperationId = "CreateHotel"
        )]
        public ActionResult<rentals> CreateHotel(string api_token, hotel item)
        {
            if (api_token == null || api_token == "" || api_token == " ")
            {
                return NotFound();
            }

            _context.hotel.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("ListHotel", new { api_token = api_token }, item);
        }

        [HttpPut("{api_token}")]
        [SwaggerResponse(201, "The hotel was updated", typeof(hotel))]
        [SwaggerResponse(400, "The hotel data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Updates your hotel data",
            Description = "Requires api token",
            OperationId = "UpdateHotel"
        )]
        public IActionResult UpdateHotel(string api_token, hotel item)
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

            hotel_info.title = item.title;
            hotel_info.description = item.description;

            _context.hotel.Update(hotel_info);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{api_token}")]
        [SwaggerResponse(201, "The hotel was deleted", typeof(hotel))]
        [SwaggerResponse(400, "The hotel data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Deletes specific hotel",
            Description = "Requires api token",
            OperationId = "DeleteHotel"
        )]
        public IActionResult Delete(string api_token)
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

            if (hotel_info.hotel_id == 0)
            {
                return Forbid();
            }


            _context.hotel.Remove(hotel_info);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
