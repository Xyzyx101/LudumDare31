using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour 
{
    public float maxHP = 100;
    public float hp { set; get; }

    void Awake()
    {
        hp = maxHP;
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void DoDamage(float damage)
    {
        hp -= damage;
    }
}
