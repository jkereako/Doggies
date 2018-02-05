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

            MessagingCenter.Subscribe<BreedListView, BreedViewModel>(
                this, "ceo.dog.BreedSelectedNotification", OnBreedSelection
            );
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<BreedListView, BreedViewModel>(
                this, "ceo.dog.BreedSelectedNotification"
            );
        }

        void OnBreedSelection(BreedListView sender, BreedViewModel viewModel)
        {
            DisplayAlert("Item Selected", viewModel.Name, "OK");
        }

    }
}
