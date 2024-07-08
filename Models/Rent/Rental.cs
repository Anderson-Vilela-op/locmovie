
using System.ComponentModel.DataAnnotations;

namespace locmovie.Models.RentalModel
{
    public class Rental
    {
        public int Id {get;set;}

        [Required]
        public int UserId {get;set;}

        [Required]
        public int MovieId {get;set;}

        [Required]
        public DateTime RentalDate {get;set;}

        [Required]
        public DateTime? DueDate {get;set;}
    }
}