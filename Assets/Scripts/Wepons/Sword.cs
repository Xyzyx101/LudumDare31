using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {
	public int chanceOfEnchant = 70;
	public int chanceOfStat = 70;
	public float statMultiplier = 0.25f;
	public float DamageToLevelMultiplyer = 0.9f;

	private int itemLevel;
	private int damage;

	public bool enchanted;

	private int[] itemStats = new int[5];

	// Use this for initialization
	void Start () {
		int levelVar = RoomManager.Instance.roomLevelVariation;
		itemLevel = Mathf.FloorToInt(RoomManager.Instance.GetRoomLevel() + (Random.Range(0, levelVar) - (levelVar * 0.5f)));

		damage = Mathf.FloorToInt(itemLevel * DamageToLevelMultiplyer);

		for (int i = 0; i < itemStats.Length; i++)
		{
			if (Random.Range(0, 100) > chanceOfStat)
			{
				itemStats[i] = Mathf.FloorToInt(Random.Range(0, itemLevel) * statMultiplier - (itemLevel * statMultiplier));
			}
			else
			{
				itemStats[i] = 0;
			}
		}

		if (Random.Range (0, 100) > chanceOfEnchant)
		{
			enchanted = true;
		}
		else
		{
			enchanted = false;
		}
	}

	public int[] getItemStats()
	{
		return itemStats;
	}

	public int GetDamage()
	{
		return damage;
	}

}
