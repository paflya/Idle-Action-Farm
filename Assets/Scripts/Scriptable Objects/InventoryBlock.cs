using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Inventory Block Info", menuName = "Scriptable Objects/Inventory Block Info")]
public class InventoryBlock : ScriptableObject
{
    [SerializeField] public int currentAmount;

    [SerializeField] public bool isCapped = false;

    [SerializeField] public int maxAmount;

    [System.NonSerialized]
    public UnityEvent<Vector3, int> amountUpdateEvent;

    private void OnEnable()
    {
        currentAmount = 0;
        amountUpdateEvent ??= new UnityEvent<Vector3, int>();
    }

    public void UpdateAmount(Vector3 position, int Amount)
    {
        currentAmount += Amount;
        amountUpdateEvent.Invoke(position, currentAmount);
    }

}
