using MangaBooksProject.Data;
using MangaBooksProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Mappers
{
    public static class MangaMapper
    {
        //function to return new Manga object
        public static Mangas toAggregate(MangaModel mangamodel)
        {
            var result = new Mangas()
            {   
                Id = mangamodel.Id,
                Title = mangamodel.Title,
                Author = mangamodel.Author,
                Chapters = mangamodel.Chapters,
                Genre = mangamodel.Genre,
                Releasedate = mangamodel.Releasedate,
                Status = mangamodel.Status,
                Description = mangamodel.Description,
                Rating = mangamodel.Rating,
                MangaImagePath = mangamodel.fileName,
            };

            return result;
        }
        //function to return new MangaModel object
        public static MangaModel toViewModel(Mangas mangamodel)
        {
            var result = new MangaModel()
            {
                Id = mangamodel.Id,
                Title = mangamodel.Title,
                Author = mangamodel.Author,
                Chapters = mangamodel.Chapters,
                Genre = mangamodel.Genre,
                Releasedate = mangamodel.Releasedate,
                Status = mangamodel.Status,
                Description = mangamodel.Description,
                Rating = mangamodel.Rating,
                fileName = mangamodel.MangaImagePath
            };

            return result;
        }


    }
}
