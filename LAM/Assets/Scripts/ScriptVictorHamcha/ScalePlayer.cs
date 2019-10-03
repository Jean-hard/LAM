using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlayer : MonoBehaviour
{
    private PlaneScript planescript;
    public float sm;
    public float sx;
    public float sp;
    public float _scale;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Scale(sm, sx, sp);
    }

    public void Scale (float smin, float smax, float sprop) // fonction de scale du perso : smin = à la taille minimal du perso, smax la taille maximal, sprop de combiel il augment/perd en avancant
    {
        _scale = sprop / gameObject.transform.position.y;// variable de la taille du personnage  qui se modifie par rapport à la position du personnage sur y

        if (_scale<=smin)   //si taille du perso inferieur à sa taille minimum alors sa taille est égal à sa taille minimum
            _scale = smin;
        
        if (_scale >= smax) //si taille du perso superieur à sa taille maximum alors sa taille est égal à sa taille maximum
            _scale = smax;


        gameObject.transform.localScale = new Vector2(_scale, _scale); // taille du personnage

    }
}
