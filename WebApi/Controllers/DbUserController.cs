using WebApi.Data;
using WebApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DbUserController : ControllerBase
    {
        private readonly IUserRepository UserRepository;

        public DbUserController(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(this.UserRepository.Read());
        }

        [HttpGet]
        public ActionResult GetOne(int id)
        {
            var tempItem = this.UserRepository.ReadOne(id);
            if(tempItem == null)
            {
                return BadRequest("No item with this Id in Database");
            }

            return Ok(tempItem);
        }

        [HttpGet]
        public ActionResult GetAllInformation(string info)
        {
            var tempListe = this.UserRepository.GetAllInformation(info);
            if(tempListe == null)
            {
                return BadRequest("No item with this reference in Database");
  
            }

            return Ok(this.UserRepository.GetAllInformation(info));
        }

        [HttpPost]
        public ActionResult Add(User user)
        {
            var tempItem = this.UserRepository.Create(user);
            if(tempItem == null)
            {
                return BadRequest("Item already existing in Database or Duplicate Id");
            }

            return Ok(this.UserRepository.Create(user));
        }

        [HttpPut]
        public ActionResult Update(User user)
        {
            var u = this.UserRepository.ReadOne(user.UserId);
            if(u == null)
            {
                return BadRequest("Item doesn't exist in database");
            }

            return Ok(this.UserRepository.Update(user));
        }

        [HttpDelete]
        public ActionResult DeleteById(int id)
        {
            var tempItem = this.UserRepository.Delete(id);

            if(tempItem == null)
            {
                return BadRequest("No item with this Id in Database");
            }

            return Ok(tempItem);
            
        }
    }

}

