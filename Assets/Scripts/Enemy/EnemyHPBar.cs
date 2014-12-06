using UnityEngine;
using System.Collections;

public class EnemyHPBar : MonoBehaviour 
{
    public GameObject entity;
    public GameObject hpQuad;
    private float quadMaxLength;
    private float hitPoints = 0;
    private float maxHitPoints = 0;
    float normalisedHealth;


    void Awake()
    {
       
    }

	void Start () 
    {
        maxHitPoints = entity.GetComponent<EnemyStats>().hp;
        quadMaxLength = hpQuad.transform.localScale.x;
        hitPoints = entity.GetComponent<EnemyStats>().hp;
	}
	
	void Update () 
    {
        hitPoints = entity.GetComponent<EnemyStats>().hp;

        if (hitPoints < 0)
        {
            hitPoints = 0;
        }

        normalisedHealth = (float)hitPoints / maxHitPoints;

        hpQuad.transform.localScale = new Vector3(quadMaxLength * normalisedHealth, hpQuad.transform.localScale.y, hpQuad.transform.localScale.z);

        if (normalisedHealth <= 0.25f)
        {
            hpQuad.renderer.material.color = Color.red;
        }
        else if (normalisedHealth <= 0.75f)
        {
            hpQuad.renderer.material.color = Color.yellow;
        }
        else
        {
            hpQuad.renderer.material.color = Color.green;
        }
	}
}
