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
    public Collider2D[] adjacent;
    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        currentLetter=' ';
        spriteRenderer.sprite=spriteArray[26];
        adjacent=GetAdjacent();
    }
    void Update() {
        
    }
    public Collider2D[] RemoveCollider(Collider2D[] ColliderArray,Collider2D Remove) {
        Collider2D[] NewColliderArray = new Collider2D[ColliderArray.Length-1];
        int i = 0, j = 0, RemoveAt = 0;
        for(int q = 0;q<ColliderArray.Length;q++) {
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
    public Collider2D[] GetAdjacent() {
        Collider2D[] touching = Physics2D.OverlapCircleAll(transform.position,(float)0.7);
        touching=RemoveCollider(touching,GetComponent<Collider2D>());
        return touching;
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
