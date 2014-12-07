using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {
	public int chanceOfEnchant = 70;
	public int chanceOfStat = 70;
	public float statMultiplier = 0.25f;

	private int itemLevel;
	private int damage;

	private bool enchanted;

	private int[] itemStats = new int[5];

	// Use this for initialization
	void Start () {
		int levelVar = GameManager.Instance.roomLevelVariation;
		itemLevel = Mathf.FloorToInt(GameManager.Instance.GetRoomLevel() + (Random.Range(0, levelVar) - (levelVar * 0.5f)));

		damage = itemLevel; //change this

		for (int i = 0; i < itemStats.Length; i++) 
		{
			if (Random.Range(0, 100) > chanceOfStat)
			{
				//make this able to be negative
				itemStats[i] = Mathf.FloorToInt(Random.Range(0, itemLevel) * statMultiplier);
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
