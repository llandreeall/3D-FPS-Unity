/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistolFire : MonoBehaviour
{
    public int selected = 0;

    private void Start()
    {
        SelectGun();
    }

    private void Update()
    {
        int previousSel = selected;
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selected >= transform.childCount - 1)
                selected = 0;
            else 
                selected++;
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selected <= 0)
                selected = transform.childCount - 1;
            else
                selected--;

        }
        if(previousSel != selected)
        {
            SelectGun();
        }
    }

    void SelectGun()
    {
        int i = 0;
        foreach(Transform gun in transform)
        {
            if(i == selected)
            {
                gun.gameObject.SetActive(true);
                gun.gameObject.GetComponent<Gun>().SetCurrentAmmo();
                gun.gameObject.GetComponent<Gun>().SetReloadsAmmo();
            } else
            {
                gun.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public GameObject ReturnSelected()
    {
        int i = 0;
        foreach (Transform gun in transform)
        {
            if (i == selected)
            {
                return gun.gameObject;
            }
            i++;
        }
        return null;
    }

    public void PrepareCanvas(Text current, Text reloads)
    {
        
        foreach (Transform gun in transform)
        {
            gun.gameObject.GetComponent<Gun>().curr = current;
            gun.gameObject.GetComponent<Gun>().rel = reloads;
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PistolFire : MonoBehaviour
{
    public int selected = 0;
    public PhotonView view;

    private void Start()
    {
        view = transform.root.GetComponent<PhotonView>();
        if (view.IsMine)
        {
            SelectGun();
        }
    }

    private void Update()
    {
        if (view.IsMine)
        {
            int previousSel = selected;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selected >= transform.childCount - 1)
                    selected = 0;
                else
                    selected++;

            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selected <= 0)
                    selected = transform.childCount - 1;
                else
                    selected--;

            }
            if (previousSel != selected)
            {
                SelectGun();
            }
        }
    }

    void SelectGun()
    {
        int i = 0;
        foreach (Transform gun in transform)
        {
            if (i == selected)
            {
                gun.gameObject.SetActive(true);
                gun.gameObject.GetComponent<Gun>().SetCurrentAmmo();
                gun.gameObject.GetComponent<Gun>().SetReloadsAmmo();
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public GameObject ReturnSelected()
    {
        int i = 0;
        foreach (Transform gun in transform)
        {
            if (i == selected)
            {
                return gun.gameObject;
            }
            i++;
        }
        return null;
    }

    public void PrepareCanvas(Text current, Text reloads)
    {

        foreach (Transform gun in transform)
        {
            gun.gameObject.GetComponent<Gun>().curr = current;
            gun.gameObject.GetComponent<Gun>().rel = reloads;
        }
    }
}
