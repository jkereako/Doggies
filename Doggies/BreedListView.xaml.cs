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

            System.Diagnostics.Debug.WriteLine(breeds.ToString());
        }
    }
}
