using UnityEngine;

public class CutWheatLogic : MonoBehaviour
{

    [SerializeField] private HarvestedObject behavior;

    [SerializeField] private float currentTime = 0;

    void Update()
    {
        if (currentTime <= behavior.growTime)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            Instantiate(behavior.replacementObject, transform.position, transform.rotation, transform.parent);

            Destroy(gameObject);
        }
    }
}
