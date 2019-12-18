using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlayer : MonoBehaviour
{
    private PlaneScript planescript;

    public float _scale;
    public float speedScale;
    public float needScale;
    private PlayerManager playerManager;
    private float delaie = 0.8f;
    public bool canScale;
    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.targetPosition == playerManager.playerBasePose)
        {
            if (delaie > 0)
                delaie -= Time.deltaTime;
            else
            {
                delaie = 0;
                _scale = needScale;
            }
            
        }
        else
        {

            if (_scale > needScale)
            {
                _scale -= speedScale;
            }
            else
            {
                _scale = needScale;
            }
        }
        


        gameObject.transform.localScale = new Vector2(_scale, _scale); // taille du personnage
    }

    public void Scale(float smin, float smax, float sprop) // fonction de scale du perso : smin = à la taille minimal du perso, smax la taille maximal, sprop de combiel il augment/perd en avancant
    {
        _scale = sprop / gameObject.transform.position.y;// variable de la taille du personnage  qui se modifie par rapport à la position du personnage sur y

        if (_scale <= smin)   //si taille du perso inferieur à sa taille minimum alors sa taille est égal à sa taille minimum
            _scale = smin;

        if (_scale >= smax) //si taille du perso superieur à sa taille maximum alors sa taille est égal à sa taille maximum
            _scale = smax;




    }

    public void newScale(float newNeedScale)
    {
        delaie = 0.8f;
        if (canScale)
        needScale = newNeedScale;
    }

    public void newSpeedScale(float newSpeedScale)
    { 
        if (canScale)
        speedScale = newSpeedScale;
    }

    public void DirectScale (float scale)
    {
        delaie = 0.8f;
        needScale = scale;
        _scale = scale;
    }
}
