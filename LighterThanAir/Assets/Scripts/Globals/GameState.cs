using UnityEngine;
using System.Collections.Generic;

/*
 * How to use:
 * 1. Place on a Game object in the scene that you want to be persistant throughout all levels.
 * 
 * @ Brian Chen
*/


public class GameState : MonoBehaviour {

	//Cachables Here
	private Transform t_transition;
	private SpriteRenderer spriteRenderer;

	//Game Logic Here


	//Game Progression
	public List<string> SCENES;
	private int i_CurrentLevel = 0;
	private int i_NextLevelQueue = 0; //set this before fading out to change levels.

	//Singleton Classes
	Timer t_timers = new Timer();
	InputManager inp_inputManager = new InputManager();

	//Game Properties Here
	private int f_fadeRepeats = 10; // <---- How many times do I repeat the fade interval.
	private float f_fadeInterval = 0.1f; // <--Every 0.1 seconds fade is called.

#region MonoBehaviour functions
	void Awake(){
		InitSingleton(); //Initialize singleton -- DO NOT TOUCH
	}

	void Start(){
		//The script this is on is the transition game object. This can change if need be.
		t_transition = transform;
		spriteRenderer = t_transition.GetComponent<SpriteRenderer>();
		OnLevelWasLoaded(Application.loadedLevel);
	}

	void Update(){
		inp_inputManager.Update();
		t_timers.Update(); //you are required to do this because the timers will not update otherwise.
	}

	void OnLevelWasLoaded(int i){
		if (Application.loadedLevelName.Equals(GetNextLevelString(i_CurrentLevel))){
			t_timers.Add("delaylevel", FadeInNewLevel, 0.3f, false);
		}
	}
#endregion

#region Gameplay
	public void FadeInNewLevel(){
		i_CurrentLevel = i_NextLevelQueue;
		i_NextLevelQueue = 0;
		FadeIn();
	}
	public void FadeOutToNextLevel(){
		i_NextLevelQueue = GetNextLevelInt(i_CurrentLevel);
		FadeOut();
	}
	public void FadeOutToMenu(){
		i_NextLevelQueue = 0;
		FadeOut();
	}
	#region private functions
	private void FadeOut(){
		spriteRenderer.enabled = true;
		spriteRenderer.color = Color.clear;
		//Example of every 0.1 seconds for 1 second fade out.
		helper_ResetLerpFactor(); //HACK--> this is to get lerp factoring to work.
		t_timers.Add("fadeout", call_FadeOut, f_fadeInterval, true, f_fadeRepeats, call_NextLevelQueued); //10 * 0.1 = 1 second
	}
	private void FadeIn(){
		spriteRenderer.color = Color.white;
		spriteRenderer.enabled = true;
		//Example of every 0.1 seconds for 1 second fade in.
		helper_ResetLerpFactor(); //HACK--> this is to get lerp factoring to work.
		t_timers.Add("fadein", call_FadeIn, f_fadeInterval, true, f_fadeRepeats, call_DisableSprRender); //10 * 0.1 = 1 second
	}
	private string GetLevelName(int i){
		return SCENES[i];
	}
	private string GetNextLevelString(int i){
		//if level is not the last level.
		return SCENES[GetNextLevelInt(i)];
	}
	private int GetNextLevelInt(int i){
		//if level is not the last level.
		if (0<=i && i < SCENES.Count-1){
			return i+1;
		}else{
			return 0;
		}
	}
	#endregion
#endregion

#region CallBacks

	#region Reset Lerp Factor
	private float helper_lerpFactor = 0;
	private void helper_ResetLerpFactor(){
		helper_lerpFactor = 0;
	}
	#endregion
	public void call_FadeOut(){
		helper_lerpFactor ++;
		spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, helper_lerpFactor/f_fadeRepeats);
	}
	public void call_FadeIn(){
		helper_lerpFactor ++;
		spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.clear, helper_lerpFactor/f_fadeRepeats);
	}
	public void call_DisableSprRender(){
		spriteRenderer.enabled = false;
	}
	public void call_NextLevelQueued(){
		Application.LoadLevel(GetNextLevelString(i_NextLevelQueue));
	}
#endregion

#region Properties and Getters and Setters
	public Timer Timers{
		get {return t_timers;}
	}
	public InputManager InputManager{
		get{return inp_inputManager;}
	}

#endregion
	
#region Singleton Instantiation
	private static GameState _instance;
	private int _AmSingleton = 0;
	private static object _lock = new object();

	private void InitSingleton(){
	
		if (FindObjectsOfType(typeof (GameState)).Length > 1){
			if (_AmSingleton == 0){
				Debug.Log ("Already Instantiated GameState");
				DestroyImmediate(gameObject);
				return;
			}
		}
		DontDestroyOnLoad(gameObject);
		_AmSingleton = 1;
		_instance = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
	}

	public static GameState Get
	{
		get
		{
			if (_instance.applicationIsQuitting) {
				Debug.LogWarning("[Singleton] Instance '"+ typeof(GameState) +
				                 "' already destroyed on application quit." +
				                 " Won't create again - returning null.");
				return null;
			}
			
			lock(_lock)
			{
				if (_instance == null)
				{
					_instance = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
					
					if ( FindObjectsOfType(typeof(GameState)).Length > 1 )
					{
						Debug.LogError("[Singleton] Something went really wrong " +
						               " - there should never be more than 1 singleton!" +
						               " Reopenning the scene might fix it.");
						return _instance;
					}
					
					if (_instance == null)
					{
						GameObject singleton = new GameObject();
						_instance = singleton.AddComponent<GameState>();
						singleton.name = "(singleton) "+ typeof(GameState).ToString();
						
						DontDestroyOnLoad(singleton);
						
						Debug.Log("[Singleton] An instance of " + typeof(GameState) + 
						          " is needed in the scene, so '" + singleton +
						          "' was created with DontDestroyOnLoad.");
					} else {
						Debug.Log("[Singleton] Using instance already created: " +
						          _instance.gameObject.name);
					}
				}
				
				return _instance;
			}
		}
	}
	
	
	private bool applicationIsQuitting = false;
	/// <summary>
	/// When Unity quits, it destroys objects in a random order.
	/// In principle, a Singleton is only destroyed when application quits.
	/// If any script calls Instance after it have been destroyed, 
	///   it will create a buggy ghost object that will stay on the Editor scene
	///   even after stopping playing the Application. Really bad!
	/// So, this was made to be sure we're not creating that buggy ghost object.
	/// </summary>
	public void OnDestroy () {
		applicationIsQuitting = true;
	}
	
#endregion
	

}
