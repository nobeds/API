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

    public class ReviewsController: ControllerBase
    {
        saulius_nobedscomEntities _context = new saulius_nobedscomEntities();

        [HttpGet("{api_token}", Name = "ListReview")]
        [SwaggerResponse(201, "The review was found", typeof(review))]
        [SwaggerResponse(400, "The review data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Get your reviews",
            Description = "Requires api token",
            OperationId = "ListReview"
        )]
        public ActionResult<List<review>> ListReview(string api_token, int? id)
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

            var item = _context.review.Where(w => w.hotel_id == hotel_info.hotel_id).ToList();

            if (id != null)
            {
                item = item.Where(w => w.rid == id).ToList();
            }

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

   
        [HttpPost("{api_token}")]
        [SwaggerResponse(201, "The review was created", typeof(review))]
        [SwaggerResponse(400, "The review data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Creates a new review",
            Description = "Requires api token",
            OperationId = "CreateReview"
        )]
        public ActionResult<review> CreateReview(string api_token, review item)
        {
            if (api_token == null || api_token == "" || api_token == " ")
            {
                return NotFound();
            }

            _context.review.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("ListReview", new {api_token = api_token, id = item.rid }, item);
        }

        [HttpPut("{api_token}")]
        [SwaggerResponse(201, "The review was updated", typeof(review))]
        [SwaggerResponse(400, "The review data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Updates your review data",
            Description = "Requires api token",
            OperationId = "UpdateReview"
        )]
        public IActionResult UpdateReview(string api_token, int id, review item)
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

            if (item == null || item.rid != id)
            {
                return BadRequest();
            }

            var todo = _context.review.Where(w => w.hotel_id == hotel_info.hotel_id && w.rid == id).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            todo.positive = item.positive;
            todo.score = item.score;
            todo.clean = item.clean;

            _context.review.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{api_token}")]
        [SwaggerResponse(201, "The review was deleted", typeof(review))]
        [SwaggerResponse(400, "The review data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Delete specific review",
            Description = "Requires api token",
            OperationId = "DeleteReview"
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

            var todo = _context.review.Where(w => w.hotel_id == hotel_info.hotel_id && w.rid == id).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            if (hotel_info.hotel_id == 0)
            {
                return Forbid();
            }

            _context.review.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
