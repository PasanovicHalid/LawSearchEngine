namespace LawSearchEngine.Presentation.Contracts.GetWeatherForecast
{
    internal class GetWeatherForecastRequest
    {
        public int Days { get; set; }
        public int BottomTemperature { get; set; }
        public int TopTemperature { get; set; }
    }
}
