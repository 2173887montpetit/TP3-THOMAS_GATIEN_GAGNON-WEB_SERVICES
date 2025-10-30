using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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

        public ServicesScores(ServerFlappybirbContext flappyContext, UserManager<Users> userManager)
        {
            _context = flappyContext;
            _userManager = userManager;
        }

        public async Task<ActionResult<IEnumerable<PublicScoreDTO>>> GetAll()
        {
            var topScores = await _context.Score
                .Where(s => s.isPublic) 
                .OrderByDescending(s => s.scoreValue)
                .Take(10)
                .Select(s => new PublicScoreDTO
                {
                    Pseudo = s.pseudo,
                    Value = s.scoreValue,
                    TempsEnSecondes = s.timeInSeconds,
                    Date = s.date
                })
                .ToListAsync();

            return topScores;
        }
        public async Task<List<MyScoreDTO>> GetMyScoresAsync(string username)
        {
            var MyScores = await _context.Score
                .Where(s => s.pseudo == username)
                .OrderByDescending(s => s.scoreValue)
                .Select(s => new MyScoreDTO
                {
                    scoreValue = s.scoreValue,
                    timeInSeconds = s.timeInSeconds,
                    date = s.date,
                    isPublic = s.isPublic
                })
                .ToListAsync();
            return MyScores;
        }

        public async Task<ActionResult<Score>> Create(Score score)
        {
            if (score == null) return null;

            _context.Score.Add(score);

            await _context.SaveChangesAsync();

            return score;
        }
    }
}
