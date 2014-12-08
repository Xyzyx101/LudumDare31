using UnityEngine;
using System.Collections;

public class StatGUI : MonoBehaviour 
{
    public Texture2D image;
    private GUISkin Skin;
    public Player playerScript;
    private bool hasPrime;
    private WeaponItem primeWep;
    private bool hasSec;
    private WeaponItem secWep;

    void Start()
    {
        Skin = GuiManager.GetSkin();
    }

    private void OnGUI()
    {
        GUI.depth = 1;
        Skin = GuiManager.GetSkin();

        GUILayout.BeginArea(new Rect(Screen.width * 0.02f, Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * 0.2f));

        GUI.color = Color.black;
        GUILayout.Label("Vitality:      " + playerScript.GetStats(stats.Vitality));
        GUILayout.Label("Strength:      " + playerScript.GetStats(stats.Strength));
        GUILayout.Label("Defense:       " + playerScript.GetStats(stats.Defense));
        GUILayout.Label("Dexterity:     " + playerScript.GetStats(stats.Dextarity));
        GUILayout.Label("Speed:         " + playerScript.GetStats(stats.Speed));

        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width * 0.02f, Screen.height * 0.3f, Screen.width * 0.2f, Screen.height * 0.1f));

        GUI.color = Color.black;
        if (hasPrime)
        {
            GUILayout.Label("Primary DMG:   " + primeWep.GetDamage());
        }
        if (hasSec)
        {
            GUILayout.Label("Secondary DMG: " + secWep.GetDamage());
        }

        GUILayout.EndArea();
    }

    void Update()
    {
        if(playerScript.inventory.primaryWeapon != null && !hasPrime)
        {
            hasPrime = true;
            primeWep = playerScript.inventory.primaryWeapon.GetComponent<WeaponItem>();
        }

        if(playerScript.inventory.secondaryWeapon != null && !hasSec)
        {
            hasSec = true;
            secWep = playerScript.inventory.secondaryWeapon.GetComponent<WeaponItem>();
        }
    }
    
    public void UpdateWeapons()
    {
        if (playerScript.inventory.primaryWeapon != null)
        {
            primeWep = playerScript.inventory.primaryWeapon.GetComponent<WeaponItem>();
        }
        if (playerScript.inventory.secondaryWeapon != null)
        {
            secWep = playerScript.inventory.secondaryWeapon.GetComponent<WeaponItem>();
        }
    }
}
