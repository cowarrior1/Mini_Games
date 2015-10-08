#pragma strict
private var mainScreenScript 		: GameController;
public var style   					: GUISkin;
private var showSettings 			: boolean;
private var mute 					: boolean;
private var playSounds 				: boolean = true;
private var windo 					: Rect;

function Start(){
	mainScreenScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(GameController);
	windo = Rect(0,0, Screen.width, Screen.height);
	if(playSounds){
		audio.Play();
	}
	
}

function Update () {
	if(Input.GetMouseButtonDown(0)){
		var mousePos : Vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var hitCollider : Collider2D = Physics2D.OverlapPoint(mousePos);

		if(hitCollider.gameObject.tag == "Respawn"){
			Application.LoadLevel("Game");
		}
	}
}

function OnGUI(){
	GUI.skin = style;
	 GUI.BeginGroup(Rect(0, 0, Screen.width, Screen.height));
	 	GUI.Box(Rect(0, Screen.height - 100, Screen.width, 70), "", "mainSubMenu");
	    	if(GUI.Button(Rect(40, Screen.height - 100, 70, 70),"","settings")){
	    		showSettings = true;
	    	}
	    	if(GUI.Button(Rect(150, Screen.height - 100, 70, 70),"","homeBtn")){
				Application.LoadLevel("MainScene");
			}

			if(showSettings){
    			windo = GUI.Window(1, windo, DoMyWindow, "");
    		}
	 GUI.EndGroup();
}

function DoMyWindow(windowID : int){
	GUI.Box(Rect(Screen.width/2-210, Screen.height/2 - 140, 420, 280 ),"","setBack");

	if(GUI.Button(Rect(Screen.width/2 +160, Screen.height/2 -155, 40, 40),"","exitSet")){
		showSettings = false;
	}
	if(!mute){
		playSounds = true;
		GUI.Button(Rect(Screen.width/2 -140, Screen.height/2 -40, 90, 90),"","soundONGreen");
		if( GUI.Button(Rect(Screen.width/2 + 40, Screen.height/2 - 50, 110, 110),"","soundOFF")){
			playSounds = false;
			mute = true;
		}

	}
	else{
		if( GUI.Button(Rect(Screen.width/2 -140, Screen.height/2 -40, 90, 90),"","soundON")){
			mute = false;
		}
		GUI.Button(Rect(Screen.width/2 + 40, Screen.height/2 - 50, 110, 110),"","soundOFFGreen");
	}
}