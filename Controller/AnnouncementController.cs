using htmos.dtos;
using htmos.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace htmos.controller;

[ApiController]
[Route("api/announcement")]
[Authorize(Policy = "CanWriteBranch")]
public class AnnouncementController(AnnouncementService announcementService) : ControllerBase
{
    [HttpPost("announce")]
    public async Task<IActionResult> Announce(AnnouncementDTO announcement)
    {
        BoolResult result = await announcementService.BroadcastAllAsync(announcement.Message);
        return Ok(result);
    }



}