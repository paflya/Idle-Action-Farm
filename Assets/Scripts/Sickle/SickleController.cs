using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==3)
        {
            other.gameObject.GetComponent<WheatLogic>().ChopCrops();
        }
    }
}
