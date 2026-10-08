using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour {


    public int Level = 1;
    public GameObject RestartBtn;
    public GameObject PassText;
    public Transform BallArr;
    public int BallCount;
    public bool isOver;

    public Text LevelText1;
    public Text LevelText2;

    public static GameController Instance;
    private void Awake() {
        Instance = this;
        Level = PlayerPrefs.GetInt("Level", 1);
        BallCount = Level + 5;
        LevelText1.text = Level.ToString();
        LevelText2.text = "Level：" + Level;
    }
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        
	}

    public void NextLevel() {
        Level++;
        PlayerPrefs.SetInt("Level", Level);
        Turn.Instance.OnStop();
        PassText.SetActive(true);
        GetComponent<AudioSource>().Play();
        Invoke("loadScane", 0.3f);
    }

    void loadScane() {
        SceneManager.LoadScene(0);
    }

    public void GameOver() {
        Time.timeScale = 0;
        Turn.Instance.OnStop();
        RestartBtn.SetActive(true);
        PlayerPrefs.SetInt("Level", Level);
    }

    public void ClickRestart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ClickReset() {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene(0);
    }

}
