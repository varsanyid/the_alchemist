using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour, IPower {

    public float DistanceToTeleport;
    public float BlinkRate;
    private float _canBlinkIn;

    private Vector3 _teleport;
    private Transform _transform;
    private bool _isFacingRight;

    void Awake()
    {
        _teleport = new Vector3(0, 0, 0);
        _isFacingRight = true;
    }
    void Update()
    {
        _canBlinkIn -= Time.deltaTime;
    }

    private void SetTeleportValues()
    {
        _transform = gameObject.transform;
        _isFacingRight = gameObject.transform.localScale.x > 0;
        _teleport.x = _isFacingRight ?_transform.position.x + DistanceToTeleport : _transform.position.x - DistanceToTeleport;
        _teleport.y = _transform.position.y;
        _teleport.z = 0f;
        gameObject.transform.position = _teleport;
    }

    public void Use()
    {
        if (CheckLayer() || !CanBlink())
            return;
        if(!CheckLayer() && CanBlink())
        {
            SetTeleportValues();
            _canBlinkIn = BlinkRate;
        }
    }

    private bool CheckLayer()
    {
        bool right = gameObject.transform.localScale.x > 0;
        Vector2 direction = right ? Vector2.right : -Vector2.right;
        RaycastHit2D raycastHit = Physics2D.Raycast(gameObject.transform.position, direction, DistanceToTeleport, 1 << LayerMask.NameToLayer("Floor"));
        return raycastHit;
    }

    private bool CanBlink()
    {
        return _canBlinkIn < 0;
    }
}
