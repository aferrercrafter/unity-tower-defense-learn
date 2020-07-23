using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;    

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SetStandarTurretBuild()
    {
        buildManager.setTorretToBuild(buildManager.standardTurretPrefab);
    }

    public void SetMissileTurretBuild()
    {
        buildManager.setTorretToBuild(buildManager.missileTurretPrefab);
    }

}
