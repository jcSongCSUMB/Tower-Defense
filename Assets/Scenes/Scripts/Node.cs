using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color MoneyInefficiencyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;
    
    BuildManager _buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        _buildManager = BuildManager.instance;
    }
    
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    
        if (!_buildManager.CanBuild)
        {
            return;
        }
    
        rend.material.color = _buildManager.HasMoney ? hoverColor : MoneyInefficiencyColor;
    }


    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void OnMouseDown()
    {
        if (!_buildManager.CanBuild)
            return;
        
        if (turret != null)
        {
            return;
        }

        _buildManager.BuildTurretOn(this);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
