using System.ComponentModel.DataAnnotations;

namespace Motorcycles.WebAPI
{
    public class Motorcycle
    {
        public string? Make { get; set; }

        public string? Model { get; set; }

        public int Year { get; set; }
    }
}
