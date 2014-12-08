using UnityEngine;
using System.Collections;

public class StateGUI : MonoBehaviour 
{
    public Texture2D image;

    public GUISkin Skin;

    private void OnGUI()
    {
        GUI.depth = 1;
        GUILayout.BeginArea(new Rect(Screen.width * 0.01f, Screen.height * 0.2f, Screen.width * 0.2f, Screen.height * 0.2f));
        

        GUI.color = Color.black;
        GUI.DrawTexture(new Rect(Screen.width * 0.01f - 2, (Screen.height * 0.13f), (Screen.width * 0.19f) + 4, (Screen.height * 0.05f) + 4), image);
        GUI.color = Color.white;

        GUI.skin = Skin;
        GUI.Label(new Rect(Screen.width * 0.01f - 2, (Screen.height * 0.13f), (Screen.width * 0.19f) + 4, (Screen.height * 0.05f) + 4), "Health");
        GUILayout.EndArea();
    }

    void Update()
    {

    }
}
