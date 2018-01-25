﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class CurrentCondidionsDisplay : IObserver
    {
        public decimal Temperature { get; private set; }
        public decimal Humidity { get; private set; }
        public ISubject WeatherData { get; private set; }

        public CurrentCondidionsDisplay(ISubject weatherData)
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
