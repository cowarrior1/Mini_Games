#pragma strict
//Developer: Alexandru Nistor
//Place: Suceva, Romania
//Date of finish: 21.07.2014
//Company: HomeDevelopment


public var style   						: GUISkin;
private var windo 						: Rect;
private var btnLimitation 				: int;
private var correctAnswer 				: String;
private var userAnswer 					: String;
public  var CountTimer	 				: float = 0;
public var CountTimer2 					: float = 0;

public var audioClipArray 				: AudioClip[];
public var audioClipBtn 				: AudioClip[];
private var soundObj 					: AudioSource;
private var allAudioSources 			: AudioSource[];

private var play 						: boolean;
private var playSounds 					: boolean = true;

public var showExit 					: boolean;
private var trash 						: boolean;
private var swere 						: boolean;
private var swing 						: boolean;
private var ind 						: boolean;
private var subway 						: boolean;
private var bus 						: boolean;
private var wast 						: boolean;
private var tree 						: boolean;
private var sema 						: boolean;
private var clean 						: boolean;
private var cars 						: boolean;
private var house 						: boolean;
private var showTutorial 				: boolean;
private var showNegFeed 				: boolean;
private var showPosFeed 				: boolean;

public var gameOver 					: boolean;
private var mute 						: boolean;


private var trashBin 					: SpriteRenderer;
private var swereSpr 					: SpriteRenderer;
private var swingSpr 					: SpriteRenderer;
private var indSpr 						: GameObject[];
private var subwaySpr 					: SpriteRenderer;
private var busSpr 						: SpriteRenderer;
private var wasteSpr 					: SpriteRenderer;
private var treeSpr 					: GameObject[];
private var semaSpr 					: SpriteRenderer;
private var cleanSpr 					: GameObject[];
private var carsSpr 					: GameObject[];
private var houseSpr 					: GameObject[];
private var timeBlock 					: GameObject[];
private var tut 						: GameObject;
private var negativeFeed 				: SpriteRenderer;
private var postiveFeed 				: GameObject;

private var scriptTime 					: TimerScript;
private var playedIntro 				: int;
private var stats 						: PlayerPrefs;

function Start () {
	if(stats.HasKey("PlayUrbe")){
		playedIntro = stats.GetInt("PlayUrbe");
	}
	if(playedIntro != 1){
		if(playSounds){
			PlaySound(7);
		}
	}
	
	windo = Rect(0,0, Screen.width, Screen.height);
	trashBin = GameObject.FindGameObjectWithTag("trashBin").GetComponent(SpriteRenderer);
	swereSpr = GameObject.FindGameObjectWithTag("swere").GetComponent(SpriteRenderer);
	swingSpr = GameObject.FindGameObjectWithTag("swing").GetComponent(SpriteRenderer);
	indSpr = GameObject.FindGameObjectsWithTag("industry");
	subwaySpr = GameObject.FindGameObjectWithTag("subway").GetComponent(SpriteRenderer);
	busSpr = GameObject.FindGameObjectWithTag("buss").GetComponent(SpriteRenderer);
	wasteSpr = GameObject.FindGameObjectWithTag("waste").GetComponent(SpriteRenderer);
	treeSpr = GameObject.FindGameObjectsWithTag("tree");
	semaSpr = GameObject.FindGameObjectWithTag("traffic").GetComponent(SpriteRenderer);
	cleanSpr = GameObject.FindGameObjectsWithTag("clean");
	carsSpr = GameObject.FindGameObjectsWithTag("cars");
	houseSpr = GameObject.FindGameObjectsWithTag("house");
	timeBlock = GameObject.FindGameObjectsWithTag("timerBlock");
	tut = GameObject.FindGameObjectWithTag("tutorial");
	negativeFeed = GameObject.FindGameObjectWithTag("negativeFeed").GetComponent(SpriteRenderer);
	postiveFeed = GameObject.FindGameObjectWithTag("positiveFeed");
	scriptTime = GameObject.Find("Timer").GetComponent(TimerScript);
	soundObj = GameObject.Find("SOund").GetComponent(AudioSource);
	allAudioSources = FindObjectsOfType(AudioSource) as AudioSource[];

	correctAnswer = "111111";
	userAnswer = "";
	negativeFeed.enabled = false;
	postiveFeed.SetActive(false);
	btnLimitation = 1;
	tut.SetActive(false);
	trashBin.enabled = false;
	swereSpr.enabled = false;
	swingSpr.enabled = false;
	for(var i =0; i<= indSpr.Length-1; i++){
		indSpr[i].SetActive(false);
	}
	subwaySpr.enabled = false;
	busSpr.enabled = false;
	wasteSpr.enabled = false;
	for(var j =0; j<= treeSpr.Length-1; j++){
		treeSpr[j].SetActive(false);
	}
	semaSpr.enabled = false;
	for(var k =0; k<= cleanSpr.Length-1; k++){
		cleanSpr[k].SetActive(false);
	}
	for(var l =0; l<= carsSpr.Length-1; l++){
		carsSpr[l].SetActive(false);
	}
	for(var m =0; m<= houseSpr.Length-1; m++){
		houseSpr[m].SetActive(false);
	}

	
}

