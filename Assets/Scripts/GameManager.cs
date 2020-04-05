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
    public string SelectedLetters;  //TODO change text to take info from SelectedCells
    public List<Cell> SelectedCells;
    // Start is called before the first frame update
    void Start() {
        SelectedString=GameObject.Find("Selected Display").GetComponent<Text>();
        List<Cell> SelectedCells = new List<Cell>();
    }

    // Update is called once per frame
    void Update() {

    }
    public void AddToSelected(char toAdd) { //TODO replace with class array equivalent
        SelectedLetters+=toAdd;
        SelectedString.text=SelectedLetters;
    }
    public void RemoveFromSelected() {  //TODO replace with class array equivalent
        SelectedLetters=SelectedLetters.Remove(SelectedLetters.Length-1);
        SelectedString.text=SelectedLetters;
    }
    public void AddCellToSelected(string cellName,char cellLetter,Collider2D[] cellAdjacent) {  //TODO limit selection to adjacent
        if(SelectedCells==null)
            SelectedCells = new List<Cell>();
        Cell test = new Cell() { name=cellName,letter=cellLetter,adjacent=cellAdjacent };
        SelectedCells.Add(test);
        Debug.Log("Added "+cellName+" "+cellLetter);
    }
    public void RemoveCellFromSelected(string cellName,char cellLetter,Collider2D[] cellAdjacent) {
        if(SelectedCells==null)
            SelectedCells=new List<Cell>();
        Cell test = new Cell() { name=cellName,letter=cellLetter,adjacent=cellAdjacent };
        SelectedCells.Remove(test);
        Debug.Log("Removed "+cellName+" "+cellLetter);
    }
}
