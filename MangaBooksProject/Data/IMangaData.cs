using MangaBooksProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Services
{
    public interface IMangaData
    {
        IEnumerable<MangaModel> GetAll();
        MangaModel GetById(int id);
        void Add(MangaModel manga);
        void Update(MangaModel manga);
        void Delete(int id);
        IEnumerable<MangaModel> GetByRating();
        IEnumerable<MangaModel> GetByStatus();
        IEnumerable<MangaModel> GetBySearchString(string searchString);
    }
}
