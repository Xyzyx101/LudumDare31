﻿using UnityEngine;
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
    private bool agro = false;
    private bool ShouldWander = true;
    private GameObject partner;
    private GameObject target;

    public GameObject primaryWeapon;
    private float desiredAngle;
    bool primaryAttack;

	public float attackDelay = 1f;
	private float attackDelayTimer;
	private bool canAttack = true;

    void Awake()
    {
        wanderTimeActual = wanderTime;
    }

    void Update()
    {
		attackDelayTimer -= Time.deltaTime;
		if (attackDelayTimer < 0) {
			canAttack = true;
		}
    }

    void FixedUpdate()
    {
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= 2.5)
        {
            primaryAttack = true;
            transform.LookAt(target.transform.position, Vector3.up);
        }
        else
        {
            primaryAttack = false;
        }

        if (primaryWeapon != null && primaryAttack)
        {
            if(canAttack) {
				primaryWeapon.SetActive(true);
				canAttack = false;
				attackDelayTimer = attackDelay * Random.value + 0.2f;
			}
        }

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
            /*
            if (temp.tag == "Player" && isAlone)
            {
                
                Flee(temp.transform.position);
                ShouldWander = false;
                
            }
            */
            if (temp.tag == "Player" /*&& !isAlone*/) // Originally a else if
            {
                Seek(temp.transform.position, 2.5f);
                target = temp;
            }
            /*
            else if (temp.tag == "Goblin" && partner == null)
            {
                isAlone = false;
                partner = temp;
            }
            */
        }

        /*
        if(partner != null && Vector3.Distance(transform.position, partner.transform.position) > 5)
        {
            ShouldWander = false;
            Seek(partner.transform.position, seperation);
        }
        */

        if (ShouldWander) // wander when not turning
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

    private void UpdateDirection()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        desiredAngle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
    }
}
