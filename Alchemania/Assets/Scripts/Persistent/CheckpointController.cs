using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class CheckpointController : MonoBehaviour
{
    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private Renderer _darkness;

    public bool IsUsed { get; private set; }

    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider.isTrigger = true;
        _rigidbody.isKinematic = true;
        foreach(var renderer in GetComponentsInChildren<Renderer>())
        {
            _darkness = renderer;
        }
        _darkness.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !IsUsed)
        {
            LevelManager.Instance.SaveGameState(gameObject.transform.position);
            IsUsed = true;
            _darkness.enabled = true;
        }
    }
}
