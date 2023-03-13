using UnityEngine;

[CreateAssetMenu(fileName = "New Harvestable Object Behavior", menuName = "Scriptable Objects/Harvestable Object Behavior")]
public class HarvestableObject : ScriptableObject
{
    [SerializeField] public GameObject replacementObject;
    [SerializeField] public GameObject pickupBlock;
}
