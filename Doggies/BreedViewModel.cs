using System;


namespace Doggies
{
    public class BreedViewModel
    {
        public BreedViewModel(string breedName) {
            Name = breedName;
        }

        public string Name { get; }
    }
}
