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
        if(maxHitPoints == 0)
        {
            maxHitPoints = entity.GetComponent<EnemyStats>().hp;
        }
        hitPoints = entity.GetComponent<EnemyStats>().hp;

        if (hitPoints < 0)
        {
            hitPoints = 0;
        }

        normalisedHealth = (float)hitPoints / maxHitPoints;

        hpQuad.transform.localScale = new Vector3(quadMaxLength * normalisedHealth, hpQuad.transform.localScale.y, hpQuad.transform.localScale.z);

        hpQuad.renderer.material.color = Color.Lerp(Color.red, Color.green, normalisedHealth); ;

	}
}
