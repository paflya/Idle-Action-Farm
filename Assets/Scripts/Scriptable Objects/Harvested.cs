using UnityEngine;

[CreateAssetMenu(fileName = "New Harvested Object Behavior", menuName = "Scriptable Objects/Harvested Object Behavior")]
public class HarvestedObject : ScriptableObject
{
    [SerializeField] public GameObject replacementObject;
    [SerializeField] public float growTime;
}
