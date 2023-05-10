using Newtonsoft.Json;
namespace GusApp.Models; public class Area
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("nazwa")]
    public string Nazwa { get; set; }

    [JsonProperty("id-nadrzedny-element")]
    public int IdNadrzednyElement { get; set; }

    [JsonProperty("nazwa-poziom")]
    public string NazwaPoziom { get; set; }
}