using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClick : MonoBehaviour
{
    public GameObject cell;
    public GameObject gm;
    public Collider2D[] touching;

    // Start is called before the first frame update
    void Start()
    {
        gm=GameObject.Find("GameManager");
    }
    public Collider2D[] DeleteElement(Collider2D[] arr,Collider2D x) 
    { 
        int i; int n=arr.Length;
        for (i = 0; i < n; i++) 
            if (arr[i] == x) 
                break; 
        if (i < n) { 
            n = n - 1; 
            for (int j = i; j < n; j++) 
                arr[j] = arr[j+1]; 
        } 
        return arr; 
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
                Collider2D[] touching=Physics2D.OverlapCircleAll(cell.transform.position,(float)0.8);
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
    }
}
