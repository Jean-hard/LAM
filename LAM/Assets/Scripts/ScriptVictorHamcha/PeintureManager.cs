using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeintureManager : MonoBehaviour
{
    private float bonnepeintureOpacité1 = 0;//opacité de la première bonne peinture
    private float bonnepeintureOpacité2 = 0;//opacité de la deuxième bonne peinture
    private float bonnepeintureOpacité3 = 0;//opacité de la troisième bonne peinrue
    public SpriteRenderer bonnepeinture1;//sprite de la première bonne peinture
    public SpriteRenderer bonnepeinture2;//sprite de la deuxième bonne peinture
    public SpriteRenderer bonnepeinture3;//sprite de la troidième bonne peinture
    public bool bonnereponses1;//quand on mets la bonnepeinture1 
    public bool bonnereponses2;//quand on mets la bonne peinture 2
    public bool bonnereponses3;//quand on mets la bonne peinture 3
    private Color opacity1;//couleur ou on change l'opacité de la peinture 1
    private Color opacity2;//couleur ou change l'opacité de la peinture 2
    private Color opacity3;// couleur ou on change l'opacité de la peintue 3

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

        if (bonnereponses1)//si on mets la bonne peinture alors
        {
            
            if (bonnepeintureOpacité1 < 1) //si l'opacité est inferieur à 1
                bonnepeintureOpacité1 += 0.1f;//elle augmente de 0,1 par frame
            if (bonnepeintureOpacité1 >= 1)//pour que cela ne dépasse pas 1
                bonnepeintureOpacité1 = 1f;//
          
        }

        if (bonnereponses2)//même chose que pour la bonne peinture 1 mais pour la 2
        {
            if (bonnepeintureOpacité2 < 1)
                bonnepeintureOpacité2 += 0.1f;
            if (bonnepeintureOpacité2 >= 1)
                bonnepeintureOpacité2 = 1f;
        }
        if (bonnereponses3)//même chose que pour la bonne peinture 1 mais pour la 3
        {
            if (bonnepeintureOpacité3 < 1)
                bonnepeintureOpacité3 += 0.1f;

            if (bonnepeintureOpacité3 >= 1)
                bonnepeintureOpacité3 = 1;
        }


        opacity1 = bonnepeinture1.color = new Color(1, 1, 1, bonnepeintureOpacité1);//dis que la valeur de l'opacité est égal à la float bonneopacité1
        opacity2 = bonnepeinture2.color = new Color(1, 1, 1, bonnepeintureOpacité2);//
        opacity3 = bonnepeinture3.color = new Color(1, 1, 1, bonnepeintureOpacité3);//


        if(bonnereponses1&&bonnereponses2&&bonnereponses3)// si on arrive à mettre les trois couleur alors gagné
        {
            Debug.Log("youwin");
        }
        
    }

    public void ActiveBonnepeinture1()//fonction du bouton de la première bonne peinture
    {
        bonnereponses1 = true;//permets de commencer à augmenter l'opacité
    }

    public void ActiveBonnepeinture2()//fonction du bouton de la deuxième bonne peinture
    {
        bonnereponses2=true;//permets de commencer à augmenter l'opacité
    }

    public void ActiveBonnepeinture3()//fonction du bouton de la troisième bonne peinture
    {
        bonnereponses3 = true; //permets de commencer à augmenter l'opacité
    }

    public void ActiveMauvaisepeinture()// fonctions des boutons de mauvaise peinture
    {
        bonnereponses1 = false;//arrete d'augmenter l'opacité
        bonnereponses2 = false;//arrete d'augmenter l'opacité
        bonnereponses3 = false;//arrete d'augmenter l'opacité
        bonnepeintureOpacité1 = 0;//mets l'opacité des bonnes peinture active à 0
        bonnepeintureOpacité2 = 0;//
        bonnepeintureOpacité3 = 0;//
    }
    

}
