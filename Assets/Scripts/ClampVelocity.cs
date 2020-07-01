using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampVelocity : MonoBehaviour
{



    // This MonoBehaviour uses hard clamping to limit the velocity of a rigidbody.


    // The maximum allowed velocity. The velocity will be clamped to keep 
    // it from exceeding this value.
    float maxVelocity;
    FirstPersonAIO firstPerson;


// The cached rigidbody reference.
private  Rigidbody rb;
// A cached copy of the squared max velocity. Used in FixedUpdate.
private float sqrMaxVelocity = 2.5f;


// Awake is a built-in unity function that is called called only once during the lifetime of the script instance.
// It is called after all objects are initialized.
// For more info, see:
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        firstPerson = GetComponent<FirstPersonAIO>();
        SetMaxVelocity(maxVelocity);
    }


    // Sets the max velocity and calculates the squared max velocity for use in FixedUpdate.
    // Outside callers who wish to modify the max velocity should use this function. Otherwise,
    // the cached squared velocity will not be recalculated.
    void SetMaxVelocity(float maxVelocity)
    {
        this.maxVelocity = maxVelocity;
        sqrMaxVelocity = maxVelocity * maxVelocity;
    }


    // FixedUpdate is a built-in unity function that is called every fixed framerate frame.
    // We use FixedUpdate instead of Update here because the docs recommend doing so when
    // dealing with rigidbodies.
    // For more info, see:
    // http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.FixedUpdate.html 
    void FixedUpdate()
    {
        var v = rb.velocity;
        if (firstPerson.isCrouching)
        {

            SetMaxVelocity(1.5f);
        }
        else if (firstPerson.isSprinting)
        {
            SetMaxVelocity(4f);
        }
        else
        {
            SetMaxVelocity(2.5f);
        }
        // Clamp the velocity, if necessary
        // Use sqrMagnitude instead of magnitude for performance reasons.
        if (v.sqrMagnitude > sqrMaxVelocity)
        { // Equivalent to: rigidbody.velocity.magnitude > maxVelocity, but faster.
          // Vector3.normalized returns this vector with a magnitude 
          // of 1. This ensures that we're not messing with the 
          // direction of the vector, only its magnitude.
            rb.velocity = v.normalized * maxVelocity;
        }
    }
}
