using UnityEngine;
using System.Collections;

public class HealthMeter : MonoBehaviour
{	
	public Texture2D image;
	private float hitPoints = 0;
	private float maxHitPoints = 0;
    private float normalisedHealth;

    private GUISkin Skin;
	
	private void OnGUI()
	{
        normalisedHealth = (float)hitPoints / maxHitPoints;

        GUI.depth = 1;

        GUI.color = Color.black;
        GUI.DrawTexture(new Rect(Screen.width * 0.01f - 2, (Screen.height * 0.13f), (Screen.width * 0.19f) + 4, (Screen.height * 0.05f) + 4), image);
        GUI.color = Color.white;

        GUI.color = Color.Lerp(Color.red, Color.green, normalisedHealth);
        GUI.DrawTexture(new Rect(Screen.width * 0.01f, (Screen.height * 0.13f) + 2, (Screen.width * 0.19f) * normalisedHealth, Screen.height * 0.05f), image);
        GUI.color = Color.white;

        GUI.skin = Skin;
        GUI.Label(new Rect(Screen.width * 0.01f - 2, (Screen.height * 0.13f), (Screen.width * 0.19f) + 4, (Screen.height * 0.05f) + 4), "Health");
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
	}
	
	//alter the current amount of health in the health bar.
	public void SetHealth(int amount)
	{
        hitPoints = amount;
	}

    public void SetMaxHealth(int amount)
    {
        maxHitPoints = amount;
    }

    void Start()
    {
        Skin = GuiManager.GetSkin();
    }
}