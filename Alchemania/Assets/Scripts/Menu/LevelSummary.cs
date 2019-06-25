using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSummary : MonoBehaviour {

    private Text[] _texts;
    private bool _updated;

    void Start()
    {
        _texts = GetComponentsInChildren<Text>();
        _updated = false;
    }

    void OnGUI()
    {
        if (!_updated)
        {
            UpdateText(ref _texts);           
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            LevelManager.Instance.IsNewLevel = true;
            LevelManager.Instance.SaveWhileLoadingNextScene();
        }
    }

    private void UpdateText(ref Text[] _texts)
    {
        try
        {
            _updated = true;
            _texts[0].text += GameManager.Instance.TimeOfLastLevel;
            int number = GameManager.Instance.CollectedNotes[GameManager.Instance.LastPlayedLevel];
            _texts[1].text += number.ToString();
        }
        catch (System.ArgumentNullException ex)
        {
            Debug.LogError(ex.Message);
        }

    }
    /*
    public GUISkin GuiSkin;
    public float Width;
    public float Height;
    public bool IsBeingShown { get { return _isBeingShown; } set { _isBeingShown = value; } }

    private bool _isBeingShown;
    private Rect _summaryWindow;
    private LevelManager _levelManager;

    void Awake()
    { 
        _isBeingShown = false;
        _summaryWindow = new Rect((Screen.width - Width) / 2, (Screen.height - Height) / 3, Width, Height);
    }

    void Start()
    {
        _levelManager = LevelManager.Instance;
    }

    void OnGUI()
    {
        if(_isBeingShown)
        {
            GUI.skin = GuiSkin;
            GUI.BeginGroup(new Rect((Screen.width - Width) / 3, (Screen.height - Height) / 2, Width/2, Height/2));
            _summaryWindow = GUI.Window(0, _summaryWindow, Summary, "first");
            GUI.Box(_summaryWindow, new GUIContent("BOX"))
            GUI.EndGroup();
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                _isBeingShown = false;
                _levelManager.IsNewLevel = true;
                _levelManager.SaveWhileLoadingNextScene();
            }
        }
    }

    void Summary(int id)
    {

    }
    */
}
