using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : Singleton<GameManager>
{

    private bool _isRunning;
    private Player _player;
    private Animator _anim;
    private string _previousLevel;
    private string _timeOfLastLevel;
    private Timer.TimerType _timeBeforeDeath;
    private Dictionary<string, int> _collectedNotesPerLevel;
    private List<string> _levels;
    private List<GameObject> _collectedNotes;


    public List<GameObject> AlreadyCollectedNotes { get { return _collectedNotes; } }
    public Timer.TimerType TimerBeforeDeath { get { return _timeBeforeDeath; } set { _timeBeforeDeath = value; } }
    public bool IsRunning { get { return _isRunning; } set { _isRunning = value; } }
    public string LastPlayedLevel { get { return _previousLevel; } set { _previousLevel = value; } }
    public string TimeOfLastLevel { get { return _timeOfLastLevel; } set { _timeOfLastLevel = value; } }
    public Dictionary<string, int> CollectedNotes
    {
        get { return _collectedNotesPerLevel; }
    }
    public override void Awake()
    {
        base.Awake();
        IsRunning = true;
        _levels = new List<string>(new string[] { "Level1", "Level2", "Level3", "Level4", "Level5", "Level6" });
        _collectedNotes = new List<GameObject>();
        _collectedNotesPerLevel = new Dictionary<string, int>(6);
        InitNotesDictionary();
    }

    private void InitNotesDictionary()
    {
        for(int i = 0; i < _levels.Count; i++)
        {
            _collectedNotesPerLevel.Add(_levels[i], 0);
        }
    }

    public void CollectNote(string level)
    {
        int numberOfNotesCollected;
        try
        {
            if (!_collectedNotesPerLevel.ContainsKey(level) || 
                !_collectedNotesPerLevel.TryGetValue(level, out numberOfNotesCollected))
            {
                return;
            }
            if(numberOfNotesCollected != 2)
            {
                _collectedNotesPerLevel[level] = ++_collectedNotesPerLevel[level];
            }
        }
        catch (System.ArgumentNullException ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        if (PersistentData.Instance.CurrentState.PlayerPosition.Vector != null && !LevelManager.Instance.IsNewLevel)
        {
            _player.transform.position = PersistentData.Instance.CurrentState.PlayerPosition.Vector;
        }
       if(!LevelManager.Instance.IsNewLevel)
        {
            _anim.SetBool("IsRespawning", true);
        }
       if(level != 8)
        {
            LastPlayedLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
    }
}
