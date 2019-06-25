using UnityEngine;
using System.Collections;

public class DoorOpener : MonoBehaviour
{

    public GameObject Door;
    public float DoorSpeed;
    private BoxCollider2D _buttonCollider;
    private Transform _doorTransform;
    private Vector3 _doorPosition;
    private bool _isUsed;
    private Animator _anim;

    void Awake()
    {
        _buttonCollider = GetComponent<BoxCollider2D>();
        _doorTransform = Door.transform;
        _doorPosition = _doorTransform.position;
        _isUsed = false;
        _anim = GetComponent<Animator>();
        DoorSpeed = 2f;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !_isUsed)
        {
            Vector3 destination = new Vector3(_doorPosition.x, _doorPosition.y + 3, 0);
            _anim.SetBool("opening", true);
            StartCoroutine(OpenSingleDoor(destination));
            _isUsed = true;
        }
    }

    private IEnumerator OpenSingleDoor(Vector3 destination)
    {
        //Door.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        float diff = Mathf.Abs(Door.gameObject.transform.position.sqrMagnitude - destination.sqrMagnitude);
        while (diff > 10)
        {
            Door.gameObject.transform.position = Vector3.Lerp(_doorPosition, destination, DoorSpeed * Time.deltaTime);
            _doorPosition = Door.gameObject.transform.position;
            diff = Mathf.Abs(_doorPosition.sqrMagnitude - destination.sqrMagnitude);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
