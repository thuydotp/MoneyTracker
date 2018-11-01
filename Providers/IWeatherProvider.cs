using System.Collections.Generic;
using MoneyTracker.Models;

namespace MoneyTracker.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
