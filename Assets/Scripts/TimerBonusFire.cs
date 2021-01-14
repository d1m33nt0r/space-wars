using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimerBonusFire : MonoBehaviour
{
    bool ChangedShoot;
    public GameObject ButtonShoot;
    public GameObject ButtonMegaShoot;

    private int timer;

    public void BonusStart()
    {
        ChangeMegaFire();
        InvokeRepeating("timerDecrement", 1, 1);
    }

    void timerDecrement()
    {
        if (timer > 0)
        { 
            timer--;
        }

        if (timer == 0)
        {
            ChangeFire();
        }
    }

    public void ReStartTimer()
    {
        timer = 20;
    }

    void ChangeMegaFire()
    {
        ButtonShoot.SetActive(false);
        ButtonMegaShoot.SetActive(true);
        ChangedShoot = false;
    }

    void ChangeFire()
    {
        ButtonShoot.SetActive(true);
        ButtonMegaShoot.SetActive(false);
        ChangedShoot = true;
    }
}
