using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerFlappybirb.Models;

namespace ServerFlappybirb.Data
{
    public class ServerFlappybirbContext : DbContext
    {
        public ServerFlappybirbContext (DbContextOptions<ServerFlappybirbContext> options)
            : base(options)
        {
        }

        public DbSet<ServerFlappybirb.Models.Score> Score { get; set; } = default!;
    }
}
