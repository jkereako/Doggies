using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doggies.ViewModels
{
    public class BreedListViewModel : BaseViewModel
    {
        //-- Properties
        public ObservableCollection<Breed> Breeds
        {
            get { return _breeds; }
        }

        //-- Private fields
        ObservableCollection<Breed> _breeds;
        ApiClient _client;

        public BreedListViewModel()
        {
            _client = new ApiClient();

        }

        public async Task GetBreedsAsync()
        {
            var enumerableBreeds = await _client.GetMasterBreedsAsync();
            var breeds = enumerableBreeds.Select((x) => new Breed(x));
            var observableBreeds = new ObservableCollection<Breed>(
                breeds
            );

            SetProperty(ref _breeds, observableBreeds, "Breeds");

            return;
        }
    }
}
