using UnityEngine;
using System.Collections;

public class NextLevelLoader : MonoBehaviour
{

    private LevelManager _levelManager;
    private LevelSummary _summary;

    void Start()
    {
     
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.TimeOfLastLevel = Timer.Instance.GameTimer.ToString();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Summary");   
        }
    }

}
