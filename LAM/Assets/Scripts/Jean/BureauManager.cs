using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BureauManager : MonoBehaviour
{
    public GameObject[] documentsTab = new GameObject[5];  // Tableau de documents que l'on peut parcourir
    public GameObject documents;    // GameObject ayant en enfant les documents
    private int currentDocIndex;    // Index du document en cours de lecture

    public GameObject docGUI;   // GameObject ayant en enfant les boutons lorsque les docs sont affichés
    public GameObject telButton;
    public GameObject backButton;
    public GameObject documentsButton;

    // boutons document précédent ou suivant
    public GameObject nextDocButton;
    public GameObject previousDocButton;
        
    // On ouvre la GUI des documents
    public void ShowDocuments()
    {
        documents.SetActive(true);
        telButton.SetActive(false);
        backButton.SetActive(false);
        documentsButton.SetActive(false);
        docGUI.SetActive(true);
    }

    // On retourne à la vue du bureau en désaffichant la GUI des documents
    public void HideDocuments()
    {
        documents.SetActive(false);
        telButton.SetActive(true);
        backButton.SetActive(true);
        documentsButton.SetActive(true);
        docGUI.SetActive(false);
    }

    // Lorsqu'on clique sur le bouton 'suivant'
    public void ShowNextDocument()
    {
        if(currentDocIndex < documentsTab.Length - 1)
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
        if (currentDocIndex > 0)
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
