using WebApi.Data;
using WebApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class DbSuperHeroController : ControllerBase
    {
        private readonly ISuperHeroRepository SuperHeroRepository;

        public DbSuperHeroController(ISuperHeroRepository SuperHeroRepository)
        {
            this.SuperHeroRepository = SuperHeroRepository;
        }

        /****** Generic Interface Methods *****/
        /**************************************/

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            return Ok(this.SuperHeroRepository.Read());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetOne(int id)
        {
            var tempItem = this.SuperHeroRepository.ReadOne(id);
            if(tempItem == null)
            {
                return BadRequest("No item with this Id in Database");
            }

            return Ok(tempItem);
        }

        /****** Sp Interface Methods *****/
        /*********************************/

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetAllInformation(string info)
        {
            var tempListe = this.SuperHeroRepository.GetAllInformation(info);
            if(tempListe == null)
            {
                return BadRequest("No item with this reference in Database");
  
            }

            return Ok(this.SuperHeroRepository.GetAllInformation(info));
        }

        
        /****** Generic Interface Methods *****/
        /**************************************/

        [HttpPost]
        public ActionResult Add(SuperHero sp)
        {
            var tempItem = this.SuperHeroRepository.Create(sp);
            if(tempItem == null)
            {
                return BadRequest("Item already existing in Database or Duplicate Id");
            }

            return Ok(this.SuperHeroRepository.Create(sp));
        }

        [HttpPut]
        public ActionResult Update(SuperHero sp)
        {
            var tempItem = this.SuperHeroRepository.ReadOne(sp.Id);
            if(tempItem == null)
            {
                return BadRequest("Item doesn't exist in database");
            }

            return Ok(this.SuperHeroRepository.Update(sp));
        }

        [HttpDelete]
        public ActionResult DeleteById(int id)
        {
            var tempItem = this.SuperHeroRepository.Delete(id);

            if(tempItem == null)
            {
                return BadRequest("No item with this Id in Database");
            }

            return Ok(tempItem);
            
        }



    }
}