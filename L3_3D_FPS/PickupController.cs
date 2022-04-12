using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Transform box;
    public GameObject box2;

    // Start is called before the first frame update
    void Start()
    {
        //Physics.IgnoreCollision(box.GetComponent<Collider>(), GetComponent<Collider>());
        
    }
    public void DestroyObj()
    {
        Destroy(gameObject, 0.2f);
    }
}
