using UnityEngine;
using System.Collections;

public class GoblinBehaviour : SteeringBehaviour
{
    public Sensor LeftSensor;
    public Sensor RightSensor;

    public Sensor AgroSensor;

    public float wanderTime = 0.3f;
    private float wanderTimeActual;

    public float seperation = 1;
    private bool isAlone = true;
    private bool ShouldWander = true;
    private bool Agro = false;
    private GameObject partner;

    void Awake()
    {
        wanderTimeActual = wanderTime;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        WhatDo();

        AdvancedCollisions(LeftSensor, RightSensor);

        ApplySteering();
        Reset();
    }

    void WhatDo()
    {
        if (AgroSensor.isColliding)
        {
            GameObject temp = AgroSensor.GiveTarget();
            if (temp.tag == "Player" && isAlone)
            {
                Flee(temp.transform.position);
                ShouldWander = false;
            }
            else if (temp.tag == "Player" && !isAlone)
            {
                Seek(temp.transform.position, 1.5f);
            }
            else if (temp.tag == "Goblin" && partner == null)
            {
                isAlone = false;
                partner = temp;
            }
        }

        if(partner != null && Vector3.Distance(transform.position, partner.transform.position) > 5)
        {
            ShouldWander = false;
            Seek(partner.transform.position, seperation);
        }

        if (ShouldWander) // wanter when not turning
        {
            Wander();
            ShouldWander = false;
        }
        else
        {
            wanderTimeActual -= Time.deltaTime;
            if (wanderTimeActual <= 0.0f) // resets time of next wander
            {
                wanderTimeActual = wanderTime;
                ShouldWander = true;
            }
        }


    }

    void OnTriggerEnter(Collider other)
    {

    }
}
