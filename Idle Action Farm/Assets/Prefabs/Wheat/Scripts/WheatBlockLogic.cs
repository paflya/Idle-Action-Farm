using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WheatBlockLogic : MonoBehaviour
{
    public bool isMoving = false;
    public Transform target;
    public float flySpeed;
    public int WheatAmount;
    

    void Update()
    {
        if (!isMoving) return;
        var step = flySpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position+Vector3.up, step);
    }

    public void CallToPlayer(Transform player)
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        target = player;
        isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.SendMessage("WheatCollected",WheatAmount);
            Destroy(gameObject);
        }
    }

}
