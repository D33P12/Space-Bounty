using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(1000f, 10000f)] //for scrollbar
    float _thrustForce = 7500,
          _pitchForce = 6000,
          _rollForce = 1000
         ,_YawForce = 2000;
    private Rigidbody _rigidbody;
    [SerializeField][Range(-1f,1f)]
    private float _thrustAmount
                 ,_pitchAmount
                 ,_rollAmount
                 ,_yawAmount;

    private void Awake()
    {
      _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!Mathf.Approximately(0f, _pitchAmount))
        {
           _rigidbody.AddTorque(transform.right *(_pitchForce * _pitchAmount * Time.fixedDeltaTime)); //torque in horizontal axis 
        }
    }
}   
