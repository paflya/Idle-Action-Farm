using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatLogic : MonoBehaviour
{    
    public GameObject cutWheat;
    public GameObject wheatBlock;

    public void ChopCrops()
    {
        Instantiate(cutWheat,this.transform.position,this.transform.rotation,this.transform.parent);
        Instantiate(wheatBlock,this.transform.position, this.transform.rotation);

        Destroy(this.gameObject);

    }

    



}
