#pragma strict
//Developer: Alexandru Nistor
//Place: Suceva, Romania
//Date of finish: 21.07.2014
//Company: HomeDevelopment

//Script Main Screen
import System.IO;
//Public Variables
public var mainBackground				: Texture2D;
public var style   						: GUISkin;

public var CountTimer2 					: int =0;

public static var showSettings 			: boolean;
public static var playSounds 			: boolean = true;
public var play 						: boolean;


public static var soundOnTog 			: boolean = true;
public static var soundOffTog 			: boolean;
private var mute 						: boolean;

public var showPresentation				: boolean;
public var elisaDest 					: Transform;
public var letterDest 					: Transform;
public var msgDest 						: Transform;
public var letterOpenDest 				: Transform;
public var audioClipArray 				: AudioClip[];
public var audioClipObj 				: AudioClip[];
//Private Variables
private var windo 						: Rect;
private var gameBackground				: SpriteRenderer;
private var urbeIco						: SpriteRenderer;
private var presTextArea 				: SpriteRenderer;

private var letterBG 					: SpriteRenderer;
private var letterMsg 					: GameObject;
private var letter 						: SpriteRenderer;
private var letterAccept 				: SpriteRenderer;

private var letterOpened				: SpriteRenderer;
private var letterOpenedBtn				: SpriteRenderer;

private var acceptLetter 				: AcceptLetter;
private var acceptMission 				: AcceptMission;
private var audioObj 					: AudioSource;


private var elisa 						: GameObject;
private var speed 						: float;
public var endPresentation 				: boolean;
public var letterOpen 					: boolean;
public var letterAcc 					: boolean;

private var presentationText 			: String;

private var elisaStartPos				: Transform;

private var stats 						: PlayerPrefs;
private var playedIntro 				: int;
function Awake(){
	if(!stats.HasKey("PlayUrbe")){
		stats.SetInt("PlayUrbe", 0);
	}
}
//Start Script
function Start(){
	if(stats.HasKey("PlayUrbe")){
		playedIntro = stats.GetInt("PlayUrbe");
	}
	if(playedIntro != 1){
		if(playSounds){
			audio.clip = audioClipArray[0];
			audio.Play();
			stats.SetInt("PlayUrbe", 1);
		}
	}

	windo = Rect(0,0, Screen.width, Screen.height);
	gameBackground = GameObject.FindGameObjectWithTag("gameBG").GetComponent(SpriteRenderer);
	urbeIco = GameObject.FindGameObjectWithTag("urbeIco").GetComponent(SpriteRenderer);
	presTextArea = GameObject.FindGameObjectWithTag("presText").GetComponent(SpriteRenderer);

	letterBG = GameObject.FindGameObjectWithTag("letterBG").GetComponent(SpriteRenderer);
	letter = GameObject.FindGameObjectWithTag("letter").GetComponent(SpriteRenderer);
	letterMsg = GameObject.FindGameObjectWithTag("letterMessage");
	letterAccept = GameObject.FindGameObjectWithTag("letterAccept").GetComponent(SpriteRenderer);
	acceptLetter = GameObject.FindGameObjectWithTag("letterAccept").GetComponent(AcceptLetter);
	acceptMission = GameObject.FindGameObjectWithTag("letterOBtn").GetComponent(AcceptMission);

	letterOpened = GameObject.FindGameObjectWithTag("letterOpen").GetComponent(SpriteRenderer);
	letterOpenedBtn = GameObject.FindGameObjectWithTag("letterOBtn").GetComponent(SpriteRenderer);
	audioObj = GameObject.Find("Sound").GetComponent(AudioSource);

	letterBG.enabled = false;
	letter.enabled = false;
	letterMsg.SetActive(false);
	letterAccept.enabled = false;
	acceptLetter.enabled = false;
	acceptMission.enabled = false;
	letterOpened.enabled = false;
	letterOpenedBtn.enabled = false;



	elisa = GameObject.Find("Elisa");
	elisaStartPos = elisa.gameObject.transform;
	presTextArea.enabled = false;

	presentationText = "\t\tAs cidades precisam de mudanças até se tornarem um ambiente melhor para se viver. \n\t\tNo momento, a Urbe Caótica necessita de ajustes para seus moradores serem mais felizes. Você vai receber uma missão deles. \n\t\t\tVamos lá!";

	speed = 6;
}

