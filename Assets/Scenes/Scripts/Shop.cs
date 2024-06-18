using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileTurret;
    public TurretBlueprint laserBeamer;
    
    BuildManager _buildManager;

    void Start()
    {
        _buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        
        _buildManager.SelectTurretToBuild(standardTurret);
    }
    
    public void SelectMissileTurret()
    {
        
        _buildManager.SelectTurretToBuild(missileTurret);
    }
    
    public void SelectLaserBeamer()
    {
        
        _buildManager.SelectTurretToBuild(laserBeamer);
    }
}
