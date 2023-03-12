using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutWheatLogic : MonoBehaviour
{
    public GameObject grownWheat;

    public const float growTime = 10f;
    private float currentTime = 0;

    void Update()
    {
        if (currentTime <= growTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            Instantiate(grownWheat,this.transform.position,this.transform.rotation,this.transform.parent);
            Destroy(this.gameObject);
        }
    }

    
}
