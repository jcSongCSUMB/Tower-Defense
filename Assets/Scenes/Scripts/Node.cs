using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color MoneyInefficiencyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector] 
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
    
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
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (turret != null)
        {
            _buildManager.SelectNode(this);
            return;
        }
        
        if (!_buildManager.CanBuild)
            return;

        BuildTurret(_buildManager.GetTurretToBuild());
    }
    
    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not Enough to build that");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject) Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(_buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not Enough to upgrade that");
            return;
        }

        // get rid of the old turret
        PlayerStats.Money -= turretBlueprint.upgradeCost;
        Destroy(turret);
        
        // build the upgraded one
        GameObject _turret = (GameObject) Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        isUpgraded = true;
        
        GameObject effect = (GameObject)Instantiate(_buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }
    
    
    
    
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
