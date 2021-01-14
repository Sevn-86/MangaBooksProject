using MangaBooksProject.Data;
using MangaBooksProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Data
{
    public class MangaBooksDbContext : DbContext
    {
        public MangaBooksDbContext(DbContextOptions<MangaBooksDbContext> options) : base(options)
        {

        }
        public DbSet<Mangas> Mangas { get; set; }
    }
}
