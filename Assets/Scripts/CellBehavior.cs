using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CellBehavior : MonoBehaviour {
    public Dictionary<char,double> LetterFreq = new Dictionary<char,double> {
        {'A',0.09},{'B',0.02},{'C',0.02},{'D',0.04},{'E',0.12},{'F',0.02},{'G',0.03},{'H',0.02},{'I',0.09},{'J',0.01},{'K',0.02},{'L',0.04},{'M',0.02},
        {'N',0.06},{'O',0.08},{'P',0.02},{'Q',0.01},{'R',0.06},{'S',0.05},{'T',0.06},{'U',0.04},{'V',0.02},{'W',0.02},{'X',0.01},{'Y',0.02},{'Z',0.01}
    };
    public bool selected;
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public RandomNumberGenerator randGen;
    public char currentLetter;
    public Collider2D[] adjacent;
    void Start() {
        spriteRenderer=gameObject.GetComponent<SpriteRenderer>();
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
    public void RandLetterWeighted() {  //TODO change distribution to reflect common pairs of letters, common beginning/ending letters near edges, prevent too many dupes etc
        randGen=RandomNumberGenerator.Create();
        byte[] letterBytes = new byte[4];
        randGen.GetBytes(letterBytes);
        int letterNum = Math.Abs(BitConverter.ToInt32(letterBytes,0));
        double frac = (double)letterNum/2147483647;
        double cumProb = 0;
        foreach(var i in LetterFreq) {
            cumProb+=i.Value;
            Debug.Log(i.Value);
            Debug.Log(frac);
            if(frac<=cumProb) {
                currentLetter=i.Key;
                Debug.Log(i.Key);
                spriteRenderer.sprite=spriteArray[(int)i.Key-65];
                break;
            }
        }
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
