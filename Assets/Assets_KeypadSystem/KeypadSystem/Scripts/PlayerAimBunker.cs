using UnityEngine;
using UnityEngine.UI;

public class PlayerAimBunker : MonoBehaviour
{
    public Transform headPos;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(headPos.position, headPos.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(headPos.position, headPos.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance <= 3f)
            {
                if (Input.GetKeyDown("e"))
                {
                    if (hit.transform.GetComponent<KeypadKeyBunker>() != null)
                    {
                        hit.transform.GetComponent<KeypadKeyBunker>().SendKey();
                    }
                    // else if(hit.transform.name=="DoorMesh")
                    // {
                    //     hit.transform.GetComponent<DoorController>().OpenClose();
                    // }
                }
            }
        }
    }
}

