using UnityEngine;
using System.Collections;

public class FootStepParticleController : MonoBehaviour {

    private CharacterController2D _controller;
    private ParticleSystem _particle;
    private ParticleSystem.EmissionModule _emissionModule;
    private bool _isRunning;
    private bool _isGrounded;

	// Use this for initialization
	void Start () {
        _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        _particle = gameObject.GetComponent<ParticleSystem>();
        _emissionModule = _particle.emission;
    }
	
	// Update is called once per frame
	void Update () {
        _isGrounded = _controller.State.IsGrounded;
        _isRunning = _controller.Velocity.x != 0;
        if(_isGrounded && _isRunning)
        {
            _emissionModule.enabled = true;
        }
        else
        {
            _emissionModule.enabled = false;
        }
	}
}
