using MangaBooksProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Services
{
    public class SqlMangaData : IMangaData
    {
        private readonly MangaBooksDbContext db;

        public SqlMangaData(MangaBooksDbContext db)
        {
            this.db = db;
        }

        public void Add(Manga manga)
        {
            db.Mangas.Add(manga);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            var manga = db.Mangas.Find(id);
            if (manga != null)
            {
                db.Mangas.Remove(manga);
                db.SaveChanges();
            }
        }

        public IEnumerable<Manga> GetAll()
        {
            var manga = db.Mangas.OrderBy(m => m.Title).ToList();
            return manga;

        }

        public Manga GetById(int id)
        {
            var manga = db.Mangas.Find(id);
            return manga;
        }

        public void Update(Manga manga)
        {
            var entry = db.Entry(manga);
            entry.State = EntityState.Modified;
            db.SaveChanges();

        }

        public IEnumerable<Manga> GetByRating()
        {
            var manga = db.Mangas.Where(m => m.Rating > 3).ToList();
            return manga;
        }

        public IEnumerable<Manga> GetByStatus()
        {
            var manga = db.Mangas.Where(m => m.Status == true).ToList();
            return manga; 
        }

        public IEnumerable<Manga> GetBySearchString(string searchString)
        {
            var manga = db.Mangas.Where(m => m.Title.Contains(searchString)).ToList();
            return manga;
        }
    }
}
