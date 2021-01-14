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
        Task <List<MangaModel>> GetAll();
        void Update(MangaModel model);
        void Delete(int id);
        string GetBySearchString(string searchString); 
    }
}
