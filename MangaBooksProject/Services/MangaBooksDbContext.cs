using MangaBooksProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Services
{
    public class MangaBooksDbContext : DbContext
    {
        public MangaBooksDbContext(DbContextOptions<MangaBooksDbContext> options) : base(options)
        {

        }
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<File> Files { get; set; }
    }
}
