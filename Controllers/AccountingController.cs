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

    public class AccountingController : ControllerBase
    {
        saulius_nobedscomEntities _context = new saulius_nobedscomEntities();

        [HttpGet("{api_token}", Name = "ListAccounting")]
        [SwaggerResponse(201, "The Accounting was selected", typeof(List<accounting>))]
        [SwaggerResponse(400, "The Accounting data is invalid")]

        [SwaggerOperation(
            Summary = "Get your rentals Accounting",
            Description = "Requires api token. Id - your rental id (optional). Date format yyyy-MM-dd",
            OperationId = "ListAccounting"
        )]
        public ActionResult<List<accounting>> ListAccounting(string api_token, int? id, DateTime? fromdate, DateTime? todate)
        {
            if (api_token == null || api_token == "" || api_token == " ")
            {
                return NotFound();
            }

            var hotel_info = _context.hotel.Where(w => w.api_token == api_token)?.FirstOrDefault();

            if (hotel_info == null)
            {
                return NotFound();
            }

            var item = new List<accounting>();


            if (fromdate != null && todate != null)
            {
                item = _context.Accounting.Where(w => w.hotel_id == hotel_info.hotel_id && w.timestamp >= fromdate && w.timestamp <= todate).ToList();
            }
            else
            {
                item = _context.Accounting.Where(w => w.hotel_id == hotel_info.hotel_id).ToList();
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
        [SwaggerResponse(201, "The Accounting was created", typeof(accounting))]
        [SwaggerResponse(400, "The Accounting data is invalid")]

        [SwaggerOperation(
            Summary = "Creates a new Accounting",
            Description = "Requires api token. DateTime format yyyy-MM-dd",
            OperationId = "CreateAccounting"
        )]
        public ActionResult<rentals> CreateAccounting(string api_token, int id, accounting item)
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

            _context.Accounting.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetAccounting", new { api_token = api_token, id = item.room_id }, item);
        }

        [HttpPut("{api_token}")]
        [SwaggerResponse(201, "The Accounting was updated", typeof(accounting))]
        [SwaggerResponse(400, "The Accounting data is invalid")]

        [SwaggerOperation(
            Summary = "Updates rental Accounting.",
            Description = "Requires api token. DateTime format yyyy-MM-dd",
            OperationId = "UpdateAccounting"
        )]
        public IActionResult Update(string api_token, int id, string date, accounting item)
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

            var todo = _context.Accounting.Where(w => w.hotel_id == hotel_info.hotel_id && w.room_id == id && w.timestamp == Convert.ToDateTime(date)).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            todo.hotel_id = hotel_info.hotel_id;
            todo.room_id = id;
            //todo.quantity = item.quantity;
            //todo.price = item.price;
            //todo.airbnb = null;
            //todo.booking = null;
            //todo.expedia = null;
            //todo.hostelworld = null;

            _context.Accounting.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{api_token}")]
        [SwaggerResponse(201, "The accounting was deleted", typeof(accounting))]
        [SwaggerResponse(400, "The accounting data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Deletes specific accounting",
            Description = "Requires api token",
            OperationId = "DeleteAccounting"
        )]
        public IActionResult DeleteAccounting(string api_token, int id, string date)
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

            var todo = _context.Accounting.Where(w => w.hotel_id == hotel_info.hotel_id && w.room_id == id && w.timestamp == Convert.ToDateTime(date)).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            if (hotel_info.hotel_id == 0)
            {
                return Forbid();
            }

            _context.Accounting.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
