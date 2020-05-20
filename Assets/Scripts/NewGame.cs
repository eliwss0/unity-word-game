using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGame : MonoBehaviour {
    public float TimeLeft = 150.0f;
    public Text DispTime;
    // Start is called before the first frame update
    void Start() {
        
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
        GameObject[] cells=GetCells();
        for(int i = 0;i<cells.Length;i++) {
            cells[i].GetComponent<CellBehavior>().RandLetter();
        }
        //TODO: Start Timer when button is pressed
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
