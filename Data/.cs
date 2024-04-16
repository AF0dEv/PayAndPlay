using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayAndPlay.Models;

namespace PayAndPlay.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet
        public DbSet<DJ> Tdjs { get; set; }
        public DbSet<Utilizador> Tutilizadores { get; set; }
        public DbSet<Perfil> Tperfis { get; set; }
        public DbSet<Pedido> Tpedidos { get; set; }
        public DbSet<PlayList> TplayLists{ get; set; }
        public DbSet<Musica> Tmusicas { get; set; }
        public DbSet<MusicaInPlayList> TmusicaInPlayLists { get; set; }
    }
}
