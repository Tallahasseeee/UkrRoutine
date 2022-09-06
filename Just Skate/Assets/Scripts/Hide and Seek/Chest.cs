using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collidable
{

    public GameObject takeButton;

    public GameObject itemGameObject;

    public Item.ItemType itemtype;

    public Item item = new Item ();

    protected override void Start()
    {
        base.Start();
        item.itemType = itemtype;
        item.amount = 1;
    }


    protected override void Update()
    {
        base.Update();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player"&& itemGameObject != null)
        {
            takeButton.SetActive(true);
            GameManager.instance.selectedItemGameObject = itemGameObject; 
        }
        
    }

    protected override void NonCollide()
    {
        takeButton.SetActive(false);
    }

}
