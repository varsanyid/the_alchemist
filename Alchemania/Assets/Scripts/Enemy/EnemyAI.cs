using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController2D), typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{
    public float Speed = 10f;

    private CharacterController2D _controller;
    private Vector2 _direction;
    private Player _player;

    void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1, 0);
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsRunning)
        {
            _controller.SetHorizontalForce(_direction.x * Speed);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player.TakeDamage(0, gameObject);
        }
        if(collision.gameObject.tag == "EnemyTurnTrigger")
        {
            _direction = -_direction;
        }
    }
}
