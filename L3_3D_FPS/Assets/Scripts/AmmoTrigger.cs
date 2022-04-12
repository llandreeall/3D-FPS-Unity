using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTrigger : MonoBehaviour
{
    public Player pl;
    private void Start()
    {
        if(GameObject.Find("Player") != null)
            pl = GameObject.Find("Player").GetComponent<Player>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            pl = other.transform.gameObject.GetComponent<Player>();
            if (pl != null)
            {
                GameObject gun = pl.weapons.ReturnSelected();
                Gun g = gun.GetComponent<Gun>();
                if (Input.GetKeyDown(KeyCode.Q))
                {

                    g.AddToReloads(50);
                    //transform.parent.GetComponent<PickupController>().DestroyObj();
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
