/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Singleton

    public static Player instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public PistolFire weapons;
    public int maxHealth = 100;
    public int currHealth;
    public Slider sl;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        sl.value = maxHealth;
    }

    public void TakeDamage(int value)
    {
        currHealth -= value;
        if (currHealth <= 0)
            currHealth = 0;
        sl.value = currHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TakeDamage(10);
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviour
{
    #region Singleton

    public static Player instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public PistolFire weapons;
    public int maxHealth = 100;
    public int currHealth;
    public Slider sl;
    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        if (view.IsMine)
        {
            GameManager.gm_instance.SetPlayerComponents(this);
            currHealth = maxHealth;
            sl.value = maxHealth;
        }
    }

    public void TakeDamage(int value)
    {
        currHealth -= value;
        if (currHealth <= 0)
            currHealth = 0;
        sl.value = currHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.P))
                TakeDamage(10);
        }
    }

    public void SetSlider(Slider newS)
    {
        sl = newS;
    }

}