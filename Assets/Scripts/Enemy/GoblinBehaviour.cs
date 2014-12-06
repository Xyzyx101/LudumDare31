using UnityEngine;
using System.Collections;

public class GoblinBehaviour : SteeringBehaviour
{
    public float wanderTime = 0.3f;
    float wanderTimeActual;
    bool ShouldWander = true;
    bool isHolding = false;
    GameObject held;

    void Awake()
    {
        wanderTimeActual = wanderTime;
    }

    void FixedUpdate()
    {
        ShouldIWander();

        ApplySteering();
        Reset();
    }

    void ShouldIWander()
    {
        if (ShouldWander) // wanter when not turning
        {
            Wander();
            ShouldWander = false;
        }
        else
        {
            wanderTimeActual -= Time.deltaTime;
        }

        if (wanderTimeActual <= 0.0f) // resets time of next wander
        {
            wanderTimeActual = wanderTime;
            ShouldWander = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Box" && isHolding == false)
        {
            other.transform.parent = this.transform;
            held = other.gameObject;
            held.name = "BoxHeld";
            isHolding = true;
        }
        else if (other.gameObject.name == "Box" && isHolding == true && held != other.gameObject)
        {
            //held.name = "Box";
            held.transform.parent = null;
            //other.transform.parent = this.transform;
            //other.name = "BoxHeld";
            //held = other.gameObject;
            isHolding = false;
        }
    }
}
