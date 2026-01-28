using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WeatherManager : Minigame
{
    [Header("Weather Manager Attributes")]
    public int fakeTemperatureRange = 20;
    [SerializeField] private string[] locations;
    private PopText questionDisplay;
    private float temperature;
    private float fakeTemperature;
    private string query = "how is the weather up there?";

    protected override void OnStart()
    {
        questionDisplay = GameController.canvas.InstantiatePopText();
        questionDisplay.SetValues("loading...");

        int location = Random.Range(0, locations.Length);
        StartCoroutine(GetWeather(locations[location]));
    }

    public override void GameStart()
    {
        base.GameStart();
        questionDisplay.SetValues(query, 0.1f);

        GameFinish(temperature < fakeTemperature ? 1 : 2);
    }

    /// <param name="winner">1 means lower, 2 means higher</param>
    public override void GameFinish(int winner)
    {
        GameController.canvas.InstantiateWinnerText().SetValues($"It was {(winner == 1 ? "Lower" : "Higher")}!", 3f);
        base.GameFinish(winner);
    }

    private const string key = "1c581fce429c45a59a9121614250109"; // stolen from Charlie

    [System.Serializable]
    public class Condition
    {
        public string text;
        public string icon;
        public int code;
    }

    [System.Serializable]
    public class Current
    {
        public int last_updated_epoch;
        public string last_updated;
        public double temp_c;
        public double temp_f;
        public int is_day;
        public Condition condition;
        public double wind_mph;
        public double wind_kph;
        public int wind_degree;
        public string wind_dir;
        public double pressure_mb;
        public double pressure_in;
        public double precip_mm;
        public double precip_in;
        public int humidity;
        public int cloud;
        public double feelslike_c;
        public double feelslike_f;
        public double windchill_c;
        public double windchill_f;
        public double heatindex_c;
        public double heatindex_f;
        public double dewpoint_c;
        public double dewpoint_f;
        public double vis_km;
        public double vis_miles;
        public double uv;
        public double gust_mph;
        public double gust_kph;
        public double short_rad;
        public double diff_rad;
        public double dni;
        public double gti;
    }

    [System.Serializable]
    public class Location
    {
        public string name;
        public string region;
        public string country;
        public double lat;
        public double lon;
        public string tz_id;
        public int localtime_epoch;
        public string localtime;
    }

    [System.Serializable]
    public class Weather
    {
        public Location location;
        public Current current;
    }

    private IEnumerator GetWeather(string location)
    {
        string address = $"https://api.weatherapi.com/v1/current.json?key={key}&q={location}";

        using (UnityWebRequest api = UnityWebRequest.Get(address))
        {
            yield return api.SendWebRequest();

            if (api.result == UnityWebRequest.Result.Success)
            {
                Weather weather = JsonUtility.FromJson<Weather>(api.downloadHandler.text);

                temperature = Mathf.RoundToInt((float)weather.current.temp_c * 100) / 100;
                Debug.Log($"{weather.location.name} is {temperature}°C"); // FOR DEBUGGING

                // Get the fake temperature
                int difference = 0;
                while (difference == 0) difference = Random.Range(-fakeTemperatureRange, fakeTemperatureRange);
                fakeTemperature = temperature + difference;

                // Create Question and Buttons
                query = $"{fakeTemperature}°C in {location}";
                questionDisplay.SetValues(query);
                GameController.canvas.InstantiateButtons(choices, new string[] { "Lower", "Higher" }, false);
            }
            else GameFail();            
        }

        yield break;
    }
}
