using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BureauManager : MonoBehaviour
{
    public GameObject[] documentsTab = new GameObject[5];  // Tableau de documents que l'on peut parcourir
    public GameObject documents;    // GameObject ayant en enfant les documents
    private int currentDocIndex;    // Index du document en cours de lecture
    private bool isOnFirstDoc;
    // boutons document précédent ou suivant
    public GameObject nextDocButton;
    public GameObject previousDocButton;

    void Update()
    {
        if (currentDocIndex == 0 && !isOnFirstDoc)
        {
            previousDocButton.SetActive(false);
            isOnFirstDoc = true;
        }
    }
    // On ouvre la GUI des documents
    public void ShowDocuments()
    {
        documents.SetActive(true);
    }

    // Lorsqu'on clique sur le bouton 'suivant'
    public void ShowNextDocument()
    {
        if(currentDocIndex < documentsTab.Length - 1)   //si ce n'est pas le dernier document
        {
            previousDocButton.SetActive(true);  // on laisse le bouton previous affiché
            documentsTab[currentDocIndex].SetActive(false); // on désaffiche le document actuel
            currentDocIndex++;
            documentsTab[currentDocIndex].SetActive(true);  // on affiche le document suivant
        }
        if (currentDocIndex == documentsTab.Length - 1) // si c'est le dernier document
        {
            nextDocButton.SetActive(false); // on désaffiche le bouton next
        }
    }

    // Lorsqu'on clique sur le bouton 'précédent'
    public void ShowPreviousDocument()
    {
        if (currentDocIndex > 0)    //si ce n'est pas le premier document
        {
            nextDocButton.SetActive(true);  // on laisse le bouton next affiché
            documentsTab[currentDocIndex].SetActive(false); // on désaffiche le document actuel
            currentDocIndex--;
            documentsTab[currentDocIndex].SetActive(true);  // on affiche le document précédent
        }
        if (currentDocIndex == 0)   // si c'est le premier document
        {
            previousDocButton.SetActive(false); // on désaffiche le bouton previous
        }
    }
}
