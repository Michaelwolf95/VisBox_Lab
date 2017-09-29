using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCompensator : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public Transform[] castPoints;
    public Vector3[] _lastPointPositions;
    public List<Collider> collidersThisFrame = new List<Collider>();
    public List<Collider> collidersLastFrame = new List<Collider>();

    private Vector3 _lastRigidbodyVelocity;
    private Vector3[] _pointVelocities;
    private Vector3[] _lastPointVelocities;

    private Vector3 _positionLastFrame;
    private Quaternion _rotationLastFrame;

    #region Monobehaviour Callbacks
    private void Awake()
    {
        _pointVelocities = new Vector3[castPoints.Length];
        _lastPointPositions = new Vector3[castPoints.Length];
        UpdateLastPointPositions();
        UpdatePointVelocities();
        _lastPointVelocities = _pointVelocities;
    }
    private void Start()
    {
        if(!_rigidbody) _rigidbody = this.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        UpdateLastPointPositions();
        UpdatePointVelocities();
        StartCoroutine(CoLateFixedUpdate());
    }

    private void FixedUpdate()
    {
        UpdatePointVelocities();
        PerformSphereCastsFromPoints();
    }

    private void LateFixedUpdate()
    {
        _lastPointVelocities = _pointVelocities;
        UpdateLastPointPositions();
        collidersLastFrame = collidersThisFrame;
        collidersThisFrame.Clear();
        _lastRigidbodyVelocity = _rigidbody.velocity;
        _positionLastFrame = this.transform.position;
        _rotationLastFrame = this.transform.rotation;
    }
  
    private void OnCollisionEnter(Collision collision)
    {
        TryAddCollider(collision.collider);
    }
    private void OnCollisionStay(Collision collision)
    {
        TryAddCollider(collision.collider);
    }
    private void OnCollisionExit(Collision collision)
    {
        TryRemoveCollider(collision.collider);
    }
    #endregion

    #region Cast Points

    private void UpdateLastPointPositions()
    {
        for (int i = 0; i < castPoints.Length; i++)
        {
            _lastPointPositions[i] = castPoints[i].position;
        }
    }
    private void UpdatePointVelocities()
    {
        for (int i = 0; i < castPoints.Length; i++)
        {
            _pointVelocities[i] = (castPoints[i].position - _lastPointPositions[i]);
        }
    }

    private void PerformSphereCastsFromPoints()
    {
        for (int i = 0; i < castPoints.Length; i++)
        {
            Debug.DrawLine(_lastPointPositions[i], castPoints[i].position, Color.blue, 0.2f);
            CompensatePhysicsSpherecast(_lastPointPositions[i], castPoints[i].position, _pointVelocities[i], _lastPointVelocities[i]);
        }
    }

    private void CompensatePhysicsSpherecast(Vector3 startPos, Vector3 lastPos, Vector3 pointVelocity, Vector3 lastPointVelocity, float radius = 0.2f)
    {
        var derivedForceVector = ((pointVelocity - lastPointVelocity) / Time.fixedDeltaTime) * _rigidbody.mass;
        var deltaPos = lastPos - startPos;
        var dist = deltaPos.magnitude;
        var dir = deltaPos.normalized;
        RaycastHit[] hits = Physics.SphereCastAll(startPos, radius, dir, dist);
        foreach(var hit in hits)
        {
            if (hit.point == Vector3.zero) continue;
            if(hit.collider != null)
            {
                if(hit.collider.attachedRigidbody != null)
                {
                    if(!hit.collider.attachedRigidbody.isKinematic && hit.collider.attachedRigidbody != this._rigidbody)
                    {
                        if(!collidersThisFrame.Contains(hit.collider))
                        {
                            Debug.Log("<color=#ff0000> [CompensatePhysics]: Missed Collider! "+ hit.collider +" </color>");
                            Debug.DrawLine(startPos, hit.point, Color.red, 10f);
                            Debug.Log(hit.point);
                            //// Move to appropriate point relative to object. "Stuck to Bat".
                            //var relativePosition = hit.point - _positionLastFrame;
                            //var localPos = Quaternion.Inverse(_rotationLastFrame) * relativePosition;
                            //var newPos = this.transform.rotation * localPos;
                            //hit.collider.attachedRigidbody.transform.position = newPos;
                            
                            hit.collider.attachedRigidbody.AddForceAtPosition(derivedForceVector, hit.point, ForceMode.Impulse);
                            TryAddCollider(hit.collider);
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region Collider Tracking

    private void TryRemoveCollider(Collider col)
    {
        if (collidersThisFrame.Contains(col))
        {
            collidersThisFrame.Remove(col);
        }
    }
    private void TryAddCollider(Collider col)
    {
        if (!collidersThisFrame.Contains(col))
        {
            collidersThisFrame.Add(col);
        }
    }
    #endregion

    private IEnumerator CoLateFixedUpdate()
    {
        while (this.enabled)
        {
            yield return new WaitForFixedUpdate();
            LateFixedUpdate();
        }
    }
}
