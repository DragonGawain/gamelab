using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomDoorSlot : MonoBehaviour
{
    private bool isFilled;
    private int numLeft;
    private int health = 100;

    [System.Serializable]
    public struct BDSlot
    {
        public bool isFilled;
        public Transform bdTransform;
    }

    [SerializeField] private BDSlot[] bedroomDoorSlots;
    [SerializeField] private GameObject doorVisual;
    public GameObject DoorVisual
    {
        get { return doorVisual; }
    }

    public BDSlot GetOneBedroomDoorSlot()
    {
        for(int i = 0; i < bedroomDoorSlots.Length; i++)
        {
            if (bedroomDoorSlots[i].isFilled == false)
            {
                bedroomDoorSlots[i].isFilled = true;
                return bedroomDoorSlots[i];
            }
        }
        return default;
        
    }

    public bool GetDamage(int amount)
    {
        // returns true if the door is destroyed
        if(health - amount <= 0)
        {
            doorVisual.SetActive(false);
            return true;
        }
        else
        {
            health -= amount;
            return false;
        }
    }
}
