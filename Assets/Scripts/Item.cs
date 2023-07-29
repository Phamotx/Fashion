using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None, Red, Green, Yellow
}
public class Item : MonoBehaviour
{
    private ItemType type;
    private Material material;
    public ItemType Type { get { return type; } }
    private void OnEnable()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
    }
    public void Init(ItemType type)
    {
        this.type = type;
        material.color = GameManager.instance.GetColor(type);
    }
}
