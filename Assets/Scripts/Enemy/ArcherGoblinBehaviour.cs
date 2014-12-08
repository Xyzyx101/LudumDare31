using UnityEngine;
using System.Collections;

public class ArcherGoblinBehaviour : SteeringBehaviour 
{

    public Sensor LeftSensor;
    public Sensor RightSensor;

    public Sensor AgroSensor;

    private bool agro = false;
    private bool ShouldWander = true;
    private GameObject target;

    public GameObject primaryWeapon;
    private float desiredAngle;
    bool primaryAttack;

    public float attackDelay = 1f;
    private float attackDelayTimer;
    private bool canAttack = true;

    void Awake()
    {

    }

    void Update()
    {
        Debug.Log(attackDelayTimer);
        attackDelayTimer -= Time.deltaTime;
        if (attackDelayTimer < 0)
        {
            canAttack = true;
        }
    }

    void FixedUpdate()
    {
        if (target != null)
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
            if (canAttack)
            {
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
            if (temp.tag == "Player")
            {
                Seek(temp.transform.position, 2.5f);
                target = temp;
            }
        }
    }

    private void UpdateDirection()
    {
        Vector3 targetDir = target.transform.position - transform.position;
        desiredAngle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
    }
}
