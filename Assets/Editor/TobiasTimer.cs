using UnityEngine;

public class TobiasTimer : MonoBehaviour
{
    [Header("Press F to print!")]
    [SerializeField] private string timer = "ZE TIME JA";
    public float scale = 1f;
    private float _seconds;
    private int _minutes;
    private int _hours;
    private int _days;

    public float Seconds
    {
        get { return _seconds; }
        set
        {
            _seconds = value; 
            while (_seconds >= 60)
            {
                _seconds -= 60;
                Minutes++;
            }
        }
    }
    public int Minutes
    {
        get { return _minutes; }
        set
        {
            _minutes = value;
            while (_minutes >= 60)
            {
                _minutes -= 60;
                Hours++;
            }
        }
    }
    public int Hours
    {
        get { return _hours; }
        set
        {
            _hours = value;
            while (_hours >= 24)
            {
                _hours -= 24;
                Days +=1;
            }
        }
    }
    public int Days
    {
        get { return _days; }
        set { _days = value; }
    }

    private void Update()
    {
        Seconds += Time.deltaTime * scale;
        if (Input.GetKeyDown(KeyCode.F))
        {
            timer = $"Day: {Days}, {Hours}:{Minutes}:{Mathf.Round(Seconds)}";
            Debug.Log(timer);
        }
    }

    // mitt alternative
    public static string GetTimerText(float time)
    {
        if (time < 0) return "0:00";

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        return $"{minutes} : {(seconds < 10 ? 0 : string.Empty)}{seconds}";
    }
}
