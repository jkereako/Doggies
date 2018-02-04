using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Doggies
{
    public partial class BreedListView : ContentPage
    {
        public BreedListView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var client = new ApiClient();
            var breeds = await client.GetMasterBreedsAsync();
            var viewModels = new List<BreedViewModel>();

            foreach (string breed in breeds)
            {
                var viewModel = new BreedViewModel(breed);
                viewModels.Add(viewModel);
            }

            breedListView.ItemsSource = viewModels;
        }
    }
}
