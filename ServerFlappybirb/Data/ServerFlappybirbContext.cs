using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerFlappybirb.Models;

namespace ServerFlappybirb.Data
{
    public class ServerFlappybirbContext : IdentityDbContext<Users>
    {
        public ServerFlappybirbContext(DbContextOptions<ServerFlappybirbContext> options) : base(options) { 
        
        }

        public DbSet<ServerFlappybirb.Models.Score> Score { get; set; } = default!;
    }
}
