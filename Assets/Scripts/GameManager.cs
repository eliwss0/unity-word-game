using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public class Cell {
        public string name;
        public char letter;
        public Collider2D[] adjacent;
    };
    public Dictionary<char,int> LetterValues = new Dictionary<char,int> {
        {'A',1},{'B',3},{'C',3},{'D',2},{'E',1},{'F',4},{'G',2},{'H',4},{'I',1},{'J',8},{'K',5},{'L',1},{'M',3},
        {'N',1},{'O',1},{'P',3},{'Q',10},{'R',1},{'S',1},{'T',1},{'U',1},{'V',4},{'W',4},{'X',8},{'Y',4},{'Z',10}
    };
    public string[] FoundWords;
    public Text SelectedString;
    public Text ScoreText;
    public int Score;
    public List<Cell> SelectedCells;
    public TextAsset Dictionary;
    public string[] words;
    // Start is called before the first frame update
    void Start() {
        List<Cell> SelectedCells = new List<Cell>();
        SelectedString.text="";
        words=Dictionary.text.Split('\n');
        Score=0;
        ScoreText.text="0";
        FoundWords;
    }
    // Update is called once per frame
    void Update() {

    }
    public int ScoreWord() {
        int finalScore = 0;
        for(int i = 0;i<SelectedString.text.Length;i++) {
            finalScore+=LetterValues[SelectedString.text[i]];
        }
        return finalScore;
    }
    public bool CheckWord() {   //checks if selected string is a word
        if(Array.Exists<string>(words,element => element==SelectedString.text)&&SelectedString.text.Length>=3) {    //Reads from UI text instead of game data. Change?
            Score+=ScoreWord();
            ScoreText.text=Score.ToString();
            return true;
        }
        else
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
    public bool AddCellToSelected(string cellName,char cellLetter,Collider2D[] cellAdjacent) {
        Cell toAdd = new Cell() { name=cellName,letter=cellLetter,adjacent=cellAdjacent };
        if(SelectedCells==null)
            SelectedCells = new List<Cell>();
        if((SelectedCells.Count>0&&CheckAdjacent(SelectedCells[SelectedCells.Count-1],toAdd))||SelectedCells.Count==0) {
            SelectedCells.Add(toAdd);
            SelectedString.text=CellListToString(SelectedCells);
            return true;
        }
        else
            return false;
    }
    public bool RemoveCellFromSelected(string cellName,char cellLetter,Collider2D[] cellAdjacent) {
        Cell toRemove = new Cell() { name=cellName,letter=cellLetter,adjacent=cellAdjacent };
        if((SelectedCells[SelectedCells.Count-1].name==toRemove.name)||CheckContains(SelectedCells,cellName)) { //TODO limit deselection to last element
            SelectedCells.RemoveAt(FindListIndex(cellName,SelectedCells));
            SelectedString.text=CellListToString(SelectedCells);
            return true;
        }
        else
            return false;
    }
    public void SaveGameManager() {
        GameManager.Instance.Score=Score;
    }
    void Awake() {
        if(Instance==null) {
            //DontDestroyOnLoad(gameObject);
            Instance=this;
        }
        else if(Instance!=this) {
            Destroy(gameObject);
        }
    }
}
