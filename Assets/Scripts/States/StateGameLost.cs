using UnityEngine;
using System.Collections;
public class StateGameLost : GameState 
{
	private float timer;
	
	public StateGameLost(GameManager manager):base(manager){ }
	
	public override void OnStateEntered()
	{
		timer = 3.0f;
	}
	public override void OnStateExit(){}
	public override void StateUpdate()
	{
		timer -= Time.deltaTime;
		if ( timer <= 0)
		{
			Application.LoadLevel("Game");
			gameManager.NewGameState( gameManager.stateGameMenu );	
		}
	}
	
	public override void StateGUI() 
	{
		GUILayout.Label("state: GAME LOST. \t timer: " + timer);

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), gameManager.gameoverIMG);
	}
}
