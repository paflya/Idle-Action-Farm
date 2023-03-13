using UnityEngine;

public class BarnLogic : MonoBehaviour
{
    [SerializeField] private InventoryBlock coins;

    public void WheatCollected(int wheat) => coins.UpdateAmount(transform.position, wheat);

}
