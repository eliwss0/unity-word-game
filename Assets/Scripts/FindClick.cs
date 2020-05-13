using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class FindClick : MonoBehaviour {
    public GameObject cell;
    public GameObject gm;
    public Collider2D[] touching;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start() {
        gm=GameObject.Find("Selected Display");
    }
    public Collider2D[] RemoveCollider(Collider2D[] ColliderArray,Collider2D Remove) {
        Collider2D[] NewColliderArray = new Collider2D[ColliderArray.Length-1];
        int i=0, j=0, RemoveAt=0;
        for(int q=0;q<ColliderArray.Length;q++) {
            if(ColliderArray[q]==Remove)
                RemoveAt=q;
        }
        while(i<ColliderArray.Length) {
            if(i!=RemoveAt) {
                NewColliderArray[j]=ColliderArray[i];
                j++;
            }
            i++;
        }
        return NewColliderArray;
    }

    // Update is called once per frame
    void Update() {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0)) {
            hit=Physics2D.Raycast(worldPoint,Vector2.zero);
            ToggleSelected(hit);
        }
        else if(Input.GetMouseButtonDown(1)) {
            DeselectAllCells();
        }
    }
    public void DeselectAllCells() {
        GameObject[] SelectedCells = GameObject.FindGameObjectsWithTag("SelectedCell");
        foreach(GameObject cell in SelectedCells) {
            if(cell.GetComponent<CellBehavior>().IsSelected()) {
                cell.GetComponent<CellBehavior>().Deselect();
                cell.tag="Cell";
                gm.GetComponent<GameManager>().RemoveCellFromSelected(cell.name,cell.GetComponent<CellBehavior>().GetChar(),cell.GetComponent<CellBehavior>().GetAdjacent());
            }
        }
    }
    public void ToggleSelected(RaycastHit2D cellHit) {  //Should probably rework to avoid GetComponent
        if(cellHit.collider!=null&&cellHit.collider.name.Substring(0,9)=="Grid Cell") { //if raycast hit a cell
            cell=GameObject.Find(cellHit.collider.name);
            if(cell.GetComponent<CellBehavior>().IsSelected()) { //toggles selected state
                if(gm.GetComponent<GameManager>().CheckWord()) {  //if word matches in dict
                    //TODO score calculating, remove tiles
                    DeselectAllCells();
                    return;
                }
                if(gm.GetComponent<GameManager>().RemoveCellFromSelected(cell.name,cell.GetComponent<CellBehavior>().GetChar(),cell.GetComponent<CellBehavior>().GetAdjacent())) {
                    cell.GetComponent<CellBehavior>().Deselect();
                    cell.tag="Cell";
                }
            }
            else if(!cell.GetComponent<CellBehavior>().IsSelected()) {
                if(gm.GetComponent<GameManager>().AddCellToSelected(cell.name,cell.GetComponent<CellBehavior>().GetChar(),cell.GetComponent<CellBehavior>().GetAdjacent())) {
                    cell.GetComponent<CellBehavior>().Select();
                    cell.tag="SelectedCell";
                }
            }   //TODO replace IsSelected with cell tag?
        }
    }
}
