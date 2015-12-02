using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
    private bool flashlight;         // Array location 0
    private bool knife;              // Array location 1
    private bool keyBasement;        // Array location 2

    private ModalPanel modalPanel;

    ArrayList inventory;
    string inventoryString;

    // Use this for initialization
    void Awake()
    {
        modalPanel = ModalPanel.Instance();
    }

    // Use this for initialization
    void Start () {
        flashlight = false;
        knife = false;
        keyBasement = false;
        inventory = new ArrayList();
    }
	
	// Update is called once per frame
	void Update () {

        // Check your inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryString = "";
            if(inventory.Count == 0)
            {
                modalPanel.Choice("I don't have anything on me.", 2f, 1f);
            }
            else
            {
                inventoryString = "I have ";
                for(int i = 0; i < inventory.Count; i++)
                {
                    // Next to last item
                    if(i == (inventory.Count-2) )
                    {
                        inventoryString += "a " + inventory[i] + " and ";
                    }
                    // Last item
                    else if (i == (inventory.Count-1))
                    {
                            inventoryString += "a " + inventory[i] + ".";
                    }
                    // All other items
                    else
                    {
                        inventoryString += "a " + inventory[i] + ", ";
                    }
                }
                modalPanel.Choice(inventoryString, 2f, 1f);
            }

        }

    }


    public void pickupFlashlight()
    {
        flashlight = true;
        inventory.Add("flashlight");
    }

    public bool hasFlashlight()
    {
        return flashlight;
    }

    public void pickupKnife()
    {
        knife = true;
        inventory.Add("knife");
    }

    public bool hasKnife()
    {
        return knife;
    }

    public void pickupKeyBasement()
    {
        keyBasement = true;
        inventory.Add("key");

    }
    public bool hasKeyBasement()
    {
        return keyBasement;
    }
}
