using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Text;

public class LevelManager : Singleton<LevelManager>
{
    private bool _isNewLevel;
    public bool IsNewLevel { get { return _isNewLevel; } set { _isNewLevel = value; } }
    private const string FILENAME = "save.alc";
    private GameState currentGameState;
    private Player _player;
   

    public override void Awake()
    {
        base.Awake();
        IsNewLevel = true;
    }

    public void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SaveGameState(Vector3 playerPosition)
    {
        if (currentGameState == null)
        {
            currentGameState = PersistentData.Instance.CurrentState;
        }
        currentGameState.PlayerPosition = new SerializableVector3(playerPosition);
        currentGameState.LevelToLoad = SceneManager.GetActiveScene().name;
        PersistentData.Instance.CurrentState = currentGameState;
    }

    public void WriteGameStateToFileSystem()
    {
        if (currentGameState != null)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "/" + FILENAME);
            binaryFormatter.Serialize(fs, currentGameState);
            fs.Close();
        }
    }

    public GameState LoadGameState()
    {
        int count = Directory.GetFiles(Application.persistentDataPath, "*", SearchOption.AllDirectories).Length;
        if (count != 0)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/" + FILENAME, FileMode.Open);
            return bf.Deserialize(fs) as GameState;
        }
        return null;
    }

    private string GetNextScene()
    {
        StringBuilder currentLevel = new StringBuilder(GameManager.Instance.LastPlayedLevel);
        int levelIndex = Int32.Parse(currentLevel[currentLevel.Length - 1].ToString());
        int nextLevelIndex = ++levelIndex;
        currentLevel[currentLevel.Length - 1] = Convert.ToChar(nextLevelIndex.ToString());
        return currentLevel.ToString();
    }

    public AsyncOperation LoadNextSceneAsync()
    {
        return SceneManager.LoadSceneAsync(GetNextScene());
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(GetNextScene());
    }

    public void SaveWhileLoadingNextScene()
    {
        WriteGameStateToFileSystem();
        LoadNextScene();
    }

    public void LoadGameAfterDeath()
    {
        GameState state = PersistentData.Instance.CurrentState;
        if(state != null)
        {
            if (state.LevelToLoad != null && state.LevelToLoad == SceneManager.GetActiveScene().name)
            {
                IsNewLevel = false;
                SceneManager.LoadScene(state.LevelToLoad);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        GameManager.Instance.IsRunning = true;
    }

    public void LoadSceneFromContinueMenu(GameState state)
    {
        if(state != null)
        {
            SceneManager.LoadScene(state.LevelToLoad);
        }
    }

}
