using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] private FreeformBuildingSystem _freeformBuildingSystem;
    [SerializeField] private BuildingTypeSO _buildingTypeSO;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            if (_freeformBuildingSystem.IsCreatingBuilding == false)
            {
                _freeformBuildingSystem.IsCreatingBuilding = true;
                _freeformBuildingSystem.CreateBuildingIndicator(_buildingTypeSO);
            }
        });
    }
}