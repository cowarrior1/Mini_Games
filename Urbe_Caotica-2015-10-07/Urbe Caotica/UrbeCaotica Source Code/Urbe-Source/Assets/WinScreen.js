#pragma strict
private var mainScreenScript 		: GameController;
private var timeBlock 				: TimerScript;
function Start(){
	mainScreenScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(GameController);
	timeBlock = GameObject.Find("Timer").GetComponent(TimerScript);
}

function Update () {
	if(Input.GetMouseButtonDown(0)){
		var mousePos : Vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var hitCollider : Collider2D = Physics2D.OverlapPoint(mousePos);

		if(hitCollider.gameObject.tag == "Respawn"){
			mainScreenScript.StopAllAudio();
			timeBlock.EnableBlocks();
			mainScreenScript.ResetScene();
		}
		else if(hitCollider.gameObject.tag == "home"){
			Debug.Log("I hit: "+ hitCollider.gameObject.tag);
			Application.LoadLevel("MainScene");
		}
	}
}