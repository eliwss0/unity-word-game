using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClick : MonoBehaviour {
    public GameObject cell;
    public GameObject gm;
    public Collider2D[] touching;
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start() {
        gm=GameObject.Find("GameManager");
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
    //TODO HasElement function

    // Update is called once per frame
    void Update() {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0)) {
            hit=Physics2D.Raycast(worldPoint,Vector2.zero);
            ToggleSelected(hit);
        }
    }
    public void ToggleSelected(RaycastHit2D cellHit) {
        if(cellHit.collider!=null&&cellHit.collider.name.Substring(0,9)=="Grid Cell") { //if raycast hit a cell
            cell=GameObject.Find(cellHit.collider.name);
            if(cell.GetComponent<CellBehavior>().IsSelected()) { //toggles selected state
                cell.GetComponent<CellBehavior>().Deselect();
                gm.GetComponent<GameManager>().RemoveFromSelected();
            }
            else if(!cell.GetComponent<CellBehavior>().IsSelected()) {
                cell.GetComponent<CellBehavior>().Select();
                gm.GetComponent<GameManager>().AddToSelected(cell.GetComponent<CellBehavior>().GetChar());
                gm.GetComponent<GameManager>().AddCellToSelected(cell.name,cell.GetComponent<CellBehavior>().GetChar(),cell.GetComponent<CellBehavior>().GetAdjacent());
            }
        }
    }
}
