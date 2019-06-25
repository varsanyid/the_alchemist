using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Player : MonoBehaviour, IDamageable
{
    private bool _isFacingRight;
    private CharacterController2D _controller;
    private Animator _animator;
    private float _normalizedHorizontalSpeed;
    private Blink _blink;
    private bool _isDead;

    public PowerTypes ActivePower { get; set; }
    public float MaxSpeed = 10f;
    public float SpeedAccelerationOnGround = 10f;
    public float SpeedAccelerationInAir = 5f;


    public void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
        _isFacingRight = transform.localScale.x > 0;
        _blink = gameObject.GetComponent<Blink>();
        _isDead = false;
        //TODO:
        ActivePower = PowerTypes.BLINK;
    }

	public void Update()
	{
        if(GameManager.Instance.IsRunning)
        {
            HandleInput();
            float velocity = _controller.Velocity.x;
            float movementFactor =
                _controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
            _controller.SetHorizontalForce(Mathf.SmoothDamp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, ref velocity, movementFactor / 100 * Time.deltaTime));
            _animator.SetBool("IsGrounded", _controller.State.IsGrounded);
        }
	}

    private void HandleInput()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            _normalizedHorizontalSpeed = 1;
            _animator.SetFloat("Speed", _normalizedHorizontalSpeed);
            if(!_isFacingRight)
            {
                Flip();
            }
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            _normalizedHorizontalSpeed = -1;
            _animator.SetFloat("Speed", Mathf.Abs(_normalizedHorizontalSpeed));
            if(_isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            _normalizedHorizontalSpeed = 0;
            _animator.SetFloat("Speed", _normalizedHorizontalSpeed);
        }
        if(_controller.CanJump && Input.GetKeyDown(KeyCode.UpArrow))
        {
            _controller.Jump();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            UsePower(ActivePower);
        }
    }

    private void StopJumping()
    {
        _animator.SetBool("Jumping", false);
    }

    private void Flip()
    {
        transform.localScale 
            = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isFacingRight = transform.localScale.x > 0;
    }

    public void UsePower(PowerTypes power)
    {
        switch(power)
        {
            case PowerTypes.BLINK:
                _blink.Use();
                break;
            case PowerTypes.FIREBALL:
                break;
            case PowerTypes.SHIELD:
                break;
            default:
                throw new InvalidOperationException();
        }
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        if (!_isDead)
        {
            // _controller.AddForce(new Vector2(0, 15));
            // _controller.HandleCollisions = false;
            _animator.SetBool("IsDead", true);
            GameManager.Instance.IsRunning = false;
            GameManager.Instance.TimerBeforeDeath = Timer.Instance.GameTimer;
            _isDead = true;
            StartCoroutine(WaitAndReload(1));
        }
    }

    private IEnumerator WaitAndReload(int sec)
    {
        yield return new WaitForSeconds(sec);
        _animator.SetBool("IsDead", false);
        LevelManager.Instance.LoadGameAfterDeath();
        ResetStates();
    }

    private void ResetStates()
    {
        _isDead = false;
    }

    public void HidePlayer()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.enabled = false;
    }

    public void FinishRespawnAnimation()
    {
        _animator.SetBool("IsRespawning", false);
    }
}
