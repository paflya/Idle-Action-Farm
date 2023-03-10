using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //Player stat params
    public int coinAmount = 0;
    public int maxCropsAmount = 40;
    public int currentCropsAmount = 0;

    void WheatCollected(int amount)
    { 
        currentCropsAmount += amount;
    }

}
