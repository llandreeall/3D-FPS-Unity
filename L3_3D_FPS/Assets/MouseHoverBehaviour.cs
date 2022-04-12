using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverBehaviour : MonoBehaviour
{
    public GameObject obj;
    void Start()
    {
        obj.SetActive(false);
    }

    void OnMouseOver()
    {
        obj.SetActive(true);
    }

    void OnMouseExit()
    {
        obj.SetActive(false);
    }
}
