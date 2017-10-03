using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballPitcher : MonoBehaviour
{
    public Transform _pitchMuzzle;
    public GameObject _baseballPrefab;
    public float _pitchForce = 10f;

	void Start ()
    {
        if (!_pitchMuzzle) _pitchMuzzle = this.transform;
	}
	
	void Update ()
    {
		if(MiddleVRInput.GetButtonDown(0))
        {
            //FirePitch();
            ActionQueue.Enqueue(FirePitch);
        }
	}

    private Queue<Action> ActionQueue = new Queue<Action>();
    private void FixedUpdate()
    {
        foreach (var action in ActionQueue)
        {
            action();
        }
        ActionQueue.Clear();
    }

    private void FirePitch()
    {
        if (!_baseballPrefab) return;
        var go = GameObject.Instantiate(_baseballPrefab, _pitchMuzzle.position, _pitchMuzzle.rotation);
        var rb = go.GetComponent<Rigidbody>();
        if(rb)
        {
            Debug.Log("Pitch!");
            rb.WakeUp();
            rb.velocity = _pitchMuzzle.forward * _pitchForce;
            //rb.AddForce(_pitchMuzzle.forward * _pitchForce, ForceMode.VelocityChange);
        }
    }
}
