using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {
	public int chanceOfEnchant = 70;
	public int chanceOfStat = 70;
	public float statMultiplier = 0.25f;
	public float DefenseToLevelMultiplyer = 0.9f;
	
	private int itemLevel;
	
	public bool enchanted = true;
	
	private int[] itemStats = new int[5];
	
	// Use this for initialization
	void Start () {
		int levelVar = RoomManager.Instance.roomLevelVariation;
		itemLevel = Mathf.FloorToInt(RoomManager.Instance.GetRoomLevel() + (Random.Range(0, levelVar) - (levelVar * 0.5f)));
		
		for (int i = 0; i < itemStats.Length; i++)
		{
			if (Random.Range(0, 100) > chanceOfStat)
			{
				itemStats[i] = Mathf.FloorToInt(Random.Range(0, itemLevel) * statMultiplier); //does not have negatives
			}
			else
			{
				itemStats[i] = 0;
			}
		}
	}
	
	public int[] getItemStats()
	{
		return itemStats;
	}
	
}