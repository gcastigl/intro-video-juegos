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
		if (pauseEnabled) {
			pauseEnabled = false;
			Time.timeScale = 1;
			AudioListener.volume = 1;
			Screen.showCursor = false;			
		} else {
			pauseEnabled = true;
			AudioListener.volume = 0;
			Time.timeScale = 0;
			Screen.showCursor = true;
		}
	}
}

private var showGraphicsDropDown = false;

function OnGUI(){
	GUI.skin.box.font = pauseMenuFont;
	GUI.skin.button.font = pauseMenuFont;
	if (pauseEnabled) {
		
		//Make a background box
		GUI.Box(Rect(Screen.width /2 - 100,Screen.height /2 - 150,250,200), "Pause Menu");
		
		//Make Main Menu button
		if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2 - 50,250,50), "Main Menu")) {
			Application.LoadLevel(mainMenuSceneName);
		}
		if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2 ,250,50), "Change Graphics Quality")) {
			if(showGraphicsDropDown == false) {
				showGraphicsDropDown = true;
			} else{
				showGraphicsDropDown = false;
			}
		}
		if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2-100,250,50), "Reload")) {
			Application.LoadLevel(Application.loadedLevel);
		}
		//Create the Graphics settings buttons, these won't show automatically, they will be called when
		//the user clicks on the "Change Graphics Quality" Button, and then dissapear when they click
		//on it again....
		if (showGraphicsDropDown == true) {
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 50,250,50), "Fast")){
				QualitySettings.SetQualityLevel(QualityLevel.Fast);
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 100,250,50), "Simple")){
				QualitySettings.SetQualityLevel(QualityLevel.Simple);
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 150,250,50), "Good")){
				QualitySettings.SetQualityLevel(QualityLevel.Good);
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 200,250,50), "Beautiful")){
				QualitySettings.SetQualityLevel(QualityLevel.Beautiful);
			}
			if(GUI.Button(Rect(Screen.width /2 + 150,Screen.height /2 + 250,250,50), "Fantastic")){
				QualitySettings.SetQualityLevel(QualityLevel.Fantastic);
			}
			if (Input.GetKeyDown("escape")) {
				showGraphicsDropDown = false;
			}
		}
		if (GUI.Button (Rect (Screen.width /2 - 100,Screen.height /2 + 50,250,50), "Quit Game")){
			Application.Quit();
		}
	}
}