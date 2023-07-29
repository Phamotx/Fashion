using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    [SerializeField] private ItemType orderType;
    [SerializeField] private bool hasCustomerOnSlot;
    [SerializeField] private Customer customer;
    public bool HasCustomerOnSlot { get { return hasCustomerOnSlot; } set { hasCustomerOnSlot = value; } }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if ((hasCustomerOnSlot) && (customer != null) && (GameManager.instance.ContainsItem(orderType)))
            {
                GameManager.instance.RemoveItem(orderType);
                hasCustomerOnSlot = false;
                customer.ReturnToBase();
                orderType = ItemType.None;
                customer = null;
            }
        }
        if (collider.tag == "Customer")
        {
            if (orderType == ItemType.None)
            {
                customer = collider.transform.GetComponent<Customer>();
                orderType = customer.Type;
            }
        }
    }
}
