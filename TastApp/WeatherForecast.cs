using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace TastApp
{
    
    

    [DataContract]
    public class Root
    {
        [DataMember(Name = "now")]
        public long Now { get; set; }
        [DataMember(Name = "now_dt")]
        public string NowDt { get; set; }
        [DataMember(Name = "info")]
        public Info Info { get; set; }
        [DataMember(Name = "fact")]
        public Fact Fact { get; set; }
        [DataMember(Name = "forecast")]
        public Forecast Forecast { get; set; }
    }

    [DataContract(Name = "fact")]
    public class Fact
    {
        private static readonly Dictionary<string, string> ConditionWeatherDict = new Dictionary<string, string>()
        {
            ["clear"] = "ясно.",
            ["partly-cloudy"] = "малооблачно.",
            ["cloudy"] = "облачно с прояснениями.",
            ["overcast"] = "пасмурно.",
            ["drizzle"] = "морось.",
            ["light-rain"] = "небольшой дождь.",
            ["rain"] = "дождь.",
            ["moderate-rain"] = "умеренно сильный дождь.",
            ["heavy-rain"] = "сильный дождь.",
            ["continuous-heavy-rain"] = "длительный сильный дождь.",
            ["showers"] = "ливень.",
            ["wet-snow"] = "дождь со снегом.",
            ["light - snow"] = "небольшой снег.",
            ["snow"] = "снег.",
            ["snow-showers"] = "снегопад.",
            ["hail"] = "град.",
            ["thunderstorm"] = "гроза.",
            ["thunderstorm-with-rain"] = "дождь с грозой.",
            ["thunderstorm-with-hail"] = "гроза с градом."
        };

        private static readonly Dictionary<string, string> WindirDict = new Dictionary<string, string>()
        {
            ["nw"] = "северо -западное.",
            ["n"] = "северное.",
            ["ne"] = "северо-восточное.",
            ["e"] = "восточное.",
            ["se"] = "юго-восточное.",
            ["s"] = "южное.",
            ["sw"] = "юго-западное.",
            ["w"] = "западное.",
            ["с"] = "штиль.",

        };
       
        [DataMember(Name = "temp")]
        public double Temp { get; set; }
        [DataMember(Name = "feels_like")]
        public double FeelsLike { get; set; }
        [DataMember(Name = "temp_water")]
        public string TempWater  { get; set;}         
        [DataMember(Name = "condition")]
        public string Condition { get; set; }
        [DataMember(Name = "wind_speed")]
        public double WindSpeed { get; set; }
        [DataMember(Name = "wind_gust")]
        public double WindGust { get; set; }
        [DataMember(Name = "wind_dir")]
        public string WindDir { get; set; }
        [DataMember(Name = "pressure_mm")]
        public double PressureMm { get; set; }

        public void GetCyrillicProperties()
        {
            Condition=(ConditionWeatherDict.ContainsKey(Condition))?(ConditionWeatherDict[Condition]):(Condition);
            WindDir = (WindirDict.ContainsKey(WindDir)) ? (WindirDict[WindDir]) : (WindDir);

        }
    }



    [DataContract]
    public class Forecast
    {
        [DataMember(Name = "data")]
        public string Date { get; set; }
        [DataMember(Name = "date_ts")]
        public long DateTs { get; set; }
        [DataMember(Name = "week")]
        public long Week { get; set; }
        [DataMember(Name = "sunrise")]
        public string Sunrise { get; set; }
        [DataMember(Name = "sunset")]
        public string Sunset { get; set; }
        [DataMember(Name = "moon_code")]
        public long MoonCode { get; set; }
        [DataMember(Name = "moon_text")]
        public string MoonText { get; set; }
        [DataMember(Name = "parts")]
        public List<Part> Parts { get; set; }
    }

    [DataContract]
    public class Part
    {
        //[DataMember(Name = "part_name")]
        public string PartName { get; set; }
        public long TempMin { get; set; }
        public long TempMax { get; set; }
        public long TempAvg { get; set; }
        public long FeelsLike { get; set; }
        public string Icon { get; set; }
        public string Condition { get; set; }
        public string Daytime { get; set; }
        public bool Polar { get; set; }
        public double WindSpeed { get; set; }
        public long WindGust { get; set; }
        public string WindDir { get; set; }
        public long PressureMm { get; set; }
        public long PressurePa { get; set; }
        public long Humidity { get; set; }
        public long PrecMm { get; set; }
        public long PrecPeriod { get; set; }
        public long PrecProb { get; set; }
    }
    [DataContract]
    public class Info
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Url { get; set; }
    }
       

}
