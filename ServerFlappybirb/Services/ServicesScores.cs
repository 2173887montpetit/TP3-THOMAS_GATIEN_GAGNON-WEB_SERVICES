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

        public ServicesScores(ServerFlappybirbContext flappyContext)
        {
            _context = flappyContext;
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

        public async Task<ActionResult<Score>> Create(Score score)
        {
            if (score == null) return null;

            _context.Score.Add(score);

            await _context.SaveChangesAsync();

            return score;
        }
    }
}
