using Microsoft.EntityFrameworkCore;
using ServerFlappybirb.Data;
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


        public async Task<List<Score>?> GetAll()
        {
            return await _context.Score.ToListAsync();
        }
    }
}
