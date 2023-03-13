using UnityEngine;

public class PlayerInventoryHandler : MonoBehaviour
{
    [Header("Objects assigned in editor")]
    [SerializeField] private GameObject wheatBlock;
    [SerializeField] private GameObject backpack;

    [Header("Scriptable objects")]
    [SerializeField] private InteractionBehavior inventoryInfo;
    [SerializeField] private InventoryBlock crops;

    [Header("Variables on runtime")]
    [SerializeField] private bool isUnloading = false;
    [SerializeField] private bool isBackpackVisible =false;

    private void FixedUpdate()
    {
        GameObject closestBlock = null;
        GameObject closestBarn = null;

        if (crops.currentAmount > 0)
        {
            closestBarn = SearchObjectByLayer(64, inventoryInfo.maxUnloadRange);
            if (closestBarn != null)
            {
                isUnloading = true;
                ThrowWheatToABarn(closestBarn.transform);
            }
            backpack.SetActive(true);
        }
        else backpack.SetActive(false);

        if (crops.currentAmount < crops.maxAmount)
        {
            closestBlock = SearchObjectByLayer(128, inventoryInfo.maxPickUpRange);

            if (closestBlock != null && !isUnloading)
            {
                CollectWheat(closestBlock);
            }
        }
        isUnloading = false;

    }

    private GameObject SearchObjectByLayer(int layer, float range)
    {
        Collider[] Colliders = Physics.OverlapSphere(transform.position, range, layer);
        if (Colliders.Length > 0)
        {
            return GetClosestObject(Colliders, range);
        }
        return null;
    }

    private void CollectWheat(GameObject hayBlock) => hayBlock.SendMessage("JumpToPos",
        new JumpToPosParams { Duration = inventoryInfo.pickupDuration, Target= backpack.transform });
    public void WheatCollected(int amount) => crops.UpdateAmount(transform.position, amount);

    private void ThrowWheatToABarn(Transform barn)
    {
        GameObject block = Instantiate(wheatBlock, transform.position, transform.rotation);
        block.SendMessage("SetIgnore");
        block.SendMessage("JumpToPos", new JumpToPosParams { Duration = inventoryInfo.throwDuration, Target = barn });
        crops.UpdateAmount(transform.position, -1);
    }



    private GameObject GetClosestObject(Collider[] colliders, float scanRange)
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
}

public struct JumpToPosParams
{
    public Transform Target { get; set; }
    public float Duration { get; set; }
}
