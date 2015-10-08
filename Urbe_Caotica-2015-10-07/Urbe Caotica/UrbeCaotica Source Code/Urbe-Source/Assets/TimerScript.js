#pragma strict

public var timeBlock				: Component[];
private var gameControl 			: GameController;
function Start () {
	timeBlock = GetComponentsInChildren(SpriteRenderer);
	gameControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(GameController);
}

function Update () {
	if(!gameControl.gameOver){
		gameControl.StartTime();
		DisableBlock();
	}
}

function DisableBlock(){
	if(gameControl.CountTimer < 108.0 && gameControl.CountTimer> 107.0){
		timeBlock[9].renderer.enabled = false;
		Debug.Log("Name " + timeBlock[9].name);
	}
	else if (gameControl.CountTimer > 95.0 && gameControl.CountTimer < 96.0){
		timeBlock[8].renderer.enabled = false;
		Debug.Log("Name " + timeBlock[8].name);
	}
	else if (gameControl.CountTimer > 83.0 && gameControl.CountTimer < 84.0){
		timeBlock[7].renderer.enabled = false;
		Debug.Log("Name " + timeBlock[7].name);
	}
	else if (gameControl.CountTimer > 71.0 && gameControl.CountTimer < 72.0){
		timeBlock[6].renderer.enabled = false;
		Debug.Log("Name " + timeBlock[6].name);
	}
	else if (gameControl.CountTimer > 59.0 && gameControl.CountTimer < 60.0){
		timeBlock[5].renderer.enabled = false;
		Debug.Log("Name " + timeBlock[5].name);
	}
	else if (gameControl.CountTimer > 47.0 && gameControl.CountTimer < 48.0){
		timeBlock[4].renderer.enabled = false;
		Debug.Log("Name " + timeBlock[4].name);
	}
	else if (gameControl.CountTimer > 35.0 && gameControl.CountTimer < 36.0){
		timeBlock[3].renderer.enabled = false;
		Debug.Log("Name " + timeBlock[3].name);
	}
	else if (gameControl.CountTimer > 23.0 && gameControl.CountTimer < 24.0){
		timeBlock[2].renderer.enabled = false;
		Debug.Log("Name " + timeBlock[2].name);
	}
	else if (gameControl.CountTimer > 11.0 && gameControl.CountTimer < 12.0){
		timeBlock[1].renderer.enabled = false;
		Debug.Log("Name " + timeBlock[1].name);
	}

}

function EnableBlocks(){
	for(var j =0; j <= timeBlock.Length-1; j++){
		timeBlock[j].renderer.enabled = true;
	}
}