function OnGUI(){
	GUI.skin = style;
    
    GUI.BeginGroup(Rect(0, 0, Screen.width, Screen.height));
    	GUI.Box(Rect(0, Screen.height - 100, Screen.width, 70), "", "mainSubMenu");

    	if(!endPresentation){
    		if(!showPresentation){
    			if(GUI.Button(Rect(Screen.width-110, Screen.height - 100, 70, 70),"","play")){
    				showPresentation = true;
    				urbeIco.enabled = false;
    				presTextArea.enabled = true;
    				if(playSounds){
    					audio.clip = audioClipArray[1];
    					audio.Play();
    					audioObj.audio.clip = audioClipObj[0];
    					audioObj.audio.Play();
    				}
    			}
    		}
    		else{
    			if(GUI.Button(Rect(Screen.width-210, Screen.height - 85, 100, 40),"","jogar")){
    				if(playSounds){
    					audioObj.audio.clip = audioClipObj[0];
    					audioObj.audio.Play();
    				}
    				endPresentation = true;
    				letterAcc = true;
    				gameBackground.enabled = false;
    				presTextArea.enabled = false;
    				audio.Stop();
    				elisa.gameObject.SetActive(false);

    			}

    			GUI.Label(Rect(Screen.width/2-80, Screen.height/2 - 220, 460, Screen.height/2 +100), presentationText, "prezText");
    			MoveElisa(elisa.transform, elisaDest);
    		}
    	}
    	else if(letterAcc){
    		EnableLetterScene();

    		if(GUI.Button(Rect(150, Screen.height - 100, 70, 70),"","homeBtn")){
    			if(playSounds){
    				audioObj.audio.clip = audioClipObj[0];
    				audioObj.audio.Play();
    			}
    			Application.LoadLevel("MainScene");
    		}
    	}
    	else if(letterOpen){
    		EnabledLetterOpenScene();

    		if(GUI.Button(Rect(150, Screen.height - 100, 70, 70),"","homeBtn")){
    			if(playSounds){
    				audioObj.audio.clip = audioClipObj[0];
    				audioObj.audio.Play();
    			}
    			Application.LoadLevel("MainScene");

    		}
    	}

    	if(GUI.Button(Rect(40, Screen.height - 100, 70, 70),"","settings")){
    		if(playSounds){
    			audioObj.audio.clip = audioClipObj[0];
    			audioObj.audio.Play();
    		}
    		showSettings = true;
    	}

    	
    	

    	if(showSettings){
    		windo = GUI.Window(1, windo, DoMyWindow, "");
    	}

    GUI.EndGroup();
}

function PlaySound(nr : int){
	audio.clip = audioClipArray[nr];
	audio.Play();
}

function StopSound(){
	audio.Stop();
}

function DoMyWindow(windowID : int){
	GUI.Box(Rect(Screen.width/2-210, Screen.height/2 - 140, 420, 280 ),"","setBack");

	if(GUI.Button(Rect(Screen.width/2 +160, Screen.height/2 -155, 40, 40),"","exitSet")){
		if(playSounds){
			audioObj.audio.clip = audioClipObj[0];
    		audioObj.audio.Play();
		}
		showSettings = false;
	}
	if(!mute){
		playSounds = true;
		GUI.Button(Rect(Screen.width/2 -140, Screen.height/2 -40, 90, 90),"","soundONGreen");
		if( GUI.Button(Rect(Screen.width/2 + 40, Screen.height/2 - 50, 110, 110),"","soundOFF")){
			playSounds = false;
			mute = true;
			audio.Stop();
			audioObj.audio.Stop();
		}

	}
	else{
		if( GUI.Button(Rect(Screen.width/2 -140, Screen.height/2 -40, 90, 90),"","soundON")){
			mute = false;
		}
		GUI.Button(Rect(Screen.width/2 + 40, Screen.height/2 - 50, 110, 110),"","soundOFFGreen");
	}
}

function MoveElisa(target : Transform, destination : Transform){
	var step = speed * Time.deltaTime;

	target.transform.position = Vector3.MoveTowards(target.transform.position, destination.position, step);
}

public function EnableLetterScene(){
	if(playSounds){
		if(!play){
			PlaySound(2);
			play = true;
		}
	}
	
	letterAccept.enabled = true;
	letter.enabled = true;
	letterBG.enabled = true;
	acceptLetter.enabled = true;
	letterMsg.SetActive(true);
	MoveElisa(letter.gameObject.transform, letterDest);
}
public function DisableLetterScene(){
	letterAccept.enabled = false;
	letter.enabled = false;
	letterBG.enabled = false;
	letterMsg.SetActive(false);
}

function EnableMainMenuScene(){
	gameBackground.enabled = true;
	urbeIco.enabled = true;
	elisa.gameObject.transform.position = elisaStartPos.transform.position;
	elisa.gameObject.SetActive(true);

}

public function EnabledLetterOpenScene(){
	MoveElisa(letterOpened.gameObject.transform, letterOpenDest);
	letterOpenedBtn.enabled = true;
	letterOpened.enabled = true;
	letterBG.enabled = true;
	acceptMission.enabled = true;
	acceptLetter.enabled = true;
	if(playSounds){
		if(!play){
			PlaySound(4);
			play = true;
		}
	}
	
}

public function DisableLetterOpenScene(){
	letterOpenedBtn.enabled = false;
	letterOpened.enabled = false;
	letterBG.enabled = false;
	acceptLetter.enabled = false;
}

function Timer2(time :  float) : boolean{
	if(CountTimer2 == 0){
		CountTimer2 = time;
	}
	else if(CountTimer2 > 0){
		CountTimer2 -= Time.deltaTime;
	}
	else{
		CountTimer2 = 0;
		return true;
	}
}

function OnApplicationQuit() {
	stats.SetInt("PlayUrbe", 0);
	stats.Save();
}