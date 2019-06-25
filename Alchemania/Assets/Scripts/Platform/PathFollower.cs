using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PathFollower : MonoBehaviour
{
    public enum FollowType
    {
        TOWARDS, LERP, SMOOTHDAMP
    }

    public FollowType Type = FollowType.TOWARDS;
    public PathDefinition Definition;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f;

    private IEnumerator<Transform> _currentPoint;
    private Rigidbody2D _rigidBody;

    void Start()
    {
        if(Definition == null)
        {
            throw new System.InvalidOperationException("Definition can't be NULL");
        }
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _rigidBody.isKinematic = true;
        _currentPoint = Definition.PathIterator();
        _currentPoint.MoveNext();
       if(_currentPoint.MoveNext())
        {
            transform.position = _currentPoint.Current.position;
        } else
        {
            return;
        }
    }
    void Update()
    {
        if(_currentPoint == null || _currentPoint.Current == null)
        {
            return;
        }
        switch (Type)
        {
            case FollowType.TOWARDS:
                transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Speed * Time.deltaTime);
                break;
            case FollowType.LERP:
                transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Speed * Time.deltaTime);
                break;
            case FollowType.SMOOTHDAMP:
                Vector3 currentVelocity = _rigidBody.velocity;
                transform.position = Vector3.SmoothDamp(transform.position, _currentPoint.Current.position, ref currentVelocity, 10 * Speed * Time.deltaTime);
                break;
            default:
                throw new System.InvalidOperationException();
        }

        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if(distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            _currentPoint.MoveNext();
        }

    }
}
