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
    
    public GameObject buildEffect;
    
    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;

    public bool CanBuild => (turretToBuild != null);

    public bool HasMoney => turretToBuild != null && PlayerStats.Money >= turretToBuild.cost;
    

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeSelectNode();
            return;
        }
        
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeSelectNode();
    }
    
    public void DeSelectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    
}
