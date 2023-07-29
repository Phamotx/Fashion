using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Item itemObject;
    private List<Item> itemList = new List<Item>();

    public void AddItem(ItemType type)
    {
        int index = itemList.Count > 0 ? itemList.Count : 0;
        var item = Instantiate(itemObject, parent);
        item.transform.localPosition = Vector2.up * (index * 0.2f);
        item.Init(type);
        itemList.Add(item);
    }
    public void RemoveItem(ItemType type)
    {
        if (itemList.Count > 0)
        {
            var obj = itemList.FirstOrDefault(x => x.Type == type);
            if (obj != null)
            {
                itemList.Remove(obj);
                Destroy(obj.gameObject);
                SetItemPosition();
            }
        }
    }

    public bool ContainsItem(ItemType type)
    {
        if (itemList.Count > 0)
        {
            var obj = itemList.FirstOrDefault(x => x.Type == type);
            return (obj != null);
        }
        return false;
    }

    void SetItemPosition()
    {
        int index = 0;
        foreach (var item in itemList)
        {
            item.transform.localPosition = Vector2.up * (index * 0.2f);
            index++;
        }
    }

    public bool HasItem()
    {
        return itemList.Count > 0;
    }
}
