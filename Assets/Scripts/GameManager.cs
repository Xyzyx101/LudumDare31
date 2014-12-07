using UnityEngine;
using System.Collections;

//globals
enum stats{
	Vitality,
	Speed,
	Dextarity,
	Defense,
	Strength
};

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance { get { return instance; } }
	private static GameManager instance = null;
	
	public GameState currentState;
	public StateGamePlaying stateGamePlaying{get;set;}
	public StateGameLost stateGameLost{get;set;}
	public StateGameIntro stateGameIntro{get;set;}
	public StateGameMenu stateGameMenu{get;set;}

	
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

	}	
	
	private void Start () 
	{
		NewGameState( stateGameMenu );
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
	
    public void SetPLaying()
    {
        NewGameState(stateGamePlaying);
    }
}