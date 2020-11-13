using Microsoft.EntityFrameworkCore;
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
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Releasedate { get; set; }
        public string Author { get; set; }
        public int Chapters { get; set; }
        public GenreType Genre {get; set;}
        [Range(1, 5 )]
        public int Rating { get; set; }
        public bool Status { get; set; } 
    }
}



//private DateTime _releasedate;

//public DateTime Releasedate
//{
//    get
//    {
//        return _releasedate;
//    }
//    set
//    {
//        _releasedate.Date.ToShortDateString();
//        _releasedate = value;
//    }
//}



//public int Chapters
//{ 
//    get
//    {
//        return _chapters;
//    }
//    set
//    {
//        _chapters = 5000;

//    }
//}