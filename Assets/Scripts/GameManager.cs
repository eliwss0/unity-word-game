using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public class Cell {
        public string name;
        public char letter;
        public Collider2D[] adjacent;
    };
    public Text SelectedString;
    public string SelectedLetters;
    public List<Cell> SelectedCells;
    // Start is called before the first frame update
    void Start() {
        SelectedString=GameObject.Find("Selected Display").GetComponent<Text>();
        SelectedString.text="";
    }

    // Update is called once per frame
    void Update() {

    }
    void ChangeDisplayText() {
        SelectedString.text=SelectedLetters;
    }
    public void AddToSelected(char toAdd) { //will be replaced with struct array equivalent
        SelectedLetters+=toAdd;
        ChangeDisplayText();
    }
    public void RemoveFromSelected() {  //will be replaced with struct array equivalent
        SelectedLetters=SelectedLetters.Remove(SelectedLetters.Length-1);
        ChangeDisplayText();
    }
    public void AddCellToSelected(string cellName,char cellLetter,Collider2D[] cellAdjacent) {
        Cell test = new Cell() { name=cellName,letter=cellLetter,adjacent=cellAdjacent };
        SelectedCells.Add(test);
        Debug.Log("Added "+cellName);
    }
}
