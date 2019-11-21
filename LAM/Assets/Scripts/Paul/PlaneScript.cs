using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    [SerializeField]
    private Vector2 initPlayerPos;

    [SerializeField]
    private GameObject[] interElementTab;
    private GameObject actualPlane;

    /**
     * On set un dialogue qui sera lancé la première fois qu'on entre dans un plan spécifique.
     */
    [SerializeField]
    private Dialogue initialDialogue = null;
    private bool dialogueUsed = false;

    /**
     * ----TODO : à commenter
     */
    public float minscale;
    public float maxscale;
    public float propscale;

    [SerializeField]
    private GameObject[] planeLightTab;

    // Start is called before the first frame update
    void Start()
    {
        actualPlane = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Quand on désactive ce plan, on désactive tout les éléments interactifs qui y sont lié
     * et on désactive toutes les lumières
     */
    public void OnDesactive()
    {
        this.gameObject.SetActive(false);
        for (int i = 0; i < interElementTab.Length; i++)
            interElementTab[i].SetActive(false);

        for (int i = 0; i < planeLightTab.Length; i++)
            planeLightTab[i].SetActive(false);
    }

    /**
     * Quand on active ce plan, on réactive tout.
     */
    public void OnActive()
    {
        this.gameObject.SetActive(true);
        for (int i = 0; i < interElementTab.Length; i++)
            interElementTab[i].SetActive(true);

        for (int i = 0; i < planeLightTab.Length; i++)
            planeLightTab[i].SetActive(true);
    }

    public Vector3 GetInitPlayerPos()
    {
        return initPlayerPos;
    }

    /**
     * Si il y a un dialogue et qu'il n'a jamais été lancé,
     * on retourne le dialogue en question.
     */
    public Dialogue GetInitialDialogue()
    {
        if (initialDialogue != null && dialogueUsed == false)
        {
            dialogueUsed = true;
            return initialDialogue;
        }
        else
            return null;
    }
}
