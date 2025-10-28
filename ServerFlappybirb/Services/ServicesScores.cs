using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerFlappybirb.Data;
using ServerFlappybirb.DTOs;
using ServerFlappybirb.Models;
namespace ServerFlappybirb.Services
{
    public class ServicesScores
    {
        private readonly ServerFlappybirbContext _context;
        private readonly UserManager<Users> _userManager;

        public ServicesScores(ServerFlappybirbContext flappyContext, UserManager<Users> UserManager)
        {
            _context = flappyContext;
            _userManager = UserManager;
        }

        public async Task<List<Score>?> GetAll()
        {
            return await _context.Score.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Score>> Create(ScoreDTO scoreDTO)
        {
            // Validate the input DTO (assuming ScoreDTO has properties to map to Score)  
            if (scoreDTO == null)
            {
                return new BadRequestResult();
            }

            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await _userManager.FindByIdAsync(userId);

            // Map ScoreDTO to Score (you may need to adjust this based on ScoreDTO's structure)  
            var score = new Score
            {
                timeInSeconds = scoreDTO.Time,
                scoreValue = scoreDTO.Score
            };

            // Add the new score to the database  
            _context.Score.Add(score);
            await _context.SaveChangesAsync();

            // Return the created score with a 201 Created response  
            return new CreatedAtActionResult("GetAll", "ServicesScores", new { id = score.id }, score);
        }
    }
}
