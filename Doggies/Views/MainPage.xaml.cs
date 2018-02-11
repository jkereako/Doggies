using Xamarin.Forms;
using Doggies.ViewModels;
using Doggies.Models;

namespace Doggies
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            MessagingCenter.Subscribe<BreedListViewModel, Breed>(
                this, "ceo.dog.BreedSelectedNotification", PrepareBreedDetailView
            );
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<BreedListViewModel, Breed>(
                this, "ceo.dog.BreedSelectedNotification"
            );
        }

        void PrepareBreedDetailView(BreedListViewModel sender, Breed breed)
        {
            // Dismiss the Master view
            IsPresented = false;

            breedDetailView.viewModel = breed;
        }
    }
}
