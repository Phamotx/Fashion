using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public Spawner spawner;
    public PlayerController player;
    public ItemCollector collector;
    public SlotManager slotManager;
    public Spawner spawner;
    [SerializeField] private List<Color> colorList;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CheckAndSpawn();
    }

    public Color GetColor(ItemType type)
    {
        return colorList[(int)type];
    }

    public void AddItem(ItemType type)
    {
        collector.AddItem(type);
    }
    public void RemoveItem(ItemType type)
    {
        collector.RemoveItem(type);
    }
    public bool ContainsItem(ItemType type)
    {
        return collector.ContainsItem(type);
    }

    public void CheckAndSpawn()
    {
        spawner.StartSpawning();
    }
}
