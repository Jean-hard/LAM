﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneManager : MonoBehaviour
{
    public List<int> numtel = new List<int>();//nuéro que le mec appel 
    public List<int> goodnumtel = new List<int>();//nuéro que le mec doit appelé
    public bool end;// action du jeu fini + une nouvelle action faite par le joueur alors ...(demandé par les GD
    public bool goodnumber; //quand tous les numéros mis par le joeur sont bon alors ...

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numtel.Count == 6 && numtel[0] == goodnumtel[0] && numtel[1] == goodnumtel[1] && numtel[2] == goodnumtel[2] && numtel[3] == goodnumtel[3] && numtel[4] == goodnumtel[4] && numtel[5] == goodnumtel[5])// si tout les numéro de l'appel son bon alors 
        {
            goodnumber = true;
            
        }
        if (end&&goodnumber)// si le jeu est finie et que le joueur a essayé une autre action alors 
        {
            Debug.Log("youwin");
        }
    }
}