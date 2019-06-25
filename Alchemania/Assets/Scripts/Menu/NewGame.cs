using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGame : MonoBehaviour {

    private SpriteRenderer _renderer;

    public Sprite Default;
    public Sprite Hover;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseOver()
    {
        _renderer.sprite = Hover;
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadSceneAsync("Level1");
        }
    }

    public void OnMouseExit()
    {
        _renderer.sprite = Default;
    }
}
