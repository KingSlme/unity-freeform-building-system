using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingTypeSO", menuName = "BuildingTypeSO")]
public class BuildingTypeSO : ScriptableObject
{
    [SerializeField] private Transform _prefab;

    public Transform Prefab => _prefab;
}