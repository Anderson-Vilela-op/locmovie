
using System.ComponentModel.DataAnnotations;

namespace locmovie.Models.MovieModel
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get;set; } = string.Empty;

        [Required]
        public string Gender {get;set;} = string.Empty;

        [Required]
        public int Year {get;set;}

        [Required]
        public bool Avaliable {get;set;}

    }
} 