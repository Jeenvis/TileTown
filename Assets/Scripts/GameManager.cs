using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
    float playingTimePause = 2;
    bool playing;
    bool shouldContinue;
    Vector2 tiles = new Vector2(25, 25);
    private Material preSelectedMaterial;
    public GameObject selectionCube;
    public GameObject selectedBuildObj;
    public Material selectedMaterial;
    private GameObject selectedObj;
    private GameObject lastSelectedObj;
    public GameObject ui_NameField;
    public GameObject ui_DescriptionField;
    public GameObject buildingObj;
    public GameObject tileObj;
    private GameObject parentObj;
    private GameObject[,] allTiles = new GameObject[0,0];
	// Use this for initialization
	void Start () {
        allTiles = new GameObject[(int)tiles.x, (int)tiles.y];
        SetupGame();
	}
    void SetupGame()
    {
        parentObj = new GameObject();
        parentObj.name = "TileGroupObj";
        for (int x = 0; x < tiles.x; x++)
        {
            for(int z = 0; z < tiles.y; z++)
            {
                GameObject tempTileObj = Instantiate(tileObj, new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z),transform.rotation) as GameObject;
                tempTileObj.transform.parent = parentObj.transform;
                allTiles[x, z] = tempTileObj;
            }
        }
        parentObj.transform.position = new Vector3(transform.position.x - (tiles.x / 2), transform.position.y, transform.position.z - (tiles.y / 2));
        playing = true;
        StartCoroutine(BuildingPopUpTimer());
    }
    IEnumerator BuildingPopUpTimer()
    {
        yield return new WaitForSeconds(playingTimePause);
        BuildingPopUp();
    }
    void BuildingPopUp()
    {
        do
        {
            int searchTileX = Random.Range(0,(int)tiles.x);
            int searchTileZ = Random.Range(0, (int)tiles.y);
            if (allTiles[(int)searchTileX, (int)searchTileZ].GetComponent<TileManager>().occupied == false)
            {
                shouldContinue = false;
                GameObject tempBuildingObj = Instantiate(buildingObj, new Vector3(allTiles[(int)searchTileX,(int)searchTileZ].transform.position.x, 1, allTiles[(int)searchTileX, (int)searchTileZ].transform.position.y), transform.rotation) as GameObject;
                if (Camera.main.transform.parent.GetComponent<CameraManager>() != null)
                {
                    Camera.main.transform.parent.GetComponent<CameraManager>().targetPos = new Vector3(tempBuildingObj.transform.position.x, Camera.main.transform.parent.GetComponent<CameraManager>().targetPos.y, tempBuildingObj.transform.position.z - 2.5f);
                    Camera.main.transform.parent.GetComponent<CameraManager>().moveAble = false;
                    Camera.main.transform.parent.GetComponent<CameraManager>().checkMoveAble = true;
                    playing = false;
                    StartCoroutine(PlayingTimer());
                }
            }
        } while (shouldContinue == true);
    }
    IEnumerator PlayingTimer()
    {
        yield return new WaitForSeconds(4);
        playing = true;
    }
	// Update is called once per frame
	void Update () {
        if (playing == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "RaycastObj")
                {
                    selectionCube.transform.position = hit.transform.position + hit.normal;
                    if (hit.transform.GetComponent<BuildingManager>() != null)
                    {
                        ui_NameField.GetComponent<Text>().text = hit.transform.GetComponent<BuildingManager>().buildingName;
                        ui_DescriptionField.GetComponent<Text>().text = hit.transform.GetComponent<BuildingManager>().buildingDescription;
                    }
                    else
                    {
                        ui_NameField.GetComponent<Text>().text = "";
                        ui_DescriptionField.GetComponent<Text>().text = "";
                    }
                }
                else
                {
                    selectionCube.transform.position = new Vector3(999999, 999999, 999999);
                }
                if (Input.GetMouseButtonDown(0) && (hit.transform.GetComponent<TileManager>() != null || hit.transform.GetComponent<RoadManager>() != null))
                {
                    Instantiate(selectedBuildObj, hit.transform.position + hit.normal, transform.rotation);
                    if (hit.transform.GetComponent<TileManager>() == true)
                    {
                        hit.transform.GetComponent<TileManager>().occupied = true;
                    }
                }
                if (Input.GetMouseButtonDown(1) && hit.transform.GetComponent<RoadManager>() != null || hit.transform.GetComponent<BuildingManager>() != null)
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
