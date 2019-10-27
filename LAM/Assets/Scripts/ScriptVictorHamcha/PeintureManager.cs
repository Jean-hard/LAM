using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeintureManager : MonoBehaviour
{
    private float bonnepeintureOpacité1 = 0;//opacité de la première bonne peinture
    private float bonnepeintureOpacité2 = 0;//opacité de la deuxième bonne peinture
    private float bonnepeintureOpacité3 = 0;//opacité de la troisième bonne peinrue
    private float fondueopacite = 0;//opacite de la fondue
    public SpriteRenderer bonnepeinture1;//sprite de la première bonne peinture
    public SpriteRenderer bonnepeinture2;//sprite de la deuxième bonne peinture
    public SpriteRenderer bonnepeinture3;//sprite de la troidième bonne peinture
    public GameObject badPeinture; //la peinture si on a pas entré les bonnes couleurs
    public SpriteRenderer fondueSprite;//sprite de la fondue
    public bool bonnereponses1;//quand on mets la bonnepeinture1 
    public bool bonnereponses2;//quand on mets la bonne peinture 2
    public bool bonnereponses3;//quand on mets la bonne peinture 3
    public bool activefondue;
    private Color opacity1;//couleur ou on change l'opacité de la peinture 1
    private Color opacity2;//couleur ou change l'opacité de la peinture 2
    private Color opacity3;// couleur ou on change l'opacité de la peintue 3
    private Color fondueopacity;//couleur de la fondue
    private int nbrcouleurmise = 0;//compteur du nombre de couleur de la pallette utilisé 
    private int feedbackcouleur = 0;//couleur de la fondue noire(0) si peinture raté blanche(0) si peinture réussi

    public Transform palettedecouleur; //les bouton palette de couleur

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bonnereponses1 && bonnereponses2 && bonnereponses3)// si on arrive à mettre les trois couleur alors gagné
        {


            feedbackcouleur = 1;//mets la couleur de la fondue en blanc
            Debug.Log("youwin");
            foreach (Transform paletteChild in palettedecouleur)
            {
                paletteChild.gameObject.GetComponent<Button>().interactable = false; //desactive le click des boutons de la pallette de couleur 
            }
        }
        else
        {
            feedbackcouleur = 0;//la couleur de la fondue est noire
        }

       
        if(activefondue)
        {
            if (fondueopacite < 1) //si l'opacité est inferieur à 1
                fondueopacite += 0.1f;//elle augmente de 0,1 par frame
            if (fondueopacite >= 1)//pour que cela ne dépasse pas 1
                fondueopacite = 1;
        }
        else
        {
            if (fondueopacite >= 0) //si l'opacité est supeireur ou egal à 1
                fondueopacite -= 0.1f;//elle diminue de 0,1 par frame
            if (fondueopacite <= 0)//pour que cela ne dépasse pas 0
                fondueopacite = 0;
        }

        fondueopacity = fondueSprite.color = new Color(feedbackcouleur, feedbackcouleur, feedbackcouleur, fondueopacite);//couleur et opacité de la fondue


        if (bonnereponses1 && nbrcouleurmise>=3)//si on mets la bonne peinture alors
        {
            
            if (bonnepeintureOpacité1 < 1) //si l'opacité est inferieur à 1
                bonnepeintureOpacité1 += 0.1f;//elle augmente de 0,1 par frame
            if (bonnepeintureOpacité1 >= 1)//pour que cela ne dépasse pas 1
                bonnepeintureOpacité1 = 1f;//
          
        }

        if (bonnereponses2 && nbrcouleurmise >= 3)//même chose que pour la bonne peinture 1 mais pour la 2
        {
            if (bonnepeintureOpacité2 < 1)
                bonnepeintureOpacité2 += 0.1f;
            if (bonnepeintureOpacité2 >= 1)
                bonnepeintureOpacité2 = 1f;
        }
        if (bonnereponses3 && nbrcouleurmise >= 3)//même chose que pour la bonne peinture 1 mais pour la 3
        {
            if (bonnepeintureOpacité3 < 1)
                bonnepeintureOpacité3 += 0.1f;

            if (bonnepeintureOpacité3 >= 1)
                bonnepeintureOpacité3 = 1;
        }


        opacity1 = bonnepeinture1.color = new Color(1, 1, 1, bonnepeintureOpacité1);//dis que la valeur de l'opacité est égal à la float bonneopacité1
        opacity2 = bonnepeinture2.color = new Color(1, 1, 1, bonnepeintureOpacité2);//
        opacity3 = bonnepeinture3.color = new Color(1, 1, 1, bonnepeintureOpacité3);//


        
        
    }




    IEnumerator Fondue()
    {
        yield return new WaitForSeconds(2f);
        activefondue = true;//active la fondu de transparent vers le blanc
        yield return new WaitForSeconds(0.5f);
        activefondue = false;//active la fondu du blanc vers le transparent
        
    }

    public void ActiveBonnepeinture1()//fonction du bouton de la première bonne peinture
    {
        nbrcouleurmise += 1;//rajoute 1 au compteur de couleur mise
        if ( nbrcouleurmise>=3)// si on clique sur trois couleur de la palette :
        {

            StartCoroutine(Fondue());//start la fondue


        }

        bonnereponses1 = true;//dis que l'une des bonne couleur a été mise

        if ((bonnereponses3 && !bonnereponses2 || bonnereponses2 && !bonnereponses3 || bonnereponses1 && !bonnereponses3 && !bonnereponses2) && nbrcouleurmise>=3)// vérifie si quand on clique sur une des bonnes couleur au moment de la troisième couleur si au moins l'une d'elle est fausse
        {
            badPeinture.SetActive(true);
            bonnereponses1 = false;//arrete d'augmenter l'opacité
            bonnereponses2 = false;//arrete d'augmenter l'opacité
            bonnereponses3 = false;//arrete d'augmenter l'opacité
            bonnepeintureOpacité1 = 0;//mets l'opacité des bonnes peinture active à 0
            bonnepeintureOpacité2 = 0;//
            bonnepeintureOpacité3 = 0;//
            nbrcouleurmise = 0;//remets le compteur de couleur mise a 0
        }
        
    }

    public void ActiveBonnepeinture2()//fonction du bouton de la deuxième bonne peinture
    {
        nbrcouleurmise += 1;//rajoute 1 au compteur de couleur mise
        if (nbrcouleurmise>=3)// si on arrive à mettre les deux autres couleur bonne alors :
        {

            StartCoroutine(Fondue());


        }

        bonnereponses2 = true;//dis que l'une des bonne couleur a été mise

        if ((bonnereponses1 && !bonnereponses3 || bonnereponses3 && !bonnereponses1 || bonnereponses2 && !bonnereponses1 && !bonnereponses3) && nbrcouleurmise >= 3)// vérifie si quand on clique sur une des bonnes couleur au moment de la troisième couleur si au moins l'une d'elle est fausse
        {
            badPeinture.SetActive(true);
            bonnereponses1 = false;//arrete d'augmenter l'opacité
            bonnereponses2 = false;//arrete d'augmenter l'opacité
            bonnereponses3 = false;//arrete d'augmenter l'opacité
            bonnepeintureOpacité1 = 0;//mets l'opacité des bonnes peinture active à 0
            bonnepeintureOpacité2 = 0;//
            bonnepeintureOpacité3 = 0;//
            nbrcouleurmise = 0;//remets le compteur de couleur mise a 0
        }
        
    }

    public void ActiveBonnepeinture3()//fonction du bouton de la troisième bonne peinture
    {
        nbrcouleurmise += 1;//rajoute 1 au compteur de couleur mise
        if (nbrcouleurmise>=3)// si on arrive à mettre les deux autres couleur bonne alors :
        {
            StartCoroutine(Fondue());
        }

        bonnereponses3 = true; //dis que l'une des bonne couleur a été mise

        if ((bonnereponses1&&!bonnereponses2 || bonnereponses2&&!bonnereponses1|| bonnereponses3&& !bonnereponses1 && !bonnereponses2) && nbrcouleurmise >= 3)// vérifie si quand on clique sur une des bonnes couleur au moment de la troisième couleur si au moins l'une d'elle est fausse
        {
            badPeinture.SetActive(true);
            bonnereponses1 = false;//arrete d'augmenter l'opacité
            bonnereponses2 = false;//arrete d'augmenter l'opacité
            bonnereponses3 = false;//arrete d'augmenter l'opacité
            bonnepeintureOpacité1 = 0;//mets l'opacité des bonnes peinture active à 0
            bonnepeintureOpacité2 = 0;//
            bonnepeintureOpacité3 = 0;//
            nbrcouleurmise = 0;//remets le compteur de couleur mise a 0
        }

        
        
    }

    public void ActiveMauvaisepeinture()// fonctions des boutons de mauvaise peinture
    {
        nbrcouleurmise += 1;//rajoute 1 au compteur de couleur mise
        if (nbrcouleurmise>=3)
        {
            StartCoroutine(Fondue());
            bonnereponses1 = false;//arrete d'augmenter l'opacité
            bonnereponses2 = false;//arrete d'augmenter l'opacité
            bonnereponses3 = false;//arrete d'augmenter l'opacité
            bonnepeintureOpacité1 = 0;//mets l'opacité des bonnes peinture active à 0
            bonnepeintureOpacité2 = 0;//
            bonnepeintureOpacité3 = 0;//
            nbrcouleurmise = 0;//remets le compteur de couleur mise a 0
        }
       
    }
    

}
