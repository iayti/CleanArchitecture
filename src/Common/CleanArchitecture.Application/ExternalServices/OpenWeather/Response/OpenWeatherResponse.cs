using System.Collections.Generic;
using Newtonsoft.Json;

namespace CleanArchitecture.Application.ExternalServices.OpenWeather.Response
{ 
    public class Coord    {
        public double lon { get; set; } 
        public double lat { get; set; } 
    }

    public class Weather    {
        public int id { get; set; } 
        public string main { get; set; } 
        public string description { get; set; } 
        public string icon { get; set; } 
    }

    public class Main    {
        public double temp { get; set; } 
        public double feels_like { get; set; } 
        public double temp_min { get; set; } 
        public double temp_max { get; set; } 
        public int pressure { get; set; } 
        public int humidity { get; set; } 
    }

    public class Wind    {
        public double speed { get; set; } 
        public int deg { get; set; } 
    }

    public class Rain    {
        [JsonProperty(PropertyName = "1h")]
        public double _1h { get; set; } 
        [JsonProperty(PropertyName = "3h")]
        public double _3h { get; set; } 
    }
    
    public class Snow    {
        [JsonProperty(PropertyName = "1h")]
        public double _1h { get; set; } 
        [JsonProperty(PropertyName = "3h")]
        public double _3h { get; set; } 
    }

    public class Clouds    {
        public int all { get; set; } 
    }

    public class Sys    {
        public int type { get; set; } 
        public int id { get; set; } 
        public string country { get; set; } 
        public int sunrise { get; set; } 
        public int sunset { get; set; } 
    }

    public class OpenWeatherResponse    {
        public Coord coord { get; set; } 
        public List<Weather> weather { get; set; } 
        public string @base { get; set; } 
        public Main main { get; set; } 
        public int visibility { get; set; } 
        public Wind wind { get; set; } 
        public Rain rain { get; set; } 
        public Clouds clouds { get; set; }
        public Snow snow { get; set; } 
        public int dt { get; set; } 
        public Sys sys { get; set; } 
        public int timezone { get; set; } 
        public int id { get; set; } 
        public string name { get; set; } 
        public int cod { get; set; } 
    }
}