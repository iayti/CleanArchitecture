namespace CleanArchitecture.Application.ExternalServices.OpenWeather.Request
{
    public class OpenWeatherRequest
    {
        public string Q { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public int Id { get; set; }
    }
}