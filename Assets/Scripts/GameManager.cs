using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Text SelectedString;
    public string SelectedLetters;
    // Start is called before the first frame update
    void Start()
    {
        SelectedString=GameObject.Find("Selected Display").GetComponent<Text>();
        SelectedString.text="";
    }

    // Update is called once per frame
    void Update()
    {

    }
    void ChangeDisplayText() {
        SelectedString.text=SelectedLetters;
    }
    public void AddToSelected(char toAdd) {
        SelectedLetters+=toAdd;
        ChangeDisplayText();
    }
    public void RemoveFromSelected() {
        SelectedLetters=SelectedLetters.Remove(SelectedLetters.Length-1);
        ChangeDisplayText();
    }
}
