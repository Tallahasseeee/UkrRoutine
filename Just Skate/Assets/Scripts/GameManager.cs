using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerGameObject;

    public GameObject inventoryGameObject;

    public GameObject selectedItemGameObject;
    public GameObject itemWorld;
    public Rigidbody2D itemWorldRB;
    public GameObject takeButton;

    public Inventory inventory;

    public GameObject gameOverPanel;

    [SerializeField] public UI_Inventory uiInventory;
    private void Start()
    {
        instance = this;
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        itemWorldRB = itemWorld.GetComponent<Rigidbody2D>();
    }

    public void turnOnInventory()
    {
        inventoryGameObject.SetActive(true);
    }

    public void turnOffInventory()
    {
        inventoryGameObject.SetActive(false);
    }

    public void TakeItem()
    {
        GameManager.instance.inventory.AddItem(selectedItemGameObject.GetComponent<ItemWorld>().item);
        selectedItemGameObject.GetComponentInParent<Collider2D>().gameObject.SetActive(false);
        Destroy(selectedItemGameObject);
        GameManager.instance.uiInventory.SetInventory(GameManager.instance.inventory);
        takeButton.SetActive(false);
    }
}
