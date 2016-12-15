using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {
	
	
	public bool isPause = false;
	public GameObject pausePanel;
	public GameObject resultPanel;
	public GameObject[] hideGameObjects;
	public GameObject resultLabel;
	public GameObject killLabel;
	 int noOfKill = 3;
	
	
	// Use this for initialization
	void Start () {
		noOfKill = Random.Range(2,6);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void pauseGame(){
		isPause = true;
		Time.timeScale = 0;
		pausePanel.SetActive (true);
		foreach (GameObject GO in hideGameObjects) {
			GO.SetActive(false);
		}
	}
	
	public void ResumeGame(){
		pausePanel.SetActive (false);
		Time.timeScale = 1;
		foreach (GameObject GO in hideGameObjects) {
			GO.SetActive(true);
		}
	}
	
	public void ExitGame (){
		Time.timeScale = 1;
		Application.LoadLevel("MenuScreen");
	}
	
	void GoToNextRound (){
		Time.timeScale = 1;
		Application.LoadLevel("stage3");
	}
	
	public void showResultScreen (string text){
		foreach (GameObject GO in hideGameObjects) {
			GO.SetActive(false);
		}
		resultLabel.GetComponent<UILabel> ().text = text;
		killLabel.GetComponent<UILabel>().text = "Kills: "+ noOfKill.ToString();
		Time.timeScale = 0;
		resultPanel.SetActive (true);
	}
}