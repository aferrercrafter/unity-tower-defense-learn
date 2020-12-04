using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoney;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;
    private Color startColor;
    private Renderer rend;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }
    private void OnMouseEnter()
    {
        if (!CanInteract())
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoney;
        }

    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (!CanInteract())
            return;

        if(turret != null)
        {
            Debug.Log("Can't Build there! TODO UI Message");
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    private bool CanInteract()
    {
        return (!EventSystem.current.IsPointerOverGameObject() && buildManager.CanBuild);
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
