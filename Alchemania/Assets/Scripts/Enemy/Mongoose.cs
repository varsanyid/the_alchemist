using UnityEngine;
using System.Collections;

public class Mongoose : MonoBehaviour
{

    private Rigidbody2D _bulletRigidBody;
    private Player _player;
    private Animator _animator;
    private bool _isInRange;
    private bool _isShooting;
    private bool _isVisible;
    private float _shootingDistance;

    public GameObject Bullet;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _shootingDistance = 20f;
        _isInRange = false;
        _isVisible = false;
    }

    void Update()
    {
        if(_isVisible)
        {
            float distance = Vector2.Distance(_player.transform.position, gameObject.transform.position);
            if (distance <= _shootingDistance)
            {
                _isInRange = true;
            }
        }
        if (_isInRange && !_isShooting)
        {
            StartCoroutine(ShootPeriodically());
        }
    }



    public void OnBecameVisible()
    {
        _isVisible = true;
        _animator.SetBool("IsVisible", _isVisible);
    }

    public void OnBecameInvisible()
    {
        _isInRange = false;
        _isShooting = false;
        _isVisible = false;
        _animator.SetBool("IsVisible", _isVisible);
    }

    private IEnumerator ShootPeriodically()
    {
        _isShooting = true;
        while (_isShooting)
        {
            GameObject bullet = Instantiate(Bullet);
            bullet.transform.position = gameObject.transform.position;
            bullet.transform.rotation = gameObject.transform.rotation;
            _bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
            _bulletRigidBody.AddForce(new Vector2(-4, 0), ForceMode2D.Impulse);
            yield return new WaitForSeconds(2);
        }
    }

}
