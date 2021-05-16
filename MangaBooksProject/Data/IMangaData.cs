using MangaBooksProject.Models;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MangaBooksProject.Data
{
    public interface IMangaData
    {
        Task<MangaModel> GetById(int Id);
        Task<int> Add(MangaModel mangamodel);
        Task <List<MangaModel>> GetAllMangas(string searchString = null);
        Task<List<MangaModel>> GetPopulairManga(string searchString = null);
        Task<List<MangaModel>> GetFinishedManga(string searchString = null);
        void Update(MangaModel model);
        void Delete(int id);
       
    }
}
