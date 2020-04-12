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
    public TextAsset Dictionary;
    // Start is called before the first frame update
    void Start() {
        SelectedString=GameObject.Find("Selected Display").GetComponent<Text>();
        List<Cell> SelectedCells = new List<Cell>();
        SelectedString.text="";
    }
    // Update is called once per frame
    void Update() {

    }
    public bool CheckWord(string word,TextAsset dict) {
        //TODO search dict for word
        return false;
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
    public int FindListIndex(string cellName,List<Cell> cellList) {
        for(int i = cellList.Count;i>=1;i--) {
            if(cellList[i-1].name==cellName) {
                return i-1;
            }
        }
        return -1;
    }
    public bool CheckAdjacent(Cell cell1,Cell cell2) {  //check if cell2 is adjacent to cell1
        for(int i = 0;i<cell1.adjacent.Length;i++) {
            if(cell2.name==cell1.adjacent[i].name)
                return true;
        }
        return false;
    }
    public bool CheckContains(List<Cell> Selected,string name) {
        for(int i = Selected.Count;i>=1;i--) {
            if(Selected[i-1].name==name) {
                return true;
            }
        }
        return false;
    }
    public bool AddCellToSelected(string cellName,char cellLetter,Collider2D[] cellAdjacent) {  //TODO limit selection to adjacent
        Cell toAdd = new Cell() { name=cellName,letter=cellLetter,adjacent=cellAdjacent };
        if(SelectedCells==null)
            SelectedCells = new List<Cell>();
        if((SelectedCells.Count>0&&CheckAdjacent(SelectedCells[SelectedCells.Count-1],toAdd))||SelectedCells.Count==0) {
            SelectedCells.Add(toAdd);
            //Debug.Log("Added "+cellName+" "+cellLetter);
            //Debug.Log("Selected: "+CellListToString(SelectedCells));
            SelectedString.text=CellListToString(SelectedCells);
            return true;
        }
        else
            return false;
    }
    public bool RemoveCellFromSelected(string cellName,char cellLetter,Collider2D[] cellAdjacent) {
        Cell toRemove = new Cell() { name=cellName,letter=cellLetter,adjacent=cellAdjacent };
        if((SelectedCells[SelectedCells.Count-1].name==toRemove.name)||CheckContains(SelectedCells,cellName)) {//TODO add condition to circumvent check for remove all
            SelectedCells.RemoveAt(FindListIndex(cellName,SelectedCells));
            //Debug.Log("Removed "+cellName+" "+cellLetter);
            //Debug.Log("Selected: "+CellListToString(SelectedCells));
            SelectedString.text=CellListToString(SelectedCells);
            return true;
        }
        else
            return false;
    }
}