function OnGUI() {
		GUI.skin = style;
		
		if(gameOver){
			Application.LoadLevel("OutOfTime");
		}
	    
	    GUI.BeginGroup(Rect(0, 0, Screen.width, Screen.height));
	    	GUI.Box(Rect(0, Screen.height - 100, Screen.width, 70), "", "mainSubMenu");
	    	IcoArray();
	    	BtnArray();
	    	if(GUI.Button(Rect(40, Screen.height - 100, 70, 70),"","settings")){
	    		MainScreen.showSettings = true;
	    	}
	    	if(GUI.Button(Rect(150, Screen.height - 100, 70, 70),"","homeBtn")){
	    		StopAllAudio();
	    		showTutorial = true;
	    		tut.SetActive(false);
	    		if(playSounds){
	    			PlaySound(4);
	    			soundObj.audio.clip = audioClipBtn[1];
	    			soundObj.audio.Play();
	    		}
	    		
    			showExit = true;
    		}
    		if(GUI.Button(Rect(260, Screen.height - 100, 70, 70),"","tutorial")){
    			
    			if(showTutorial){
    				showTutorial = false;
    				StopSound();
    				tut.SetActive(false);
    			}else
    			{    		
    				if(playSounds){
	    				soundObj.audio.clip = audioClipBtn[1];
		    			soundObj.audio.Play();
	    				PlayTutSounds();
    				}		
    				showTutorial = true;
    				tut.SetActive(true);
    			}
    			
    		}

    		if(btnLimitation < 7){
    			GUI.Label(Rect(Screen.width - 135, Screen.height/2+120, 120, 60),"","confirmFaded");
    		}
    		else{
    			if(GUI.Button(Rect(Screen.width - 135, Screen.height/2+120, 120, 60),"","confirm")){
    				if(correctAnswer == userAnswer){
    					showPosFeed = true;
    					if(playSounds){
		    				soundObj.audio.clip = audioClipBtn[2];
			    			soundObj.audio.Play();
    						PlaySound(6);
    					}
    					
    					Debug.Log("Correct!");
    				}
    				else{
    					Debug.Log("Wrong");
    					if(playSounds){
    						soundObj.audio.clip = audioClipBtn[4];
	    					soundObj.audio.Play();
    						PlaySound(5);
    					}
    					Application.LoadLevel("OutOfTime");
    					// showNegFeed = true;
    					
    				}
    			}
    		}

    		if(showPosFeed){
    			postiveFeed.SetActive(true);
    		}
    		if(showNegFeed){
    			negativeFeed.enabled = true;
    			if(Timer2(3)){
    				ResetScene();
    			}
    			
    		}
	    	if(MainScreen.showSettings){
    			windo = GUI.Window(1, windo, DoMyWindow, "");
    		}
    		if(showExit){
    			windo = GUI.Window(2, windo, ExitWindow, "");
    		}
	    GUI.EndGroup();
}
function PlayTutSounds(){
	PlaySound(0);
}

