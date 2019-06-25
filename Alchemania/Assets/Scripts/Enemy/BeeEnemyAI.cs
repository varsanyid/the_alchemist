using UnityEngine;
using System.Collections;

public class BeeEnemyAI : MonoBehaviour
{

    private CharacterController2D _playerController;
    private GameManager _gameManager;
    private Vector3 _initialPosition;
    private bool _hasBeenSeen;
    private bool _followed;
    private bool _facingRight;
    private float _range = 8f;

    public float MoveSpeed = 0.5f;

    void Awake()
    {
        _hasBeenSeen = false;
    }

    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        _initialPosition = transform.position;
        _gameManager = GameManager.Instance;
    }

    void Update()
    {
        if(_hasBeenSeen && _gameManager.IsRunning)
        {
            float distance = Vector3.Distance(transform.position, _playerController.transform.position);
            if(distance < _range)
            {
                _followed = true;
                transform.position = 
                    Vector3.Lerp(transform.position, _playerController.transform.position, MoveSpeed * Time.deltaTime);
                if(transform.position.x < _playerController.transform.position.x && !_facingRight)
                {
                    _facingRight = true;
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                }
                if(transform.position.x > _playerController.transform.position.x && _facingRight)
                {
                    _facingRight = false;
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                }
            }
            if (_followed && distance > _range)
            {
                transform.position 
                    = Vector3.Lerp(transform.position, _initialPosition, MoveSpeed * Time.deltaTime);
            }
        }
    }


    public void OnBecameVisible()
    {
        _hasBeenSeen = true;
    }


}
