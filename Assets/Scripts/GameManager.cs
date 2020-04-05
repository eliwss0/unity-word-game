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
        SelectedString.text="";
    }

    // Update is called once per frame
    void Update() {

    }
    public string CellListToString(List<Cell> cellList) {
        string cellString="";
        foreach(Cell cell in cellList) {
            cellString+=cell.letter;
        }
        return cellString;
    }
    public string UpdateSelectedString(List<Cell> cells) {
        string newDisplayString="";
        for(int i = 0;i<SelectedCells.Capacity;i++) {
            newDisplayString+=SelectedCells[0].letter;
        }
        return newDisplayString;
    }
    public int FindListIndex(char cellLetter,List<Cell> cellList) { //TODO fix only removing last occurance of char?
        for(int i = cellList.Count;i>0;i--) {
            if(cellList[i-1].letter==cellLetter) {
                return i-1;
            }
        }
        return -1;
    }
    public void AddCellToSelected(string cellName,char cellLetter,Collider2D[] cellAdjacent) {  //TODO limit selection to adjacent
        if(SelectedCells==null)
            SelectedCells = new List<Cell>();
        Cell test = new Cell() { name=cellName,letter=cellLetter,adjacent=cellAdjacent };
        SelectedCells.Add(test);
        Debug.Log("Added "+cellName+" "+cellLetter);
        Debug.Log(CellListToString(SelectedCells));
        SelectedString.text=CellListToString(SelectedCells);
    }
    public void RemoveCellFromSelected(string cellName,char cellLetter,Collider2D[] cellAdjacent) {
        if(SelectedCells==null)
            SelectedCells=new List<Cell>();
        SelectedCells.RemoveAt(FindListIndex(cellLetter,SelectedCells));
        Debug.Log("Removed "+cellName+" "+cellLetter);
        Debug.Log(CellListToString(SelectedCells));
        SelectedString.text=CellListToString(SelectedCells);
    }
}
