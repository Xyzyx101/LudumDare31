using UnityEngine;
using System.Collections;

public class Armour : MonoBehaviour {
	public int chanceOfEnchant = 70;
	public int chanceOfStat = 70;
	public float statMultiplier = 0.25f;
	public float DefenseToLevelMultiplyer = 0.3f;
	
	private int itemLevel;
	
	public bool enchanted;
	
	private int[] itemStats = new int[]{0,0,0,0,0};
	
	// Use this for initialization
	void Start () {
		int levelVar = RoomManager.Instance.roomLevelVariation;
		itemLevel = Mathf.FloorToInt(RoomManager.Instance.GetRoomLevel() + (Random.Range(0, levelVar) - (levelVar * 0.5f)));
		
		itemStats[(int)stats.Defense] += Mathf.FloorToInt(itemLevel * DefenseToLevelMultiplyer);
		
		for (int i = 0; i < itemStats.Length; i++)
		{
			if (Random.Range(0, 100) > chanceOfStat)
			{
				itemStats[i] += Mathf.FloorToInt(Random.Range(0, itemLevel) * statMultiplier - (itemLevel * statMultiplier * 0.5f));
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
	
}
