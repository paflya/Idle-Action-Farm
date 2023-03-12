using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnLogic : MonoBehaviour
{
    public Transform player;
    public int Wheat;

    public float Timeout;
    private float currentTime;
     public void WheatCollected(DataPair pair)
    {
        player = pair.sendTransform;
        Wheat += pair.sendValue;
        player.GetComponent<PlayerInputHandler>().WheatCollected(-Wheat);
    }
    private void Update()
    {
        if (Wheat == 0) return;
        if (currentTime < 0) 
        {
            currentTime = Timeout;
            player.SendMessage("SellWheat", new DataPair(transform, Wheat));
            Wheat = 0;
        }
        else currentTime -= Time.deltaTime;
        
    }
}
