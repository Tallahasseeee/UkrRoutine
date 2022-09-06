using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Item item;
    
    public void Use()
    {
        switch (item.itemType)
        {
            default:
            case Item.ItemType.Flashlight: GameManager.instance.playerGameObject.GetComponent<Player>().PointLight.SetActive(!GameManager.instance.playerGameObject.GetComponent<Player>().PointLight.activeSelf);break;
        }

    }

    public void SpawnItemWorld()
    {
        if (item.itemType == Item.ItemType.Flashlight)
            GameManager.instance.playerGameObject.GetComponent<Player>().PointLight.SetActive(false);
        GameObject itemWorld = Instantiate(GameManager.instance.itemWorld, GameManager.instance.playerGameObject.transform.position, GameManager.instance.playerGameObject.transform.rotation);
        itemWorld.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
        itemWorld.GetComponent<Rigidbody2D>().AddForce(GameManager.instance.playerGameObject.GetComponent<Player>().dir*10, ForceMode2D.Impulse);
        GameManager.instance.uiInventory.inventory.RemoveItem(item);
        GameManager.instance.uiInventory.RefreshInventoryItems();

    }
}
