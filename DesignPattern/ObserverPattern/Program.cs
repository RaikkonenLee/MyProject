using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ObserverPattern
{
    class WeatherStation
    {
        static void Main(string[] args)
        {
            //定義主題
            WeatherData weatherData = new WeatherData();
            //定義觀察者1(假設顯示在APP)
            CurrentConditionsDisplay currentDisplay = 
                new CurrentConditionsDisplay(weatherData);
            //定義觀察者2(假設顯示在網站上)
            StatisticsDisplay statisticsDisplay = 
                new StatisticsDisplay(weatherData);
            //
            //給予值去自動更新觀察者的狀態
            weatherData.SetMeasurements(80M, 65M, 30.4M);
            weatherData.SetMeasurements(82M, 70M, 29.2M);
        }
    }

    public class CurrentConditionsDisplay : IObserver
    {
        public decimal Temperature { get; private set; }
        public decimal Humidity { get; private set; }
        public ISubject WeatherData { get; private set; }

        public CurrentConditionsDisplay(ISubject weatherData)
        {
            WeatherData = weatherData;
            WeatherData.RegisterObserver(this);
        }

        public void Update(decimal temperature, decimal humidity, decimal pressure)
        {
            Temperature = temperature;
            Humidity = humidity;
            Display();
        }

        public void Display()
        {
            Console.WriteLine("Current contitions: " + Temperature + 
                "F degrees and " + Humidity + "% humidity");
        }
    }

    public class StatisticsDisplay : IObserver
    {
        public decimal Temperature { get; private set; }
        public decimal Humidity { get; private set; }
        public decimal Pressure { get; set; }

        public ISubject WeatherData { get; private set; }

        public StatisticsDisplay(ISubject weatherData)
        {
            WeatherData = weatherData;
            WeatherData.RegisterObserver(this);
        }

        public void Update(decimal temperature, decimal humidity, decimal pressure)
        {
            Temperature = temperature;
            Humidity = humidity;
            Pressure = pressure;
            Display();
        }

        public void Display()
        {
            Console.WriteLine("Current contitions: " + Temperature +
                "F degrees and " + Humidity + "% humidity and " + Pressure + " pressure ");
        }
    }

    public class WeatherData : ISubject
    {
        public List<IObserver> ObserverList { get; private set; }
        public decimal Temperature { get; private set; }
        public decimal Humidity { get; private set; }
        public decimal Pressure { get; private set; }

        public WeatherData()
        {
            ObserverList = new List<IObserver>();
        }

        public void RegisterObserver(IObserver observer)
        {
            ObserverList.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            ObserverList.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in ObserverList)
            {
                observer.Update(Temperature, Humidity, Pressure);
            }
        }

        public void MeasurementsChange()
        {
            NotifyObservers();
        }

        public void SetMeasurements(decimal temperature, decimal humidity, decimal pressure)
        {
            Temperature = temperature;
            Humidity = humidity;
            Pressure = pressure;
            MeasurementsChange();
        }

        public void GetMeasurements()
        {
            
        }
    }
    
    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
        void SetMeasurements(decimal temperature, decimal humidity, decimal pressure);
        void GetMeasurements();
    }

    public interface IObserver
    {
        void Update(decimal temperature, decimal humidity, decimal pressure);
    }
}
