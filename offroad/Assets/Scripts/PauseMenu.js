var mainMenuSceneName : String;
var pauseMenuFont : Font;
private var pauseEnabled = false;			

function Start(){
	pauseEnabled = false;
	Time.timeScale = 1;
	AudioListener.volume = 1;
	Screen.showCursor = false;
}

function Update(){
	if (Input.GetKeyDown("escape")) {
		if (pauseEnabled == true) {
			//unpause the game
			pauseEnabled = false;
			Time.timeScale = 1;
			AudioListener.volume = 1;
			Screen.showCursor = false;			
		} else if (pauseEnabled == false) {
			pauseEnabled = true;
			AudioListener.volume = 0;
			Time.timeScale = 0;
			Screen.showCursor = true;
		}
	}
}

private var showGraphicsDropDown = false;

function OnGUI() {
	GUI.skin.box.font = pauseMenuFont;
	GUI.skin.button.font = pauseMenuFont;
	if (pauseEnabled == true) {
		//Make a background box
		GUI.Box(Rect(Screen.width /2 - 100,Screen.height /2 - 100,250,100), "Pause");
		//Make quit game button
		if (GUI.Button (Rect (Screen.width / 2 - 100, Screen.height / 2 - 50, 250, 50), "Restart?")) {
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel);
		}
		if (GUI.Button (Rect (Screen.width / 2 - 100,Screen.height / 2 + 00, 250, 50), "Main menu?")) {
			Time.timeScale = 1;
			Application.LoadLevel("main-menu");
		}
	}
}