using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Doggies.Models;

namespace Doggies.ViewModels
{
    public class BreedListViewModel : BaseViewModel
    {
        //-- Properties
        public ObservableCollection<IName> Breeds
        {
            get { return _breeds; }
        }

        //-- Commands
        public ICommand RowSelectedCommand { get; }

        //-- Private fields
        ObservableCollection<IName> _breeds;
        ApiClient _client;

        public BreedListViewModel()
        {
            RowSelectedCommand = new Command<IName>(RowSelected);
            _client = new ApiClient();
        }

        public async Task GetBreedsAsync()
        {
            var enumerableBreeds = await _client.GetMasterBreedsAsync();
            var breeds = enumerableBreeds.Select((x) => new Breed(x));
            var observableBreeds = new ObservableCollection<IName>(
                breeds
            );

            SetProperty(ref _breeds, observableBreeds, "Breeds");

            return;
        }

        async void RowSelected(IName name)
        {
            var breed = name as Breed;

            // This should never happen.
            if (breed == null) { return; }

            var subtypeBreeds = await _client.GetSubtypeBreedsAsync(breed.Name);
            var subtypes = subtypeBreeds.Select((x) => new Subtype(breed, x));
            var index = _breeds.IndexOf(breed);

            foreach (var subtype in subtypes)
            {
                _breeds.Insert(index + 1, subtype);
            }

            OnPropertyChanged("Breeds");

            // REVIEW: There's gotta be a better way to do this.
            // Notify the main page.
            // MessagingCenter.Send<BreedListViewModel, Breed>(
            //     this, "ceo.dog.BreedSelectedNotification", breed
            //);
        }
    }
}
