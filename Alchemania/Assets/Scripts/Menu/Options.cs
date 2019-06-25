using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

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
    }

    void OnMouseExit()
    {
        _renderer.sprite = Default;
    }
}
