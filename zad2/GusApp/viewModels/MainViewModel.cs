using System.Collections.ObjectModel;
using GusApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace GusApp.ViewModels;
public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Area> Areas { get; set; }

    public MainViewModel()
    {
        Areas = new ObservableCollection<Area>();
        _ = FetchAreas();
    }
    private async Task FetchAreas()
    {
        var client = new RestClient("https://api-dbw.stat.gov.pl");
        var request = new RestRequest("/api/1.1.0/area/area-area", Method.Get);
        request.AddHeader("Accept", "application/json");

        var response = await client.ExecuteAsync(request);
        if (response.IsSuccessful)
        {
            try
            {
                var areas = JsonConvert.DeserializeObject<List<Area>>(response.Content);

                foreach (var area in areas)
                {
                    Areas.Add(area);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }

        }

    }

    // Implementacja INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
