using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Doggies.ViewModels
{
    public class BreedListViewModel : BaseViewModel
    {
        //-- Properties
        public ObservableCollection<Breed> Breeds
        {
            get { return _breeds; }
        }

        //-- Commands
        public ICommand RowSelectedCommand { get; }

        //-- Private fields
        ObservableCollection<Breed> _breeds;
        ApiClient _client;

        public BreedListViewModel()
        {
            RowSelectedCommand = new Command<Breed>(RowSelected);
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

        void RowSelected(Breed breed)
        {
            // REVIEW: There's gotta be a better way to do this.
            // Notify the main page.
            MessagingCenter.Send<BreedListViewModel, Breed>(
                this, "ceo.dog.BreedSelectedNotification", breed
           );
        }
    }
}
