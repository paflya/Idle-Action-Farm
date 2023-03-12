using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEngine.GraphicsBuffer;

public class WheatBlockLogic : MonoBehaviour
{
    public GameObject sender;
    public GameObject target;
    public float duration;
    public int WheatAmount;
    public float timeOut;

    public void Call(moveInfo jumpInfo)
    {
        sender = jumpInfo.sender;
        target = jumpInfo.target;
        duration = jumpInfo.duration;

        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
    private void Update()
    {
        if (target == null) return;

        float distance = (sender != null) ? Vector3.Distance(sender.transform.position, target.transform.position) :
            Vector3.Distance(transform.position, target.transform.position);

        float speed = distance / duration;
        var step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" && sender == null) || other.tag == "Barn")
        {
            print("pass");
            if (other.tag == "Barn") other.SendMessage("WheatCollected", new DataPair(sender.transform, WheatAmount));
            else other.SendMessage("WheatCollected", WheatAmount);
            transform.DOKill();
            Destroy(gameObject);

        }

    }
}
            
public struct DataPair
{
    public Transform sendTransform;
    public int sendValue;
    public DataPair(Transform transform, int value)
    {
        this.sendTransform = transform;
        this.sendValue = value;
    }
}
