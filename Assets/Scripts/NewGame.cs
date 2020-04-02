using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartNewGame() {
        GameObject[] cells=GetCells();
        for(int i = 0;i<cells.Length;i++) {
            cells[i].GetComponent<CellBehavior>().RandLetter();
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
