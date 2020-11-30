using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTorret;
    public TurretBlueprint missileTurret;

    private BuildManager buildManager;    

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandarTurretBuild()
    {
        buildManager.SelectTorretToBuild(standardTorret);
    }

    public void SelectMissileTurretBuild()
    {
        buildManager.SelectTorretToBuild(missileTurret);
    }

}