function DoMyWindow(windowID : int){
	GUI.Box(Rect(Screen.width/2-210, Screen.height/2 - 140, 420, 280 ),"","setBack");

	if(GUI.Button(Rect(Screen.width/2 +160, Screen.height/2 -155, 40, 40),"","exitSet")){
		if(playSounds){
			soundObj.audio.clip = audioClipBtn[1];
	    	soundObj.audio.Play();
		}
		MainScreen.showSettings = false;
	}

	if(!mute){
		playSounds = true;
		GUI.Button(Rect(Screen.width/2 -140, Screen.height/2 -40, 90, 90),"","soundONGreen");
		if( GUI.Button(Rect(Screen.width/2 + 40, Screen.height/2 - 50, 110, 110),"","soundOFF")){
			playSounds = false;
			mute = true;
			audio.Stop();
			soundObj.audio.Stop();
		}

	}
	else{
		if( GUI.Button(Rect(Screen.width/2 -140, Screen.height/2 -40, 90, 90),"","soundON")){
			mute = false;
		}
		GUI.Button(Rect(Screen.width/2 + 40, Screen.height/2 - 50, 110, 110),"","soundOFFGreen");
	}

}

function ExitWindow(windowID : int){
	GUI.Box(Rect(Screen.width/2-210, Screen.height/2 - 140, 420, 280 ),"","setBackExit");

	if(GUI.Button(Rect(Screen.width/2 -140, Screen.height/2 + 30, 90, 60),"","sim")){
		if(playSounds){
			soundObj.audio.clip = audioClipBtn[1];
	    	soundObj.audio.Play();
		}
		Application.LoadLevel("MainScene");
	}
	if(GUI.Button(Rect(Screen.width/2 + 40, Screen.height/2 + 30, 90, 60),"","nao")){
		if(playSounds){
			soundObj.audio.clip = audioClipBtn[1];
	    	soundObj.audio.Play();
		}
		showExit = false;
	}
}

function ResetScene(){
	trash = swere = swing =	ind = subway = bus = wast = tree = sema = false;
	clean = cars = house = showPosFeed = false;
	showNegFeed = false;
	userAnswer = "";
	CountTimer = 0;
	CountTimer2 = 0;
	
	btnLimitation = 1;
	negativeFeed.enabled = false;
	postiveFeed.SetActive(false);
	showNegFeed = false;
	tut.SetActive(false);
	trashBin.enabled = false;
	swereSpr.enabled = false;
	swingSpr.enabled = false;
	for(var i =0; i<= indSpr.Length-1; i++){
		indSpr[i].SetActive(false);
	}
	subwaySpr.enabled = false;
	busSpr.enabled = false;
	wasteSpr.enabled = false;
	for(var j =0; j<= treeSpr.Length-1; j++){
		treeSpr[j].SetActive(false);
	}
	semaSpr.enabled = false;
	for(var k =0; k<= cleanSpr.Length-1; k++){
		cleanSpr[k].SetActive(false);
	}
	for(var l =0; l<= carsSpr.Length-1; l++){
		carsSpr[l].SetActive(false);
	}
	for(var m =0; m<= houseSpr.Length-1; m++){
		houseSpr[m].SetActive(false);
	}
}

function IcoArray(){
	TrashBin();
	Sweres();
	Swing();
	Industry();
	Subway();
	Waste();
	Trees();
	Buss();
	Traffic();
	Cleanex();
	Cars();
	House();
}

function TrashBin(){
	if(!trash){
		if(GUI.Button(Rect(70, Screen.height/2+120, 60, 60),"","trashBin")){
			if(btnLimitation<7){
				btnLimitation++;
				trash = true;
				userAnswer += "1";
				Debug.Log("userAnswer"+ userAnswer);
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				trashBin.enabled = true;
			}
		}
	}else{
		if(GUI.Button(Rect(70, Screen.height/2+120, 60, 60),"","trashBinPress")){
			
		}
	}
	
}

function Sweres(){
	if(!swere){
		if(GUI.Button(Rect(130, Screen.height/2+120, 60, 60),"","sewer")){
			if(btnLimitation<7){
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				btnLimitation++;
				swere = true;
				swereSpr.enabled = true;
			}	
		}
	}else{
		if(GUI.Button(Rect(130, Screen.height/2+120, 60, 60),"","sewerPress")){
			
		}
	}
}

