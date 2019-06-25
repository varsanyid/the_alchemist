using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Note : MonoBehaviour
{

    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidBody2D;
    private bool _isBeingRead;
    private SpriteRenderer _renderer;
    private Rect _window;

    public GUISkin GuiSkin;
    public float Width;
    public float Height;


    void OnGUI()
    {
        if (_isBeingRead)
        {
            GUI.skin = GuiSkin;
            _window = GUI.Window(0, _window, CollectNote, "");
        } 
    }

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.isTrigger = true;
        _window = new Rect((Screen.width - Width)/2, (Screen.height - Height)/2, Width, Height);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            _isBeingRead = true;
            GameManager.Instance.IsRunning = false;
            GameManager.Instance.CollectNote(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    void CollectNote(int id)
    {
        GUI.DrawTexture(new Rect(0, 0, Width, Height), _renderer.sprite.texture);
    }

    void Update()
    {
        if(_isBeingRead && Input.GetKeyDown(KeyCode.Escape))
        {
            _isBeingRead = false;
            GameManager.Instance.IsRunning = true;
            Destroy(gameObject);
        }
    }
}
