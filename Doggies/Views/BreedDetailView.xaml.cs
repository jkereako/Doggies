using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Doggies
{
    public partial class BreedDetailView : ContentPage
    {
        public Breed viewModel
        {
            set
            {
                name.Text = value.Name;
            }
        }

        public BreedDetailView()
        {
            InitializeComponent();
        }
    }
}
