using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour 
{
    public float baseHP = 100;
    public float hp { set; get; }
    public float strength = 1;
    public float damage { set; get; }
    private int gameDiff;

    void Awake()
    {
        gameDiff = GameManager.Instance.GetRoomLevel();
        hp = baseHP * gameDiff;
        damage = strength * gameDiff;
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
