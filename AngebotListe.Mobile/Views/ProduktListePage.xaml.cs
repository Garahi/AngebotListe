using AngebotListe.Mobile.Models;
using AngebotListe.Mobile.Services;
using System.Collections.ObjectModel;

namespace AngebotListe.Mobile.Views;

public partial class ProduktListePage : ContentPage
{
    private readonly ObservableCollection<Product> _items = new();

    public ProduktListePage()
    {
        InitializeComponent();
        cvProdukte.ItemsSource = _items;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Lade();
    }

    private async Task Lade(string? search = null)
    {
        var list = await DataService.GetProductsAsync(search);
        _items.Clear();
        foreach (var p in list) _items.Add(p);
    }

    private async void OnSucheChanged(object sender, TextChangedEventArgs e)
    {
        await Lade(e.NewTextValue);
    }

    private async void OnRefreshing(object sender, EventArgs e)
    {
        await Lade(txtSuche.Text);
        refreshView.IsRefreshing = false;
    }
}
