using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void startGame(){

		SceneManager.LoadScene ("cutScene", LoadSceneMode.Single); //load cut scene
	}
	public void startTutorial(){

		SceneManager.LoadScene ("Tutorial1", LoadSceneMode.Single);//load Tutorial1 scene
	}
	public void gameControls(){

		SceneManager.LoadScene ("ControlsScene", LoadSceneMode.Single);//load Controls scene
	}

	public void quitGame(){

		Application.Quit ();//close application
	}
}
