using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TastApp.models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Newtonsoft.Json.Linq;

namespace TastApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly RequestRecordContext _context;

        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions jsonSerializerOptionsCyr;
       
        private const int YaReqestTimeoutMS = 100;

        private const string YandexXApiKey = "4b6dd7e6-901c-4162-ad80-659e048af33e";

        private static readonly Dictionary<string, string> _cityRequest = new Dictionary<string, string>()
          {
              ["Krasnodar"] = "https://api.weather.yandex.ru/v2/forecast?lat=45.040235&lon=45.040235&lang=ru_RU",
              ["Moscow"] = "https://api.weather.yandex.ru/v2/forecast?lat=55.753878&lon=37.620373&lang=ru_RU",
              ["Orengurg"] = "https://api.weather.yandex.ru/v2/forecast?lat=51.787519&lon=55.101737&lang=ru_RU",
              ["St.Peretburg"] = "https://api.weather.yandex.ru/v2/forecast?lat=59.939125&lon=30.315822&lang=ru_RU",
              ["Kaliningrad"] = "https://api.weather.yandex.ru/v2/forecast?lat=54.707321&lon=20.507245&lang=ru_RU"
          };


        public WeatherForecastController(RequestRecordContext context)
        {
            _context = context;
            _httpClient=new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-Yandex-API-Key", YandexXApiKey);
            //_httpClient.Timeout=TimeSpan.FromMilliseconds(YaReqestTimeoutMS);      // таймаут запроса к яндексу

            jsonSerializerOptionsCyr=new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
        }
      
        [HttpGet("{cityName}")]
        
        public async Task<string> GetCity(string cityName)
        {
            if (!_cityRequest.ContainsKey(cityName)) return "BadCityName";
            try
            {
                string requestUri = _cityRequest[cityName];
                using (HttpResponseMessage responseYa =(await _httpClient.GetAsync(requestUri)).EnsureSuccessStatusCode())
                {
                    string responseBody = await responseYa.Content.ReadAsStringAsync();
                    DataContractJsonSerializer j = new DataContractJsonSerializer(typeof(Root));
                    Root weather = j.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(responseBody))) as Root;                     
                    RequestRecord requestRecord = new RequestRecord();
                    requestRecord.DateTime = DateTime.Now;
                    weather.Fact.GetCyrillicProperties();
                    requestRecord.Body = JsonSerializer.Serialize(weather.Fact, jsonSerializerOptionsCyr); 
                    requestRecord.CityName = cityName;
                    var s1 = await _context.AddAsync(requestRecord);
                    var s2 = await _context.SaveChangesAsync();
                    return requestRecord.Body;
                }

            }
            catch (Exception exc)
            {
                try
                {
                    ErrorsLog errLogRecord = new ErrorsLog();
                    errLogRecord.DateTimeErr = DateTime.Now;
                    errLogRecord.ErrMessage = exc.Message;
                    var s1 = await _context.AddAsync(errLogRecord);
                    var s2 = await _context.SaveChangesAsync();
                }
                catch (Exception )
                {
                    return "Fail. Cannot Log errmessage to DB :"+exc.Message;
                }
                return "Fail";
            }        



        }
    }
}