function Swing(){
	if(!swing){
		if(GUI.Button(Rect(190, Screen.height/2+120, 60, 60),"","swing")){
			if(btnLimitation<7){
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				userAnswer += "1";
				btnLimitation++;
				swing = true;
				swingSpr.enabled = true;
			}
		}
	}else{
		if(GUI.Button(Rect(190, Screen.height/2+120, 60, 60),"","swingPress")){
			
		}
	}
}

function Industry(){
	if(!ind){
		if(GUI.Button(Rect(250, Screen.height/2+120, 60, 60),"","industry")){
			if(btnLimitation<7){
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				btnLimitation++;
				ind = true;
				for(var i = 0; i<= indSpr.Length-1; i++){
					indSpr[i].SetActive(true);
				}
			}
		}
	}else{
		if(GUI.Button(Rect(250, Screen.height/2+120, 60, 60),"","industryPress")){
			
		}
	}
}

function Subway(){
	if(!subway){
		if(GUI.Button(Rect(310, Screen.height/2+120, 60, 60),"","subway")){
			if(btnLimitation<7){
				userAnswer += "1";
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				btnLimitation++;
				subway = true;
				if(busSpr.enabled == true){
					subwaySpr.enabled = true;	
				}
				
			}
		}
	}else{
		if(GUI.Button(Rect(310, Screen.height/2+120, 60, 60),"","subwayPress")){
			
		}
	}
}

function Waste(){
	if(!wast){
		if(GUI.Button(Rect(370, Screen.height/2+120, 60, 60),"","waste")){
			if(btnLimitation<7){
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				btnLimitation++;
				wast = true;
				wasteSpr.enabled = true;
				swereSpr.enabled = true;
			}
		}
	}else{
		if(GUI.Button(Rect(370, Screen.height/2+120, 60, 60),"","wastePress")){
			
		}
	}
}

function Trees(){
	if(!tree){
		if(GUI.Button(Rect(430, Screen.height/2+120, 60, 60),"","tree")){
			if(btnLimitation<7){
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				userAnswer += "1";
				btnLimitation++;
				tree = true;
				for(var j = 0; j<= treeSpr.Length-1; j++){
					treeSpr[j].SetActive(true);
				}
			}
		}
	}else{
		if(GUI.Button(Rect(430, Screen.height/2+120, 60, 60),"","treePress")){
			
		}
	}
}

function Buss(){
	if(!bus){
		if(GUI.Button(Rect(490, Screen.height/2+120, 60, 60),"","bus")){
			if(btnLimitation<7){
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				btnLimitation++;
				bus = true;
				busSpr.enabled = true;
			}
		}
	}else{
		if(GUI.Button(Rect(490, Screen.height/2+120, 60, 60),"","busPress")){
			
		}
	}
}

function Traffic(){
	if(!sema){
		if(GUI.Button(Rect(550, Screen.height/2+120, 60, 60),"","traffic")){
			if(btnLimitation<7){
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				userAnswer += "1";
				btnLimitation++;
				sema = true;
				semaSpr.enabled = true;
			}
		}
	}else{
		if(GUI.Button(Rect(550, Screen.height/2+120, 60, 60),"","trafficPress")){
			
		}
	}
}

function Cleanex(){
	if(!clean){
		if(GUI.Button(Rect(610, Screen.height/2+120, 60, 60),"","clean")){
			if(btnLimitation<7){
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				userAnswer += "1";
				btnLimitation++;
				clean = true;
				for(var k = 0; k<= cleanSpr.Length-1; k++){
					cleanSpr[k].SetActive(true);
				}
				semaSpr.enabled = false;
			}
		}
	}else{
		if(GUI.Button(Rect(610, Screen.height/2+120, 60, 60),"","cleanPress")){
			
		}
	}
}
function Cars(){
	if(!cars){
		if(GUI.Button(Rect(670, Screen.height/2+120, 60, 60),"","cars")){
			if(btnLimitation<7){
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				btnLimitation++;
				cars = true;
				for(var k = 0; k<= carsSpr.Length-1; k++){
					carsSpr[k].SetActive(true);
				}
			}
		}
	}else{
		if(GUI.Button(Rect(670, Screen.height/2+120, 60, 60),"","carsPress")){
			
		}
	}
}

