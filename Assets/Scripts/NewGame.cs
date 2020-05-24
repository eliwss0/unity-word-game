using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour {
    public SpriteRenderer PauseObscSpriteRend;
    public BoxCollider2D PauseObscCollider;
    public float TimeLeft = 150.0f;
    public Text DispTime;
    // Start is called before the first frame update
    void Start() {
        PauseObscSpriteRend.color=new Color(0,0,0,0);
        Time.timeScale=0;
    }

    // Update is called once per frame
    void Update() {
        TimeLeft-=Time.deltaTime;
        DispTime.text=(TimeLeft).ToString("0");
        if(TimeLeft<=0) {
            SceneManager.LoadScene("ScoreScreen",LoadSceneMode.Single);
        }
    }
    public void StartNewGame() {
        Time.timeScale=1;
        TimeLeft=150.0f;
        if(Time.timeScale==1) {
            GameObject[] cells = GetCells();
            for(int i = 0;i<cells.Length;i++) {
                cells[i].GetComponent<CellBehavior>().RandLetter();
            }
        }
    }
    public void PauseToggle() {
        if(Time.timeScale==1) { //pause if unpaused
            Time.timeScale=0;
            PauseObscSpriteRend.color=new Color(1,1,1,1);
        }
        else if(Time.timeScale==0) {  //unpause if paused
            Time.timeScale=1;
            PauseObscSpriteRend.color=new Color(0,0,0,0);
        }
        else
            Debug.Log("Pause is broken");
    }
    public void EndGame() {
        GameObject[] cells = GetCells();
        for(int i = 0;i<cells.Length;i++) {
            cells[i].GetComponent<CellBehavior>().currentLetter=' ';
        }
    }
    public GameObject[] GetCells() {
        CellBehavior[] CellBehaviors = FindObjectsOfType<CellBehavior>();
        GameObject[] objects = new GameObject[CellBehaviors.Length];
        for(int i = 0;i<objects.Length;i++) {
            objects[i]=CellBehaviors[i].gameObject;
        }
        return objects;
    }
}
