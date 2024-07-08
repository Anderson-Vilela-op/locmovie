
using System.ComponentModel.DataAnnotations;
using locmovie.Models.MovieModel;

namespace locmovie.Models.RentalModel
{
    public class RentalDto
    {
        public required int Id {get;set;}
        
        public required int UserId {get;set;}

        public required int MovieId {get;set;}

        public required DateTime RentalDate {get;set;}

        public required DateTime? DueDate {get;set;}
        

    }
}