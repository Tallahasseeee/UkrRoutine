using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    public Inventory inventory;
    [SerializeField]private Transform inv;
    [SerializeField]private Transform cell;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        ClearUIinventory();
        int x = 0;
        int y = 0;
        float cellSize = 150f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform CellRectTransform = Instantiate(cell, inv).GetComponent<RectTransform>();
            CellRectTransform.gameObject.SetActive(true);
            CellRectTransform.anchoredPosition = new Vector2(x * cellSize, y * cellSize);
            Image image = CellRectTransform.Find("icon").GetComponent<Image>();
            image.sprite = item.GetSprite();
            CellRectTransform.GetComponent<Cell>().item = item;
            x++;
            if(x > 5)
            {
                x = 0;
                y--;
            }
        }
    }
    public void ClearUIinventory()
    {
        if(inv.Find("Cell(Clone)") != null)
            Destroy(inv.Find("Cell(Clone)").gameObject);

    }
}
