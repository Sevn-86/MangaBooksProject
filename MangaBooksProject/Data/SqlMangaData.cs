using MangaBooksProject.Mappers;
using MangaBooksProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public async Task<List<MangaModel>> GetAllMangaBooks(string searchString = null)
        {
            var result = GetAllMangas();
            result = FilterByTitle(result, searchString);
 
            var allMangasResult = await result.ToListAsync() ?? Enumerable.Empty<Mangas>();
            var mangaModels = await MapResultsToViewModel(allMangasResult);

            return mangaModels;
        }


        //function to retrieve all manga where Rating > 3
        public async Task<List<MangaModel>> GetPopulairManga(string searchString = null)
        {
            var result = GetAllMangas();
            result = FilterByTitle(result, searchString);
            result = result.Where(x => x.Rating > 3);

            var allMangasResult = await result.ToListAsync() ?? Enumerable.Empty<Mangas>();
            var mangaModels = await MapResultsToViewModel(allMangasResult);

            return mangaModels;
        }

        //function te retrieve all manga where Status == true
        public async Task<List<MangaModel>> GetFinishedManga(string searchString = null)
        {
            var result = GetAllMangas();
            result = FilterByTitle(result, searchString);
            result = result.Where(x => x.Status == true);

            var allMangasResult = await result.ToListAsync() ?? Enumerable.Empty<Mangas>();
            var mangaModels = await MapResultsToViewModel(allMangasResult);

            return mangaModels;
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
            var oldEntry = db.Mangas.FirstOrDefault(x => x.Id.Equals(mangamodel.Id));

            db.Entry(oldEntry).CurrentValues.SetValues(editManga);
            db.SaveChanges();
        }


        private IQueryable<Mangas> GetAllMangas()
        {
            var result = db.Mangas.AsQueryable();
            return result;
        }

        private IQueryable<Mangas> FilterByTitle(IQueryable<Mangas> iQueryable, string searchString = null)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                iQueryable = iQueryable.Where(x => x.Title.Contains(searchString));
            }
            return iQueryable;
        }

        private async Task<List<MangaModel>> MapResultsToViewModel(IEnumerable<Mangas> Mangas)
        {
            var viewModels = new List<MangaModel>();

            foreach (var mangamodel in Mangas)
            {
                viewModels.Add(MangaMapper.toViewModel(mangamodel));
            }
            return viewModels;
        }


    }
}
