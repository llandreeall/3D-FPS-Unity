using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager gm_instance;

    private void Awake()
    {
        gm_instance = this;
    }

    #endregion
    
    public GameObject GameCanvas;

    public Slider playerSlider;
    public Text currentAmmoPlayer;
    public Text reloadsPlayer;


    public void SetPlayerComponents(Player p)
    {
        //p.sl = playerSlider;
        p.SetSlider(playerSlider);
        p.weapons.PrepareCanvas(currentAmmoPlayer, reloadsPlayer);
    }
    
}
