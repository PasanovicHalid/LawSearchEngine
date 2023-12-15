using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Presentation.Contracts.GetWeatherForecast
{
    internal class GetWeatherForecastRequest
    {
        public int Days { get; set; }
        public int BottomTemperature { get; set; }
        public int TopTemperature { get; set; }
    }
}
