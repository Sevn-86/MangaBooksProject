using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Models
{
    public class Manga
    {
        private DateTime releasedate;

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime Releasedate
        {
            get
            {
                return releasedate;
            }
            set
            {
                releasedate.IsDaylightSavingTime();
                releasedate = value;
            }
        } 
        public string Author { get; set; }
        public string Description { get; set; }
        public int Chapters { get; set; }
        public GenreType Genre { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public bool Status { get; set; }

        //[Display(Name = "Upload a bookart for your manga")]
        //[Required]
        //[NotMapped]
        //public IFormFile MangaImage { get; set; }
    }
}















