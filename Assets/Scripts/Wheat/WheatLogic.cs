using UnityEngine;

public class WheatLogic : MonoBehaviour
{
    [SerializeField] private HarvestableObject behavior;


    public void ChopCrops()
    {
        Instantiate(behavior.replacementObject, transform.position, transform.rotation, transform.parent);
        Instantiate(behavior.pickupBlock, transform.position, transform.rotation);
        transform.GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }
}
