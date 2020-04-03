using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClick : MonoBehaviour {
    public GameObject cell;
    public GameObject gm;
    public Collider2D[] touching;

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
        RaycastHit2D hit;

        if(Input.GetMouseButtonDown(0)) {
            hit=Physics2D.Raycast(worldPoint,Vector2.zero);
            if(hit.collider!=null&&hit.collider.name.Substring(0,9)=="Grid Cell") { //if raycast hit a cell
                //Debug.Log(hit.collider.name);
                cell=GameObject.Find(hit.collider.name);
                Collider2D[] touching=Physics2D.OverlapCircleAll(cell.transform.position,(float)0.7);
                touching=RemoveCollider(touching,hit.collider);
                for(int i=0;i<touching.Length;i++)
                    Debug.Log(touching[i].name);
                if(cell.GetComponent<CellBehavior>().IsSelected()) { //toggles selected state
                    cell.GetComponent<CellBehavior>().Deselect();
                    gm.GetComponent<GameManager>().RemoveFromSelected();
                }
                else if(!cell.GetComponent<CellBehavior>().IsSelected()) {
                    cell.GetComponent<CellBehavior>().Select();
                    gm.GetComponent<GameManager>().AddToSelected(cell.GetComponent<CellBehavior>().GetChar());
                }
            }
        }
        touching=default(Collider2D[]);
    }
}
