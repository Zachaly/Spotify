﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotify.Application.Admin.Musicians;

namespace Spotify.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class MusicianController : Controller
    {
        [HttpGet("")]
        public IActionResult GetMusicians([FromServices] GetMusicians getMusicians) => Ok(getMusicians.Execute());

        [HttpGet("{id}")]
        public IActionResult GetMusician(int id, [FromServices] GetMusician getMusician) => Ok(getMusician.Execute(id));

        [HttpPost("")]
        public async Task<IActionResult> AddMusician(
            [FromBody] AddMusician.Request request,
            [FromServices] AddMusician addMusician)
            => Ok(await addMusician.Execute(request));

        [HttpPut("")]
        public async Task<IActionResult> UpdateMusician(
            [FromBody] UpdateMusician.Request request,
            [FromServices] UpdateMusician updateMusician)
            => Ok(await updateMusician.Execute(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusician(int id, [FromServices] DeleteMusician deleteMusician)
            => Ok(await deleteMusician.Execute(id));
    }
}
