using UnityEngine;

[CreateAssetMenu(fileName = "New Movement Info", menuName = "Scriptable Objects/Movement Info")]
public class MovementInfo : ScriptableObject
{
    [SerializeField] public float playerSpeed;

    [SerializeField] public float cutRange;

}


