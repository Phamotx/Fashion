using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform Parent;
    [SerializeField] private Customer customer;
    [SerializeField] private int poolSize = 3;
    [SerializeField] private float delay;
    private Coroutine coroutine;
    private Customer[] customerPool;

    private void Start()
    {
        // Initialize the object pool
        customerPool = new Customer[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            customerPool[i] = Instantiate(customer, transform.position, Quaternion.identity, Parent);
            customerPool[i].gameObject.SetActive(false);
        }
    }

    // Method to pull an object from the pool
    public Customer PullCustomer()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!customerPool[i].gameObject.activeInHierarchy)
            {
                customerPool[i].transform.position = transform.position;
                customerPool[i].transform.rotation = Quaternion.identity;
                customerPool[i].gameObject.SetActive(true);
                return customerPool[i];
            }
        }

        // If all objects are in use, create a new one and add it to the pool
        Customer newObj = Instantiate(customer, transform.position, Quaternion.identity, Parent);
        customerPool = ResizeArray(customerPool, poolSize + 1);
        customerPool[poolSize] = newObj;
        poolSize++;

        return newObj;
    }

    // Helper method to resize the object pool array
    private Customer[] ResizeArray(Customer[] array, int newSize)
    {
        Customer[] newArray = new Customer[newSize];
        for (int i = 0; i < Mathf.Min(array.Length, newSize); i++)
        {
            newArray[i] = array[i];
        }
        return newArray;
    }
    public void StartSpawning()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Spawn());
        }
        else
        {
            StopCoroutine(coroutine);
            coroutine = null;
            coroutine = StartCoroutine(Spawn());
        }

    }

    IEnumerator Spawn()
    {
        while (!GameManager.instance.slotManager.SlotPositionsFull())
        {
            yield return new WaitForSeconds(delay);

            SpawnCustomer();
        }
    }

    void SpawnCustomer()
    {
        var spawnPosition = GameManager.instance.slotManager.GetSlotPositions();
        Customer custom = PullCustomer();
        custom.Init(spawnPosition, transform.position);
    }
}
