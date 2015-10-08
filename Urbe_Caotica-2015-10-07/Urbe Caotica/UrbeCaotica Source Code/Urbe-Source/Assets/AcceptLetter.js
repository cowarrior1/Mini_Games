#pragma strict
private var mainScreenScript 		: MainScreen;
private var audioObj 				: AudioSource;
public var clip1 					: AudioClip;
function Start(){
	mainScreenScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent(MainScreen);
	audioObj = GameObject.Find("Sound").GetComponent(AudioSource);
}

function Update(){
	if(Input.GetMouseButtonDown(0)){
		var mousePos : Vector2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var hitCollider : Collider2D = Physics2D.OverlapPoint(mousePos);

		if(hitCollider.gameObject.tag == "letterAccept"){
			audioObj.audio.clip = clip1;
			audioObj.audio.Play();
			mainScreenScript.DisableLetterScene();
			mainScreenScript.letterOpen = true;
			mainScreenScript.letterAcc = false;
			mainScreenScript.play = false;
			gameObject.SetActive(false);
		}
	}
}