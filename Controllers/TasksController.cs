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

    public class TasksController : ControllerBase
    {
        saulius_nobedscomEntities _context = new saulius_nobedscomEntities();

        [HttpGet("{api_token}", Name = "ListTasks")]
        [SwaggerResponse(201, "The Tasks was selected", typeof(List<Tasks>))]
        [SwaggerResponse(400, "The Tasks data is invalid")]

        [SwaggerOperation(
            Summary = "Get your tasks",
            Description = "Requires api token. Id - your rental id (optional). Date format yyyy-MM-dd",
            OperationId = "ListTasks"
        )]
        public ActionResult<List<Tasks>> ListTasks(string api_token, int? id, DateTime? fromdate, DateTime? todate)
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

            var item = new List<Tasks>();


            if (fromdate != null && todate != null)
            {
                item = _context.Tasks.Where(w => w.hotel_id == hotel_info.hotel_id && w.updated >= fromdate && w.updated <= todate).ToList();
            }
            else
            {
                item = _context.Tasks.Where(w => w.hotel_id == hotel_info.hotel_id).ToList();
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
        [SwaggerResponse(201, "The Tasks was created", typeof(Tasks))]
        [SwaggerResponse(400, "The Tasks data is invalid")]

        [SwaggerOperation(
            Summary = "Creates a new task",
            Description = "Requires api token. DateTime format yyyy-MM-dd",
            OperationId = "CreateTasks"
        )]
        public ActionResult<rentals> CreateTasks(string api_token, int id, Tasks item)
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

            _context.Tasks.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTasks", new { api_token = api_token, id = item.room_id }, item);
        }

        [HttpPut("{api_token}")]
        [SwaggerResponse(201, "The Tasks was updated", typeof(Tasks))]
        [SwaggerResponse(400, "The Tasks data is invalid")]

        [SwaggerOperation(
            Summary = "Updates rental Tasks. NOBEDS.COM channel manager will update all channels automaticaly (Syncing rentals must have channels IDs)",
            Description = "Requires api token. DateTime format yyyy-MM-dd",
            OperationId = "UpdateTasks"
        )]
        public IActionResult Update(string api_token, int id, string date, Tasks item)
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

            var todo = _context.Tasks.Where(w => w.hotel_id == hotel_info.hotel_id && w.room_id == id && w.updated == Convert.ToDateTime(date)).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            todo.hotel_id = hotel_info.hotel_id;
            todo.room_id = id;

            _context.Tasks.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{api_token}")]
        [SwaggerResponse(201, "The task was deleted", typeof(Task))]
        [SwaggerResponse(400, "The task data is invalid")]
        [SwaggerResponse(500, "500 - something odd happened")]

        [SwaggerOperation(
            Summary = "Deletes specific task",
            Description = "Requires api token",
            OperationId = "DeleteTasks"
        )]
        public IActionResult DeleteTasks(string api_token, int id, string date)
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

            var todo = _context.Tasks.Where(w => w.hotel_id == hotel_info.hotel_id && w.room_id == id && w.updated == Convert.ToDateTime(date)).FirstOrDefault();

            if (todo == null)
            {
                return NotFound();
            }

            if (hotel_info.hotel_id == 0)
            {
                return Forbid();
            }

            _context.Tasks.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
