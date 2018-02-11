using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Doggies
{
    public partial class BreedListPage : ContentPage
    {
        public BreedListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.GetBreedsAsync();
        }

        #region Event Handlers
        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            // Check for deselection
            if (e.SelectedItem == null)
            {
                // ItemSelected is invoked on both selection and deselection.
                // `e.SelectedItem` is null if the row has been deselected.
                //
                // "Be aware that ItemSelected is called both when items are 
                // deselected and when they are selected. That means you'll need
                // to check for null SelectedItem in your ItemSelected event 
                // handler before you can use it:"
                //
                // see: https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/interactivity/#Selection_Taps
                return;
            }

            var listView = sender as ListView;
            var viewModel = e.SelectedItem as Breed;

            // Deselect the row
            listView.SelectedItem = null;
            MessagingCenter.Send<BreedListPage, Breed>(
                this, "ceo.dog.BreedSelectedNotification", viewModel
            );
        }
        #endregion Event Handlers
    }
}
