using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Models
{
    public class Manga
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime Releasedate { get; set; }
        public string Author { get; set; }
        public int Chapters { get; set; }
        public GenreType Genre {get; set;}
        [Range(1, 5 )]
        [Required]
        public int Rating { get; set; }
        public bool Status { get; set; } 
    }
}
