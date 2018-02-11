using Xamarin.Forms;

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

            MessagingCenter.Subscribe<BreedListPage, Breed>(
                this, "ceo.dog.BreedSelectedNotification", OnBreedSelection
            );
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<BreedListPage, Breed>(
                this, "ceo.dog.BreedSelectedNotification"
            );
        }

        void OnBreedSelection(BreedListPage sender, Breed viewModel)
        {
            // Dismiss the Master view
            IsPresented = false;

            breedDetailView.viewModel = viewModel;
        }
    }
}
