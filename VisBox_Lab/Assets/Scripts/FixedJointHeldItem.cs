using UnityEngine;
using System.Collections;

public class FixedJointHeldItem : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public FixedJoint _joint;
    public float _limitVelocity = 10f;

    private void Start()
    {
        if (!_rigidbody) _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.transform.SetParent(null);
    }
    private void FixedUpdate()
    {
        if(_rigidbody)
        {
            if(_rigidbody.velocity.magnitude > _limitVelocity)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.transform.position = _joint.anchor;
            }
        }
    }
}
