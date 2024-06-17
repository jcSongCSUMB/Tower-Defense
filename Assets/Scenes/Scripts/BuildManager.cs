using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return; 
        }
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;
    private TurretBlueprint turretToBuild;

    public bool CanBuild => (turretToBuild != null);

    public bool HasMoney => turretToBuild != null && PlayerStats.Money >= turretToBuild.cost;



    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not Enough to build that");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject) Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret built! Money left: " + PlayerStats.Money);
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
    
}
