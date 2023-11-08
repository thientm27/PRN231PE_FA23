using BusinessLogic.Token;
using BusinessObjects;
using BusinessObjects.Models;
using ControllerAPI.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/Manage")]
    [ApiController]
    [Authorize(Roles = EnumClass.RoleNames.Manager)]
    public class TattooStikerController : ControllerBase
    {
        ITattooStickerRepo service;

        public TattooStikerController(ITattooStickerRepo serviceParam)
        {
            service = serviceParam;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = service.GetAll();
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

        [HttpPost]
        public IActionResult Post(TattooSticker entity)
        {
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
    }
}
