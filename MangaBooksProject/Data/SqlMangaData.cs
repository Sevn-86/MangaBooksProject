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

        //adds new manga to the database
        public async Task<int> Add(MangaModel mangamodel)
        {
            var newManga = MangaMapper.toAggregate(mangamodel);
            await db.Mangas.AddAsync(newManga);
            await db.SaveChangesAsync();
            return newManga.Id;
        }

        //retrieves all mangas from the database 
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


        //function to retrieve all manga where Rating > 3
        public async Task<List<MangaModel>> GetPopulairManga(string searchString = null)
        {
            var mangas = new List<MangaModel>();
            var allmanga = new List<Mangas>();

            if (!string.IsNullOrEmpty(searchString))
            {
                allmanga = await db.Mangas.Where(x => x.Title.Contains(searchString)).ToListAsync();
            }
            else
            {
                allmanga = await db.Mangas.Where(x => x.Rating > 3).ToListAsync();
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

        //function te retrieve all manga where Status == true
        public async Task<List<MangaModel>> GetFinishedManga(string searchString = null)
        {
            var mangas = new List<MangaModel>();
            var allmanga = new List<Mangas>();

            if (!string.IsNullOrEmpty(searchString))
            {
                allmanga = await db.Mangas.Where(x => x.Title.Contains(searchString)).ToListAsync();
            }
            else
            {
                allmanga = await db.Mangas.Where(x => x.Status == true).ToListAsync();
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



        //returns manga By Id parameter
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

        //Function to delete manga from database
        public void Delete(int id)
        {
            var manga = db.Mangas.Find(id);
            if (manga != null)
            {
                db.Mangas.Remove(manga);
                db.SaveChanges();
            }
        }

        //function to update manga from database
        public void Update(MangaModel mangamodel)
        {

            var editManga = MangaMapper.toAggregate(mangamodel);
            var oldEntry = db.Mangas.FirstOrDefault(x=>x.Id.Equals(mangamodel.Id));

            db.Entry(oldEntry).CurrentValues.SetValues(editManga);
            db.SaveChanges();
        }
    }
}
