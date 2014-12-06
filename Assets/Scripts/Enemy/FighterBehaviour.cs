using UnityEngine;
using System.Collections;

public class FighterBehaviour : SteeringBehaviour 
{
    public Sensor RightSensor;
    public Sensor LeftSensor;

    public float sensorRange = 3;

    public float wanderTime = 0.3f;
    float wanderTimeActual;
    bool ShouldWander = true;

    void Awake()
    {
        wanderTimeActual = wanderTime;
    }

	void FixedUpdate () 
    {
        ShouldIWander();

        AdvancedCollisions(LeftSensor ,RightSensor);
        
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
}
