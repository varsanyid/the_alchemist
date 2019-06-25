using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

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
            Application.Quit();
        }
    }

    public void OnMouseExit()
    {
        _renderer.sprite = Default;
    }
}
