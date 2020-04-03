using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CellBehavior : MonoBehaviour {
    public bool selected;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public RandomNumberGenerator randGen;
    public char currentLetter;
    public GameObject GameManager;
    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        currentLetter=' ';
        spriteRenderer.sprite=spriteArray[26];
    }
    void Update() {
        
    }
    public void RandLetter() {
        randGen=RandomNumberGenerator.Create();
        byte[] rndArray = new byte[4];
        randGen.GetBytes(rndArray);
        int letterNum = Math.Abs(BitConverter.ToInt32(rndArray,0)%26);
        currentLetter=(char)(letterNum+65);
        spriteRenderer.sprite = spriteArray[letterNum];
    }
    public void Select() {
        transform.localScale=new Vector3(0.95f,0.95f,0.95f);
        selected=true;
    }
    public void Deselect() {
        transform.localScale=new Vector3(1,1,1);
        selected=false;
    }
    public bool IsSelected() {
        return selected;
    }
    public char GetChar() {
        return currentLetter;
    }
}
