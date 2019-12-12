using PokeApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace PokeApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        private static HttpClient HttpClient
           = new HttpClient() { BaseAddress = new Uri("https://pokeapi.co/api/v2/") };

        public ObservableCollection<Result> Results { get; set; }

        public MainPage()
        {
            InitializeComponent();
            GetPokemon();
            BindingContext = this;
        }

        public async void GetPokemon()
        {
            var pokemonResponse = await HttpClient.GetAsync("pokemon");
            pokemonResponse.EnsureSuccessStatusCode();
            var responseBody = await pokemonResponse.Content.ReadAsStringAsync();
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(responseBody);
            Results = new ObservableCollection<Result>(pokemon.Results);
        }
    }
}
