using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App39
{
    public partial class MainPage : ContentPage
    {
        private object nameEntry;
        private object subscribed;

        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        
        {
            //if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            if (!string.IsNullOrWhiteSpace(studentname.Text))
            {
                await App.Database.SavePersonAsync(new Person
                {
                    Name = studentname.Text,
                    Subscribed = correct.IsChecked
                });
                 

                studentname.Text= string.Empty;
                correct.IsChecked = false;

                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            }
        }
        Person lastSelection;
        void collectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            lastSelection = e.CurrentSelection[0] as Person;

            studentname.Text = lastSelection.Name;
            correct.IsChecked = lastSelection.Subscribed;
        }

        // Update
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (lastSelection != null)
            {
                lastSelection.Name = studentname.Text;
                lastSelection.Subscribed = correct.IsChecked;

                await App.Database.UpdatePersonAsync(lastSelection);

                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            }
        }
        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            if (lastSelection != null)
            {
                await App.Database.DeletePersonAsync(lastSelection);

                collectionView.ItemsSource = await App.Database.GetPeopleAsync();

                studentname.Text = "";
                correct.IsChecked = false;
            }
        }

        // Get subscribed
        async void Button_Clicked_2(System.Object sender, System.EventArgs e)
        {
            collectionView.ItemsSource = await App.Database.QuerySubscribedAsync();
        }

        // Get Not Subscribed
        async void Button_Clicked_3(System.Object sender, System.EventArgs e)
        {
            collectionView.ItemsSource = await App.Database.LinqNotSubscribedAsync();
        }
    }
}
