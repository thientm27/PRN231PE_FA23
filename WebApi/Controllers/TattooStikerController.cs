using BusinessLogic.Token;
using BusinessObjects;
using BusinessObjects.Models;
using ControllerAPI.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace WebApi.Controllers
{
    [Route("api/Manage")]
    [ApiController]
    [Authorize(Roles = EnumClass.RoleNames.Manager)]
    public class TattooStikerController : ControllerBase
    {
        ITattooStickerRepo service;
        IRoseTattooRepo service2;

        public TattooStikerController(ITattooStickerRepo serviceParam, IRoseTattooRepo service2Param)
        {
            service = serviceParam;
            service2 = service2Param;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = service.GetAll();
            if (result.Any()) return Ok(result);
            return NoContent();
        }

        [HttpGet("type")]
        public IActionResult GetTpye()
        {
            var result = service2.GetAll();
            if (result.Any()) return Ok(result);
            return NoContent();
        }
        [HttpGet("{id}")]
        public IActionResult GetByid(int id)
        {
            var result = service.GetById(id);
            if (result != null) return Ok(result);
            return BadRequest("Student don't exist in DB");
        }

        //[HttpGet("search")]
        //public IActionResult GetByQuery(int groupId, DateTime minBirthdate, DateTime maxBirthdate)
        //{
        //    var result = service.GetAll();
        //    if (result.Any()) return Ok(result);
        //    return BadRequest("Student don't exist in DB");
        //}

        [HttpGet("search")]
        public IActionResult GetByQuery(string? searchString, DateTime? searchDate)
        {
            if(searchString == null || searchDate == null)
            {
                searchString = "";
                searchDate = DateTime.Today;
            }
            var result = service.GetByParameter(searchString, (DateTime)searchDate);
            if (result.Any()) return Ok(result);
            return BadRequest("Item don't exist in DB");
        }
        [HttpPost]
        public IActionResult Post(TattooSticker entity)
        {
            if (!IsValidName(entity.TattooStickerName))
            {
                return BadRequest("TattooStickerName includes a-z, A-Z, /, *, $, #, space and digit 0-9. Each word of the TattooStickerName must begin with the capital letter.");
            }
            if (!IsValidDate((DateTime)entity.ImportDate))
            {
                return BadRequest("ImportDate >=1990 and ImportDate <= current date.");

            }
            var result = service.AddNew(entity);
            if (result) return Ok("Add TattooSticker successful!");
            return BadRequest("Add TattooSticker failed!");
        }

        [HttpPut]
        public IActionResult Put(TattooSticker student)
        {
            var result = service.UpdateEntity(student);
            if (result) return Ok("Update Student successful!");
            return BadRequest("Update Student failed!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = service.DeleteById(id);
            if (result) return Ok("Delete TattooSticker successful!");
            return BadRequest("Delete TattooSticker failed!");
        }

        private bool IsValidName(string name)
        {

            string pattern = "^(?:[A-Z][a-zA-Z0-9*$/# ]*)+$";
            return Regex.IsMatch(name, pattern);
        }
        private bool IsValidDate(DateTime date)
        {
            DateTime minDate = new DateTime(1990, 1, 1);
            DateTime currentDate = DateTime.Now;

            return date >= minDate && date <= currentDate;
        }
    }
}
