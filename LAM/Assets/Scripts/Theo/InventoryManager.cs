using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{

    public const int nbrItemMax = 10;
    public GameObject[] itemList = new GameObject[nbrItemMax];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(GameObject item)
    {
        Debug.Log("l'item " + item + " est ajouté à l'inventaire");
        itemList[0] = item;
    }

    public bool IsInInventory(GameObject item)
    { // check si l'item donné en paramètre est déjà dans l'inventaire ou non
        int i = 0;
        bool found = false;
        while (!found && i < this.itemList.Length)
        {
            found = GameObject.ReferenceEquals(item, this.itemList[i]);
            i++;
        }
        return found;
    }
}