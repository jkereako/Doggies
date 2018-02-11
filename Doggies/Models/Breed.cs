using System;

namespace Doggies.Models
{
    public class Breed : IName
    {
        public Breed(string breedName)
        {
            Name = breedName;
        }

        public string Name { get; }
    }
}
