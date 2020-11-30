using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject standardTurretPrefab;
    public GameObject missileTurretPrefab;
    public bool CanBuild { get { return turretToBuild != null; } }

    private TurretBlueprint turretToBuild;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Build Manager in scene, restart");
            return;
        }
            
        instance = this;
    }

    public void SelectTorretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        Debug.Log("Set turret to build");
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to buy that");
            return;
        }

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Construction complete, money left: " + PlayerStats.money);
    }

}
