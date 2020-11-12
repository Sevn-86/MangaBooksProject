using MangaBooksProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Services
{
    public interface IMangaData
    {
        IEnumerable<Manga> GetAll();
        Manga GetById(int id);
        void Add(Manga manga);
        void Update(Manga manga);
        void Delete(int id);
        IEnumerable<Manga> GetByRating();
        IEnumerable<Manga> GetByStatus();
        IEnumerable<Manga> GetBySearchString(string searchString);
    }
}
