#pragma strict
private var mainScreenScript 		: MainScreen;
private var load 					: boolean;
function Start(){
	mainScreenScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(MainScreen);
}

function Update () {
	if(Input.GetMouseButtonDown(0)){
		var mousePos : Vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var hitCollider : Collider2D = Physics2D.OverlapPoint(mousePos);

		if(hitCollider.gameObject.tag == "letterOBtn"){
			load = true;
			mainScreenScript.StopSound();
			audio.Play();
			
			
		}
	}
	
	if(load){
			Application.LoadLevel("Game");
	}
	
}