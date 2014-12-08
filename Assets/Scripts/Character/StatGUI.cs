using UnityEngine;
using System.Collections;

public class StatGUI : MonoBehaviour 
{
    public Texture2D image;
    private GUISkin Skin;
    public Player playerScript;

    void Awake()
    {
        Skin = GuiManager.GetSkin();
    }

    private void OnGUI()
    {
        GUI.depth = 1;
        Skin = GuiManager.GetSkin();

        GUILayout.BeginArea(new Rect(Screen.width * 0.02f, Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * 0.2f));

        GUI.color = Color.black;
        GUILayout.Label("Vitality" + playerScript.GetStats(stats.Vitality));
        GUILayout.Label("Strength" + playerScript.GetStats(stats.Strength));
        GUILayout.Label("Defense" + playerScript.GetStats(stats.Defense));
        GUILayout.Label("Dexterity" + playerScript.GetStats(stats.Dextarity));
        GUILayout.Label("Speed" + playerScript.GetStats(stats.Speed));

        GUILayout.EndArea();
    }

    void Update()
    {

    }
}
