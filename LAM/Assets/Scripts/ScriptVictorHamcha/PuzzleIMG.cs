using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleIMG : MonoBehaviour
{
    public List<GameObject> cut = new List<GameObject>();// liste sprite mask qui coupe l'objet
    public List<GameObject> img = new List<GameObject>();// liste image coupé
    public List<GameObject> bloc = new List<GameObject>();// list des pièce entière
    public GameObject puzzlegame; // jeux de puzzle entier
    public GameObject btnplay;// bouton pour jouer au puzzle
    public GameObject btnplay2;// bouton pour jouer au puzzle
    private int slice = 0;
    float x = -11.71f;
    float y = 3.972f;
    public static int v;

    // Start is called before the first frame update
    void Start()
    {
        puzzlegame.SetActive(false);//le jeux est désactivé en début de partie
        Slice();//coupe les différentes pièce en début de partie
    }

    public void Slice()
    {
        for (int i = 0; i < 16; i++)
        {


            if (slice < 4)// vérifie si il y a moins de 4 pièce sur une ligne 
            {
                x += 4.68f;// va à l'enplacement suivant 
                slice++;//dit qu'il y a une pièce en plus sur la ligne

            }
            else// si il y déja 4 pièce dans une ligne 
            {
                y -= 2.602f;// passe a la ligne suivante
                slice = 1;//dit qu'il y a une pièce sur la ligne
                x = -7.01f;//position de la pièce revient à la première de la ligne
            }

            cut[i].transform.parent = img[i].transform;//le sprite mask devient enfant de l'image
            cut[i].transform.position = new Vector2(x, y);//le sprite mask prend la pllace de la pièce qu'il doit créé
            bloc[i].transform.position = new Vector2(x, y);// la pièce aussi
            img[i].transform.parent = bloc[i].transform;// la pèce de vient parent de l'image coupé par le sprite mask
            bloc[i].transform.position = new Vector2(7.009999f, 3.972f);//positiones toutes les pièce au même endroit 
            bloc[i].transform.localScale = new Vector2(0.7165048f, 0.7165048f);// les pièce prenne une nouvelle taille


        }

    }

    public void Play()
    {
        puzzlegame.SetActive(true);//active le jeu
        btnplay.SetActive(false);// désactive le bouton de jeu
        btnplay2.SetActive(false);// désactive le bouton de jeu
    }

}
