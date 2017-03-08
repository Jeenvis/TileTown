using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
    public enum MenuItem {farmGroup,buildingGroup,infastructureGroup,floraGroup};
    public GameObject UI_FarmGroup;
    public GameObject UI_BuildingGroup;
    public GameObject UI_InfastructureGroup;
    public GameObject UI_FloraGroup;
    public MenuItem startMenu = MenuItem.farmGroup;
	// Use this for initialization
	void Start () {
        ShowMenu(startMenu);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ShowMenu(MenuItem menuItem)
    {
        switch (menuItem)
        {
            case MenuItem.farmGroup://Farms
                SetAllUIGroups(false);
                UI_FarmGroup.SetActive(true);
                break;
            case MenuItem.buildingGroup://Buildings
                UI_BuildingGroup.SetActive(true);
                break;
            case MenuItem.infastructureGroup://Infastructure
                UI_InfastructureGroup.SetActive(true);
                break;
            case MenuItem.floraGroup://Flora
                UI_FloraGroup.SetActive(true);
                break;
        }
    }
    void SetAllUIGroups(bool value)
    {
        UI_FarmGroup.SetActive(true);
    }
}
