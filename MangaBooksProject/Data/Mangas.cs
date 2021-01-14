using MangaBooksProject.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MangaBooksProject.Data
{

    public class Mangas
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Releasedate { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Chapters { get; set; }
        public string Genre { get; set; }
        public int Rating { get;  set; }
        public bool Status { get; set; }
        public string MangaImagePath { get; set; }
    }
}

