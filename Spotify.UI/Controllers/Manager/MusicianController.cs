using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Musicians;
using Spotify.Domain.Infrastructure;
using Spotify.Domain.Models;

namespace Spotify.UI.Controllers.Manager
{
    [Route("Manager/[controller]")]
    [Authorize(Policy = "Manager")]
    public class MusicianController : ManagerController
    {
        public MusicianController(UserManager<ApplicationUser> userManager,
            IApplicationUserManager appUserManager) : base(userManager, appUserManager)
        {
        }

        [HttpGet("")]
        public IActionResult GetMusicians([FromServices] GetManagerMusicians getMusicians) 
            => Ok(getMusicians.Execute(GetId()));

        [HttpGet("{id}")]
        public IActionResult GetMusician(int id, [FromServices] GetMusician getMusician)
        {
            if (!IsManagerCorrect(id))
                return BadRequest();

            return Ok(getMusician.Execute(id));
        }

        [HttpPost("")]
        public async Task<IActionResult> AddMusician(
            [FromBody] AddMusician.Request request,
            [FromServices] AddMusician addMusician)
        {
            request.ManagerId = GetId();

            return Ok(await addMusician.Execute(request));
        }
            

        [HttpPut("")]
        public async Task<IActionResult> UpdateMusician(
            [FromBody] UpdateMusician.Request request,
            [FromServices] UpdateMusician updateMusician)
        {
            if(!IsManagerCorrect(request.Id))
                return BadRequest();

            return Ok(await updateMusician.Execute(request));
        }
            

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusician(int id, [FromServices] DeleteMusician deleteMusician)
        {
            if (!IsManagerCorrect(id))
                return BadRequest();

            return Ok(await deleteMusician.Execute(id));
        }
    }
}
