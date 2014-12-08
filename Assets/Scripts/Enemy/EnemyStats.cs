using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour 
{
    public float baseHP = 100;
    public float hp { set; get; }
    public float strength = 1;
    public float damage { set; get; }
    private int gameDiff;
    public GameObject[] Spawnable;
    public int chanceToSpawn = 25;
    private bool dropCheck = true;

    void Awake()
    {

    }

    void Start()
    {
        gameDiff = RoomManager.Instance.GetRoomLevel();
        hp = baseHP * gameDiff;
        damage = strength * gameDiff;
    }

    void Update()
    {
        if (hp <= 0)
        {
            if (Random.Range(0, 100) < chanceToSpawn && dropCheck)
            {
                dropCheck = false;
                int objectToSpawn = Random.Range(0, Spawnable.Length);
                GameObject spawnedObject = (GameObject)Instantiate(Spawnable[objectToSpawn], this.transform.position, Spawnable[objectToSpawn].transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }

    public void DoDamage(float damage)
    {
        hp -= damage;
    }
}
