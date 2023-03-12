using DG.Tweening;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Joystick joystick;
    public CharacterController controller;
    public Animator animator;
    public float playerSpeed = 10f;

    public float pickupDuration = 0.5f;

    public float throwDuration = 1.0f;

    public GameObject sickle;

    public float maxPickUpRange;
    public float maxUnloadRange;
    public float cutRange;

    public GameObject closestObject;
    public GameObject wheatBlock;

    public GameObject uiWheat;
    public GameObject uiCoins;
    

    //Player stat params
    public int coinAmount = 0;
    public int maxCropsAmount = 40;
    public int currentCropsAmount = 0;

    

    void Update()
    {
        Vector3 move = new Vector3(-joystick.Vertical, 0, joystick.Horizontal);
        controller.Move(playerSpeed * Time.deltaTime * move);
        controller.Move(Time.deltaTime * Vector3.up*Physics.gravity.y);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            animator.SetTrigger("isWalking");
            sickle.GetComponent<BoxCollider>().enabled = false;
            sickle.SetActive(false);
            animator.ResetTrigger("isAttacking");
        }
        else
        {
            animator.ResetTrigger("isWalking");
        }
    }

    private void FixedUpdate()
    {
        Collider[] InteractionColliders = Physics.OverlapSphere(transform.position, maxPickUpRange, 128);

        if (InteractionColliders.Length > 0)
        {
            if (!(currentCropsAmount < maxCropsAmount)) return;

            if (closestObject != null) return;
            closestObject = GetClosestObject(InteractionColliders,maxPickUpRange);

            if (closestObject != null)
            {
                CollectWheat(closestObject);
            }
           
        }
       
        if (controller.velocity.x !=0 && controller.velocity.z !=0)
        { 
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, cutRange, 8))
            {
                sickle.SetActive(true);
                animator.SetTrigger("isAttacking");
            }
        }

        
        if (currentCropsAmount <= 0) return;
        
        Collider[] BarnColliders = Physics.OverlapSphere(transform.position, maxUnloadRange, 64);
        if (BarnColliders.Length > 0)
        {
           closestObject = GetClosestObject(BarnColliders, maxUnloadRange);
            
            if (closestObject != null)
            {
                ThrowWheatToABarn(closestObject);
            }

        }

    }

    private void CollectWheat(GameObject hayBlock) => hayBlock.SendMessage("Call",new moveInfo(null,gameObject,pickupDuration));

    public void ThrowWheatToABarn(GameObject barn)
    {
        GameObject block = Instantiate(wheatBlock, transform.position, transform.rotation);
        block.SendMessage("Call", new moveInfo(gameObject, barn, throwDuration));

    }

    public void WheatCollected(int amount)
    {
        currentCropsAmount += amount;
        uiWheat.SendMessage("UpdateIndicator", new uiItemInfo(gameObject,currentCropsAmount,"/"+maxCropsAmount));
    }

    public GameObject GetClosestObject(Collider[] colliders, float scanRange)
    {
        float closest = scanRange;
        GameObject closestObject = null;
        for (int i = 0; i < colliders.Length; i++)  
        {
            float dist = Vector3.Distance(colliders[i].transform.position, this.transform.position);
            if (dist < closest)
            {
                closest = dist;
                closestObject = colliders[i].gameObject;
            }
        }
        return closestObject;
    }

    public void ToggleWeaponState(string isActive) => sickle.GetComponent<BoxCollider>().enabled = string.IsNullOrEmpty(isActive);  

}

public struct uiItemInfo
{
        public GameObject sender;
        public int value;
        public string postfix;

        public uiItemInfo(GameObject sender, int value, string postfix)
        {
                this.sender = sender;
                this.value = value;
                this.postfix = postfix;
        }
}
public struct moveInfo
{
    public GameObject sender;
    public GameObject target;
    public float duration;
    
    public moveInfo(GameObject sender, GameObject target, float duration)
    {
        this.sender= sender;
        this.target= target;
        this.duration = duration;
    }
}



