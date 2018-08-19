using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XamarinLatinoMaps.Models;

namespace XamarinLatinoMaps
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            MapView.MoveToRegion(
               MapSpan.FromCenterAndRadius(
                   new Position(37.379672, -5.994488), Distance.FromKilometers(2)));

        }


        private void Street_OnClicked(object sender, EventArgs e)
        {
            MapView.MapType = MapType.Street;
        }


        private void Hybrid_OnClicked(object sender, EventArgs e)
        {
            MapView.MapType = MapType.Hybrid;
        }

        private void Satellite_OnClicked(object sender, EventArgs e)
        {
            MapView.MapType = MapType.Satellite;
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 30;

            var position1 = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            LogitudeLabel.Text = position1.Longitude.ToString();

            LatitudeLabel.Text = position1.Latitude.ToString();

            MapView.MoveToRegion(
               MapSpan.FromCenterAndRadius(
                   new Position(position1.Latitude, position1.Longitude), Distance.FromKilometers(0.5)));

            var pin = new Pin()
            {
                Position = new Position(position1.Latitude, position1.Longitude),
                Label = "Hola amigos",
                Address = "Hola amigos 2",
                Type = PinType.Place,
                Id = "Xamarin1",
               


            };
            MapView.Pins.Add(pin);

        }

        private async void LoadApi(object sender, EventArgs e)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://restcountries.eu");
            var url = "/rest/v2/all";


            var response = await client.GetAsync(url);


            var answer = await response.Content.ReadAsStringAsync();


            var list2 = JsonConvert.DeserializeObject<List<Land>>(answer);


            List<Land> solucion = new List<Land>(list2);

            Land tierra = solucion[0];

            var pin = new Pin()
            {
                Position = new Position(tierra.Latlng[0], tierra.Latlng[1]),
                Label = tierra.Name,
                Address = tierra.Capital,
                Type = PinType.Place,
                Id = "Xamarin",
               

            };
            MapView.Pins.Add(pin);

        }


        private async void FullLoadApi(object sender, EventArgs e)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://restcountries.eu");
            var url = "/rest/v2/all";


            var response = await client.GetAsync(url);


            var answer = await response.Content.ReadAsStringAsync();


            var list2 = JsonConvert.DeserializeObject<List<Land>>(answer);


            List<Land> solucion = new List<Land>(list2);

            Land tierra;

            var pin = new Pin()
            {
                Position = new Position(0, 0),
                Label = "Pin de prueba",
                Address = "Dirección de prueba",
                Type = PinType.Place,
                Id = "Xamarin",
                

            };

            for (int i = 0; i < 30; i++)
            {

                tierra = solucion[i];


                pin = new Pin()
                {
                    Position = new Position(tierra.Latlng[0], tierra.Latlng[1]),
                    Label = tierra.Name,
                    Address = tierra.Capital,
                    Type = PinType.Place,
                    Id = "Xamarin",
                   

                };
                MapView.Pins.Add(pin);

            }

        }
    }
}
