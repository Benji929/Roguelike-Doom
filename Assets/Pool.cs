using UnityEngine;

[CreateAssetMenu(fileName = "PoolSO", menuName = "Scriptable Object/Pool", order = 1)]
public class Pool : ScriptableObject
{
    public string tag;
    public GameObject prefab;
    public int size;
    public bool canExpand;
}
