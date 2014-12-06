using UnityEngine;
using System.Collections;

public class StateGameMenu : GameState 
{
	public StateGameMenu(GameManager manager):base(manager){ }
	
	public override void OnStateEntered()
	{
		Application.LoadLevel(0);
	}
	public override void OnStateExit(){}
	public override void StateUpdate() {}
	
	public override void StateGUI() 
	{
		GUILayout.Label("state: MENU");
		
		if (GUI.Button (new Rect (Screen.width * 0.1f, Screen.height * 0.4f, 100.0f, 50.0f), "Play Game"))
		{
			gameManager.NewGameState( gameManager.stateGamePlaying );
		}
		
		if (GUI.Button (new Rect (Screen.width * 0.1f, Screen.height * 0.6f, 100.0f, 50.0f), "Quit"))
		{
			Application.Quit();
		}
	}
}
