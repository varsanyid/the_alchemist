using UnityEngine;
using System.Collections;

public class FootstepController : MonoBehaviour {

    private CharacterController2D _controller;
    private AudioSource _runningClip;

    void Awake()
    {
        _controller = gameObject.transform.parent.GetComponent<CharacterController2D>();
        _runningClip = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(_controller.Velocity.x > 0 && !_runningClip.isPlaying)
        {
            _runningClip.Play();
        }
        else
        {
            _runningClip.Stop();
        }
    }
}
