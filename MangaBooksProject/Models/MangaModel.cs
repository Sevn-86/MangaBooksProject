using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Models
{
    public class MangaModel
    {


        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Title { get; set; }
        [DataType("Date")]
        public string Releasedate { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Chapters { get; set; }
        public string Genre { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public bool Status { get; set; }
        public string MangaImagePath { get; set; }
        [Display(Name = "Please select the Manga cover photo")]
        public IFormFile MangaImage { get; set; }
        public string fileName { get; set; }
        
    } 
}














