using AngebotListe.Mobile.Models;
using AngebotListe.Mobile.Services;
using System.Collections.ObjectModel;

namespace AngebotListe.Mobile.Views;

public partial class AngebotListePage : ContentPage
{
    private readonly ObservableCollection<Angebot> _items = new();

    public AngebotListePage()
    {
        InitializeComponent();
        cvAngebote.ItemsSource = _items;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Lade();
    }

    private async Task Lade()
    {
        var list = await DataService.GetAngeboteAsync();
        _items.Clear();
        foreach (var a in list) _items.Add(a);
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Lade();
        refreshView.IsRefreshing = false;
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Angebot a) return;
        cvAngebote.SelectedItem = null;
        await Navigation.PushAsync(new AngebotDetailPage(a));
    }
}
