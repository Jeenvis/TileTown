using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public bool moveAble = true;
    public bool checkMoveAble = false;
    float distanceMovement = 0.075f;
    float distanceCheckMoveAble = 0.25f;
    float speedMovement = 7.5f;
    float scrollDistance = 0.075f;
    float scrollMovement = 10;
    public Vector3 targetPos;
    Vector3 targetCamPos;
    GameObject camChild;
    void Start()
    {
        targetPos = transform.position;
        
        camChild = transform.FindChild("Main Camera").gameObject;
        targetCamPos = camChild.transform.position;
    }
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 1 * Time.deltaTime * speedMovement * Mathf.Clamp(Vector3.Distance(transform.position,targetPos),0,1));
        if(camChild.transform.position != targetCamPos)
        {
            //camChild.transform.position = Vector3.MoveTowards(camChild.transform.position, targetCamPos, 1 * Time.deltaTime * scrollMovement * Vector3.Distance(camChild.transform.position, targetCamPos));
        }
        if (moveAble == true)
        {
            CameraMovement();
        }
        if(checkMoveAble == true)
        {
            if(Vector3.Distance(transform.position,targetPos) < distanceCheckMoveAble)
            {
                moveAble = true;
                checkMoveAble = false;
            }
        }
	}
    void CameraMovement()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            targetCamPos = new Vector3(targetCamPos.x, targetCamPos.y, targetCamPos.z + scrollDistance);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            targetCamPos = new Vector3(targetCamPos.x, targetCamPos.y, targetCamPos.z - scrollDistance);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKeyUp(KeyCode.A))
        {
            targetPos = new Vector3(targetPos.x - distanceMovement, targetPos.y, targetPos.z);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKeyUp(KeyCode.D))
        {
            targetPos = new Vector3(targetPos.x + distanceMovement, targetPos.y, targetPos.z);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKeyUp(KeyCode.S))
        {
            targetPos = new Vector3(targetPos.x, targetPos.y, targetPos.z - distanceMovement);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKeyUp(KeyCode.W))
        {
            targetPos = new Vector3(targetPos.x, targetPos.y, targetPos.z + distanceMovement);
        }

    }

}
