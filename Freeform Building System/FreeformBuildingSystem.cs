using UnityEngine;
using System;

public class FreeformBuildingSystem : MonoBehaviour
{
    public bool IsCreatingBuilding { get; set; } = false;

    public event EventHandler OnBuildingCreated;

    public void CreateBuildingIndicator(BuildingTypeSO buildingTypeSO)
    {
        Transform building = Instantiate(buildingTypeSO.Prefab, transform.position, Quaternion.identity, transform);
        building.gameObject.AddComponent<BuildingIndicator>();
    }
}