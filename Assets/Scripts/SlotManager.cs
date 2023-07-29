using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public List<Slots> slotList;
    public Vector3 GetSlotPositions()
    {
        int index = 0;
        foreach (var slot in slotList)
        {
            if (!slot.HasCustomerOnSlot)
            {
                slotList[index].HasCustomerOnSlot = true;
                break;
            }
            index++;
        }

        return slotList[index].transform.position;
    }

    public bool SlotPositionsFull()
    {
        foreach (var slot in slotList)
        {
            if (!slot.HasCustomerOnSlot)
            {
                return false;
            }
        }
        return true;
    }
}
