using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingIndicator : MonoBehaviour
{
    private KeyCode _cancelBuildingKey = KeyCode.Escape;
    private Color _validColor = Color.green;
    private Color _invalidColor = Color.red;
    private int _buildableLayerMask;
    private bool _validLocation = true;

    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        ToggleScriptComponents(false);
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _meshRenderer.material.color = _validLocation ? _validColor : _invalidColor;
        _buildableLayerMask = LayerMask.NameToLayer("Buildable");
    }

    private void Update()
    {
        FollowMouse();
        HandleInput();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Buildable"))
            return;
        _meshRenderer.material.color = _invalidColor;
        _validLocation = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Buildable"))
            return;
        _meshRenderer.material.color = _validColor;
        _validLocation = true;
    }

    private void FollowMouse()
    {
        Vector3? mousePosition = GetMouseWorldPosition();
        if (mousePosition.HasValue)
            transform.position = mousePosition.Value;
    }

    private Vector3? GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, 1 << _buildableLayerMask))
        {
            return hit.point;
        }    
        return null;
    }

    private void HandleInput()
    {   
        if (Input.GetKeyDown(_cancelBuildingKey))
            CancelBuilding();
        if (_validLocation && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            PlaceBuilding();
    }

    private void CancelBuilding()
    {
        GetComponentInParent<FreeformBuildingSystem>().IsCreatingBuilding = false;
        Destroy(gameObject);
    }

    private void PlaceBuilding()
    {
        GetComponentInParent<FreeformBuildingSystem>().IsCreatingBuilding = false;
        ToggleScriptComponents(true);
        Destroy(this);
    }

    private void ToggleScriptComponents(bool enable)
    {
        foreach (Component component in GetComponentsInChildren<Component>())
        {
            if (!(component is Transform || component is MeshFilter || component is MeshRenderer || component is Collider))
                if (component is Behaviour && component is not BuildingIndicator)
                    ((Behaviour)component).enabled = enable;
        }
    }
}