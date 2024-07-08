
namespace locmovie.Models.RentalModel 
{
    public class RentalRegisterDto
    {
        public required int MovieId {get;set;}
        public required int UserId {get;set;}
        public required DateTime RentalDate {get;set;}
        public required DateTime DueDate {get;set;}

    }
}