using MangaBooksProject.Mappers;
using MangaBooksProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Data
{
    public class SqlMangaData : IMangaData
    {
        private readonly MangaBooksDbContext db;
        public SqlMangaData(MangaBooksDbContext db)
        {
            this.db = db;
        }

        public async Task<int> Add(MangaModel mangamodel)
        {
            var newManga = MangaMapper.toAggregate(mangamodel);
            await db.Mangas.AddAsync(newManga);
            await db.SaveChangesAsync();
            return newManga.Id;
        }

        public async Task<List<MangaModel>> GetAll(string searchString = null)
        {
            var mangas = new List<MangaModel>();
            var allmanga = new List<Mangas>();

            if (!string.IsNullOrEmpty(searchString))
            {
                allmanga = await db.Mangas.Where(x => x.Title.Contains(searchString)).ToListAsync();
            }
            else
            {
                allmanga = await db.Mangas.ToListAsync();
            }


            if (allmanga?.Any() == true)
            {
                foreach (var mangamodel in allmanga)
                {
                    mangas.Add(MangaMapper.toViewModel(mangamodel));
                }
            }
            return mangas;
        }


        public async Task<MangaModel> GetById(int Id)
        {
            var mangamodel = await db.Mangas.FindAsync(Id);

            if (mangamodel != null)
            {
                var mangaDetails = MangaMapper.toViewModel(mangamodel);
                return mangaDetails;
            }
            return null;
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

        public void Update(MangaModel mangamodel)
        {

            var editManga = MangaMapper.toAggregate(mangamodel);
            var oldEntry = db.Mangas.FirstOrDefault(x=>x.Id.Equals(mangamodel.Id));

            db.Entry(oldEntry).CurrentValues.SetValues(editManga);
            //entry.State = EntityState.Modified;
            db.SaveChanges();
        }



        public string GetBySearchString(string searchString)
        {
            var mangas = from m in db.Mangas
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                mangas = mangas.Where(s => s.Title.Contains(searchString));
            }
            return searchString;
        }


        public IEnumerable<Mangas> GetMangasBySearchString(string searchString)
        {
            var mangas = from m in db.Mangas
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                mangas = mangas.Where(s => s.Title.Contains(searchString));
            }
            return mangas;
        }



        //public string GetBySearchString(string searchString)
        //{
        //    var manga = db.Mangas.Where(m => m.Title.Contains(searchString) || searchString == null).OrderBy(m => m.Title).ToList();
        //    return searchString;
        //}






    }
}
