using UnityEngine;
using System.Collections;

public class HealthMeter : MonoBehaviour
{	
	public Texture2D image;
	private float hitPoints = 0;
	private float maxHitPoints = 0;
    float normalisedHealth;
	
	private void OnGUI()
	{
		normalisedHealth = (float)hitPoints / maxHitPoints;

        GUI.depth = 1;

        GUI.color = Color.black;
        GUI.DrawTexture(new Rect(Screen.width * 0.2f - 2, (Screen.height * 0.125f), (Screen.width * 0.2f) + 4, (Screen.height * 0.05f) + 4), image);
        GUI.color = Color.white;

        whatColor();
        GUI.DrawTexture(new Rect(Screen.width * 0.2f, (Screen.height * 0.125f) + 2, (Screen.width * 0.2f) * normalisedHealth, Screen.height * 0.05f), image);
        GUI.color = Color.white;
	}
	
    void Update()
    {
        if(hitPoints <= 0)
        {
            hitPoints = 0;
        }
    }
				
	//set the range covered by the health bar to a specific value.
	public void SetMaxHitPoints(int newValue)
	{
		maxHitPoints = newValue;
		hitPoints = maxHitPoints;
	}
	
	//alter the current amount of health in the health bar.
	public void AlterHealth(int amount)
	{
		hitPoints += amount;
        if(hitPoints + amount > maxHitPoints)
        {
            hitPoints = maxHitPoints;
        }
	}

    void whatColor()
    {
        if (normalisedHealth <= 0.25f)
        {
            GUI.color = Color.red;
        }
        else if (normalisedHealth <= 0.75f)
        {
            GUI.color = Color.yellow;
        }
        else
        {
            GUI.color = Color.green;
        }
    }
}