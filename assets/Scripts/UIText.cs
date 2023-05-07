using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text ammo;
    [SerializeField] private TMP_Text areaHit;
    [SerializeField] private TMP_Text timerText;
    public float targetTime = 2f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //count down timer if timer is not at 0
        if (targetTime >= 0f) {
            targetTime -= Time.deltaTime;
        }

        score.text = "Score: " + GameManager.Score;
        if (weapon.hasPrimary) ammo.text = weapon.currentPrimaryAmmo + " / " + weapon.totalAmmo;
        if (weapon.hasSecondary) ammo.text = weapon.currentSecondaryAmmo + " / " + weapon.totalAmmo;

        //once the target is shot, start the timer for how long the text will remain on screen.
        if (weapon.TargetHit != null)
        {
            areaHit.text = weapon.TargetHit;

            //timer will reset to 2 seconds each time target is shot
            targetTime = 2f;

            //wondering if this is even necessary (it is or else the if statement above will always be true)
            weapon.TargetHit = null;

        }

        //shitty workaround for muzzle flash and sound problem (fix later)
        if (weapon.currentPrimaryAmmo == -1 && weapon.hasPrimary)
        {
            ammo.text = 0 + " / " + weapon.totalAmmo;
        }

        else if(weapon.currentSecondaryAmmo == -1 && weapon.hasSecondary)
        {
            ammo.text = 0 + " / " + weapon.totalAmmo;
        }

        if (targetTime <= 0.0f)
        {
            areaHit.text = "";

        }

        if (GameManager.TestStarted)
        {
            timer += Time.deltaTime;
            timerText.text = "Timer: " + timer.ToString("N2");
        }

        if (GameManager.TimerReset)
        {
            timer = 0;
            GameManager.Score = 0;
            GameManager.TimerReset = false;
        }

    }

    //IEnumerator ExampleCoroutine()
    //{
    //    yield return new WaitForSeconds(2);
    //    areaHit.text = "";
    //    weapon.TargetHit = null;
    //}
}
