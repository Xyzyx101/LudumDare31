using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance { get { return instance; } }
	private static GameManager instance = null;
	
	private GameState currentState;
	public StateGamePlaying stateGamePlaying{get;set;}
	public StateGameLost stateGameLost{get;set;}
	public StateGameIntro stateGameIntro{get;set;}
	public StateGameMenu stateGameMenu{get;set;}

	public int roomLevelIncrement = 5;
	public int roomLevelStart = 10;
	public int roomLevelVariation = 8;
	private int roomLevel;

	
	private void Awake () 
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;        
		} 
		else 
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		
		stateGamePlaying = new StateGamePlaying(this);
		stateGameLost = new StateGameLost(this);
		stateGameIntro = new StateGameIntro(this);
		stateGameMenu = new StateGameMenu(this);

		//stuff for game(shoudl this be in game playing?
		roomLevel = roomLevelStart;
	}	
	
	private void Start () 
	{
		NewGameState( stateGameIntro );
	}
	
	private void Update () 
	{
		if (currentState != null)
		{
			currentState.StateUpdate();
		}
	}
	
	private void OnGUI () 
	{
		if (currentState != null)
		{
			currentState.StateGUI();
		}
	}
	
	public void NewGameState(GameState newState)
	{
		if( null != currentState)
		{
			currentState.OnStateExit();
		}
		currentState = newState;
		currentState.OnStateEntered();
	}
}