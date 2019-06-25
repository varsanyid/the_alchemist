using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class KillerItem : MonoBehaviour
{

    private BoxCollider2D _collider;
    private Rigidbody2D _rigidBody;
    private Player _player;


    void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider.isTrigger = true;
        _rigidBody.isKinematic = true;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _player.TakeDamage(0, gameObject);
        }
    }
}
