using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    int rangMask = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // pour activer le système d'inventaire, ajouter cette fonction au OnClick du bouton
    public void AddMaskToList()
    {
        GameObject mask = EventSystem.current.currentSelectedGameObject;
        GameObject newMask = Instantiate(mask, new Vector2(150f * rangMask++ + 100f, 950f), Quaternion.identity); //on crée une copie du masque cliqué qu'on place en haut à gauche de l'écran
        newMask.transform.localScale = 1.2f * newMask.transform.localScale;
        newMask.SetActive(true);
        // Destroy(newMask.GetComponent<Button>()); // enlève le clic sur le masque
        foreach (Transform child in newMask.transform) // enlève le texte "Button"
        {
            Destroy(child.gameObject);
        }
        newMask.transform.SetParent(GameObject.Find("Canvas").transform); // met le masque dans le canvas
    }

    // public void AddItem(GameObject item)
    // {
    //     Debug.Log("l'item " + item + " est ajouté à l'inventaire");
    //     itemList[0] = item;
    // }

    // public bool IsInInventory(GameObject item)
    // { // check si l'item donné en paramètre est déjà dans l'inventaire ou non
    //     int i = 0;
    //     bool found = false;
    //     while (!found && i < this.itemList.Length)
    //     {
    //         found = GameObject.ReferenceEquals(item, this.itemList[i]);
    //         i++;
    //     }
    //     return found;
    // }
}