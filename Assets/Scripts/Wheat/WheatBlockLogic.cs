using DG.Tweening;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class WheatBlockLogic : MonoBehaviour
{
    [Header("Variables from scriptable object on Start")]
    [SerializeField] private BlockInfo blockInfo;

    [Header("Variables on runtime")]
    [SerializeField] private bool isActive;
    [SerializeField] private float currentTime;
    [SerializeField] private bool ignorePlayer;

    public void JumpToPos(JumpToPosParams jumpParams)
    {
        isActive = true;
        Sequence jumpSequence= transform.DOJump(jumpParams.Target.position,2f,1,jumpParams.Duration);
        Tween tween= transform.DOMove(jumpParams.Target.position, jumpParams.Duration);
        jumpSequence.onComplete += () => tween.Play();
        jumpSequence.Play();

        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }
    private void Update()
    {
        if (!isActive)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= blockInfo.timeOut) { Destroy(gameObject); return; }
            else return;
        }
    }

    public void SetIgnore() => ignorePlayer = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !ignorePlayer)
        {
            other.SendMessage("WheatCollected", 1);
            transform.DOKill();
            Destroy(gameObject);
        }
         if   (other.tag == "Barn")
         {
            other.SendMessage("WheatCollected", blockInfo.blockCost);
            transform.DOKill();
            Destroy(gameObject);
        }

    }
}
