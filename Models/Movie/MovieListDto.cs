namespace locmovie.Models.MovieModel
{
    public class MovieListDto
    {
        public required int Id {get;set;}
        public required string Title {get;set;}
        public required string Gender {get;set;}
        public required int Year {get;set;}
        public required bool Avaliable {get;set;}
    }
}