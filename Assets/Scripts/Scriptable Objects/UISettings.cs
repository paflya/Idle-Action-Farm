using UnityEngine;

[CreateAssetMenu(fileName = "New UI Settings", menuName = "Scriptable Objects/UI Settings")]
public class UISettings : ScriptableObject
{
    [SerializeField] public bool hasAnimation;

    [SerializeField] public float animationLength;

    [SerializeField] public bool isGradual;

    [SerializeField] public float durationTime;

}
