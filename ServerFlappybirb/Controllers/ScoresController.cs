using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerFlappybirb.Data;
using ServerFlappybirb.DTOs;
using ServerFlappybirb.Models;
using ServerFlappybirb.Services;

namespace ServerFlappybirb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly ServerFlappybirbContext _context;
        private readonly ServicesScores _services;
        private readonly UserManager<Users> _userManager;

        public ScoresController(ServerFlappybirbContext context, ServicesScores services, UserManager<Users> userManager )
        {
            _context = context;
            _services = services;
            _userManager = userManager;
        }


        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(ScoreDTO scoreDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new { Message = "Utilisateur non valide ou non authentifié" });

            Users? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Unauthorized(new { Message = "Utilisateur non valide ou non authentifié" });

            Score score = new Score
            {
                pseudo = user.UserName,
                date = DateTime.Now,
                timeInSeconds = scoreDTO.Time,
                scoreValue = scoreDTO.Score,
                isPublic = true
            };

            var result = await _services.Create(score);

            if (result == null || result.Value == null)
                return BadRequest(new { Message = "Erreur lors de l'enregistrement du score" });

            return Ok(result.Value);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MyScoreDTO>>> GetMyScores()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            var scores = await _services.GetMyScoresAsync(user.UserName);
            return Ok(scores);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PublicScoreDTO>>> GetPublicScores()
        {
            var scores = await _services.GetAll();

            return Ok(scores);
        }
    }
}

