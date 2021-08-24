using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Destruction : MonoBehaviour
{
    [SerializeField]
    string groundTag;
    [SerializeField]
    string CheckPointTag;
    [SerializeField]
    CinemachineVirtualCamera followCam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(groundTag))
        {
            UIWork.instance.TweenRetryPanel();
            this.GetComponent<Car>().enabled = false;
            for (int i=0;i<this.transform.childCount-2;i++)
            {
                this.transform.GetChild(i).GetComponent<Rigidbody2D>().isKinematic = false;
            }

        }
        if(collision.gameObject.CompareTag(CheckPointTag))
        {
            Debug.Log("Completed");
            UIWork.instance.TweenLevelCompeletePanel();
        }
    }
}