function House(){
	if(!house){
		if(GUI.Button(Rect(730, Screen.height/2+120, 60, 60),"","house")){
			if(btnLimitation<7){
				if(playSounds){
					soundObj.audio.clip = audioClipBtn[0];
					soundObj.audio.Play();
				}
				btnLimitation++;
				house = true;
				for(var m = 0; m<= houseSpr.Length-1; m++){
					houseSpr[m].SetActive(true);
				}
			}
		}
	}else{
		if(GUI.Button(Rect(730, Screen.height/2+120, 60, 60),"","housePress")){
			
		}
	}
}

function Timer(time :  float) : boolean{
	if(CountTimer == 0){
		CountTimer = time;
	}
	else if(CountTimer > 0){
		CountTimer -= Time.deltaTime;
	}
	else{
		CountTimer = 0;
		return true;
	}
}

function StartTime(){
	if(Timer(120)){
		gameOver = true;
	}
	if(CountTimer>8.0 && CountTimer < 10.0){
		soundObj.audio.clip = audioClipBtn[3];
		soundObj.audio.Play();
	}
	
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

function BtnArray(){
	if(btnLimitation == 1){
		GUI.Button(Rect(Screen.width- 300, Screen.height-75, 16, 21),"","1");
		GUI.Button(Rect(Screen.width- 273, Screen.height-75, 16, 21),"","2");
		GUI.Button(Rect(Screen.width- 246, Screen.height-75, 16, 21),"","3");
		GUI.Button(Rect(Screen.width- 219, Screen.height-75, 16, 21),"","4");
		GUI.Button(Rect(Screen.width- 192, Screen.height-75, 16, 21),"","5");
		GUI.Button(Rect(Screen.width- 165, Screen.height-75, 16, 21),"","6");
	}
	else if(btnLimitation == 2){
		GUI.Button(Rect(Screen.width- 300, Screen.height-75, 16, 21),"","1");
		GUI.Button(Rect(Screen.width- 273, Screen.height-75, 16, 21),"","2");
		GUI.Button(Rect(Screen.width- 246, Screen.height-75, 16, 21),"","3");
		GUI.Button(Rect(Screen.width- 219, Screen.height-75, 16, 21),"","4");
		GUI.Button(Rect(Screen.width- 192, Screen.height-75, 16, 21),"","5");
	}
	else if (btnLimitation == 3){
		GUI.Button(Rect(Screen.width- 300, Screen.height-75, 16, 21),"","1");
		GUI.Button(Rect(Screen.width- 273, Screen.height-75, 16, 21),"","2");
		GUI.Button(Rect(Screen.width- 246, Screen.height-75, 16, 21),"","3");
		GUI.Button(Rect(Screen.width- 219, Screen.height-75, 16, 21),"","4");
	}
	else if (btnLimitation == 4){
		GUI.Button(Rect(Screen.width- 300, Screen.height-75, 16, 21),"","1");
		GUI.Button(Rect(Screen.width- 273, Screen.height-75, 16, 21),"","2");
		GUI.Button(Rect(Screen.width- 246, Screen.height-75, 16, 21),"","3");
	}
	else if (btnLimitation == 5){
		GUI.Button(Rect(Screen.width- 300, Screen.height-75, 16, 21),"","1");
		GUI.Button(Rect(Screen.width- 273, Screen.height-75, 16, 21),"","2");
	}
	else if (btnLimitation == 6){
		GUI.Button(Rect(Screen.width- 300, Screen.height-75, 16, 21),"","1");
	}
	
	GUI.Button(Rect(Screen.width- 396, Screen.height-75, 96, 21),"","selecionar");
}

function PlaySound(nr : int) {
	audio.clip = audioClipArray[nr];
	audio.Play();
}

function StopSound(){
	audio.Stop();
}

function StopAllAudio() {
    for(var audioS : AudioSource in allAudioSources) {
        audioS.Stop();
    }
}

function OnApplicationQuit() {
	stats.SetInt("PlayUrbe", 0);
	stats.Save();
}