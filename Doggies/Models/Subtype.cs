
namespace Doggies.Models
{
    public class Subtype : IName
    {
        public Subtype(Breed breed, string name)
        {
            Breed = breed;
            Name = name;
        }

        public Breed Breed { get; }
        public string Name { get; }
    }
}
