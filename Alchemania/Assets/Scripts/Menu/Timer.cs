using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : Singleton<Timer>
{

    private Text _timerText;
    private TimerType _timer;

    public TimerType GameTimer { get { return _timer; } }

    public struct TimerType
    {
        private int _minute;
        private float _second;
        //private float _millis;

        public int Minute { get { return _minute; } set { _minute = value; } }
        public float Second { get { return _second; } set { _second = value; } }
        //public float MilliSecond { get { return _millis; } set { _millis = value; } }

        public override string ToString()
        {
            return Minute + "m " + Mathf.Round(Second) + "s ";
        }
    }

    public override void Awake()
    {
        base.Awake();
        _timerText = GetComponent<Text>();
        _timerText.color = Color.white;
        
    }

    void Start()
    {
        if (GameManager.Instance.TimerBeforeDeath.Minute == 0 && GameManager.Instance.TimerBeforeDeath.Second == 0)
        {
            _timer = new TimerType();
        }
        else
        {
            _timer = GameManager.Instance.TimerBeforeDeath;
        }
        
    }

    void OnGUI()
    {
        _timerText.text = _timer.ToString();
    }

    void Update()
    {
        if (GameManager.Instance.IsRunning)
        {
            if (_timer.Second == 60.0)
            {
                _timer.Minute++;
                _timer.Second = 0;
            }
            _timer.Second += Time.deltaTime;
        }
    }


}
