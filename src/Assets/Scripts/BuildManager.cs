using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return turretToBuild.cost <= PlayerStats.Money; } }
    public GameObject buildEffect;

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
        Debug.Log("Player money: " + PlayerStats.Money + " Turret: " + turretToBuild.cost);

        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to buy that " + turretToBuild.cost);
            return;
        }

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        PlayerStats.Money = PlayerStats.Money - turretToBuild.cost;
        Debug.Log("Construction complete, money left: " + PlayerStats.Money);
    }

}
