using UnityEngine;

[CreateAssetMenu(fileName = "New Block Info", menuName = "Scriptable Objects/Block Info")]
public class BlockInfo : ScriptableObject
{
    [SerializeField] public float timeOut;
    [SerializeField] public int blockCost;
    [SerializeField] public int blockCount =1;
}
