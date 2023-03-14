using UnityEngine;


[CreateAssetMenu(fileName = "New Player Interaction Behavior", menuName = "Scriptable Objects/Interaction Behavior")]
public class InteractionBehavior : ScriptableObject
{
    [SerializeField] public float pickupDuration;

    [SerializeField] public float throwDuration;

    [SerializeField] public float throwDelay;

    [SerializeField] public float maxPickUpRange;

    [SerializeField] public float maxUnloadRange;

    [SerializeField] public float unloadTime;

}
