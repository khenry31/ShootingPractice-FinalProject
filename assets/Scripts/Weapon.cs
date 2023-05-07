using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class Weapon : MonoBehaviour
{

    public Gun[] loadout;
    public Transform weaponParent;
    public GameObject bulletHolePrefab;
    public LayerMask canBeShot;
    public LayerMask interactables;
    

    public AudioSource reload_snd;
    public AudioClip reload;

    private bool isFiring;

    //I feel like there could have been an easier way to deal with these variables
    //because this feels like too much and looks confusing
    public bool primaryEquipped = false;
    public bool secondaryEquipped = false;
    public bool hasPrimary = false;
    public bool hasSecondary = false;

    public bool emptyMag = false;
    public bool fullAutoUpdater = false;
    public bool isReloading = false;

    private float shotCounter;

    public int currentPrimaryAmmo;
    public int currentSecondaryAmmo;
    public int totalAmmo;
    public int currentIndex;
    private GameObject currentWeapon;
    private GameObject crosshair;

    public int headshotScore;
    public int midScore;
    public int lowScore;
    public string TargetHit;

    public int headshotDamage = 0;
    public int centerOfMassDamage = 0;
    public int generalDamage = 0;
    public float reloadTestTime = 0;

    public TMP_Text InteractPrompt;

    // Start is called before the first frame update
    void Start()
    {
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
        //figure out a way for the player to equip next weapon using scroll wheel
        if (Input.GetKeyDown(KeyCode.Alpha1) && primaryEquipped && secondaryEquipped &&!hasSecondary) { Equip(1); hasPrimary = false;  hasSecondary = true; };
        if (Input.GetKeyDown(KeyCode.Alpha2) && primaryEquipped && secondaryEquipped &&!hasPrimary) { Equip(2); hasPrimary = true; hasSecondary = false; };

        if (currentWeapon != null)
        {
            Aim(Input.GetMouseButton(1));

            //semiauto shooting
            if (currentIndex == 1)
            {
                
                if (Input.GetMouseButtonDown(0) && currentSecondaryAmmo > 0 && !isReloading)
                {
                    Shoot();
                    currentSecondaryAmmo--;
                }

            }

            //full auto shooting
            if(currentIndex == 2)
            {
                //might involve isReloading because this could potentially cause glitches/problems later down the line
                if (Input.GetMouseButtonDown(0) && currentPrimaryAmmo > 0 && !isReloading)
                {
                    isFiring = true;
                }

                else if (Input.GetMouseButtonUp(0))
                {
                    isFiring = false;
                }

                if (isFiring && currentPrimaryAmmo > 0)
                {
                    shotCounter -= Time.deltaTime;

                    if(shotCounter <= 0)
                    {
                        shotCounter = loadout[currentIndex].fireRate;
                        Shoot();
                        currentPrimaryAmmo--;
                    }

                }

                else
                {
                    shotCounter -= Time.deltaTime;
                }

                
            }

            //reload (fix later: make it so that player isn't able to reload with a full mag)
            if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                Reload();
            }

            if (isReloading)
            {
                reloadTestTime -= Time.deltaTime;

                if (reloadTestTime <= 0)
                {
                    isReloading = false;
                    reloadTestTime = 0.75f;
                }
            }

        }

        //raycast that searches for interactable objects
        objectInteract();
    }

    void Equip(int p_ind)
    {
        currentIndex = p_ind;

        if (currentWeapon != null) Destroy(currentWeapon);

        GameObject t_newWeapon = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        t_newWeapon.transform.localPosition = Vector3.zero;
        t_newWeapon.transform.localEulerAngles = new Vector3(0,0,0);

        currentWeapon = t_newWeapon;
        totalAmmo = loadout[p_ind].totalAmmo;
        //currentAmmo = totalAmmo;
    }

    void Aim(bool p_isAiming)
    {
        //disables crosshair

        //looks for children of object
        Transform t_anchor = currentWeapon.transform.Find("Anchor");
        Transform t_state_ads = currentWeapon.transform.Find("States/ADS");
        Transform t_state_hip = currentWeapon.transform.Find("States/Hip");

        if (p_isAiming)
        {
            //aim
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
            crosshair.SetActive(false);
        }

        else
        {
            //hip
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
            crosshair.SetActive(true);
        }
    }

    void Shoot()
    {
        Transform t_spawn = transform.Find("Cameras/Camera");

        Ray ray = new Ray(t_spawn.position, t_spawn.forward);

        RaycastHit t_hit = new RaycastHit();

        if(currentIndex == 2) fullAutoUpdater = true;

        if (Physics.Raycast(ray, out t_hit, 100f, canBeShot))
        {
            GameObject t_newHole = Instantiate(bulletHolePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
            
            if(t_hit.transform.parent.tag == "staticTarget")
            {
                scoreTracker(t_hit, t_newHole);
            }

            if(t_hit.transform.parent.tag == "Target")
            {
                movingTargetScoreTracker(t_hit, t_newHole);
            }

            t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
            Destroy(t_newHole, 3f);
        }
    }

    void scoreTracker(RaycastHit copy_hit, GameObject hole)
    {
        //REMEMBER! it needs to go to the parent!


        if(copy_hit.transform.tag == "Headshot")
        {
            //had to have bulletholes on a different layer to be ignored because they blocked the collider from the raycast
            GameManager.Score += headshotScore;
            TargetHit = "Headshot!";

        }

        if (copy_hit.transform.tag == "General")
        {
            GameManager.Score += lowScore;
            TargetHit = "General Hit";

        }

        if (copy_hit.transform.tag == "CenterOfMass")
        {
            GameManager.Score += midScore;
            TargetHit = "Center of Mass Hit";
        }

    }

    void movingTargetScoreTracker(RaycastHit copy_hit, GameObject hole)
    {
        TargetStand currentTarget = copy_hit.transform.parent.gameObject.GetComponent<TargetStand>();


        if (copy_hit.transform.tag == "Headshot" && !currentTarget.isShot)
        {
            //had to have bulletholes on a different layer to be ignored because they blocked the collider from the raycast
            //also change gameManager scores to private and make them getters and setters
            GameManager.Score += headshotScore;
            TargetHit = "Headshot!";
            currentTarget.takeDamage(headshotDamage);

        }

        if (copy_hit.transform.tag == "General" && !currentTarget.isShot)
        {
            GameManager.Score += lowScore;
            TargetHit = "General Hit";
            currentTarget.takeDamage(generalDamage);

        }

        if (copy_hit.transform.tag == "CenterOfMass" && !currentTarget.isShot)
        {
            GameManager.Score += midScore;
            TargetHit = "Center of Mass Hit";
            currentTarget.takeDamage(centerOfMassDamage);
        }

        if (copy_hit.transform.parent.tag == "Target" && !currentTarget.isShot)
        {
            hole.transform.parent = currentTarget.transform;
        }
    }

    void objectInteract()
    {
        Transform t_spawn = transform.Find("Cameras/Camera");

        Ray ray = new Ray(t_spawn.position, t_spawn.forward);

        RaycastHit t_hit = new RaycastHit();


        if (Physics.Raycast(ray, out t_hit, 2f, interactables))
        {
            //if interactable object is the pistol
            if(t_hit.transform.tag == "pistol")
            {
                InteractPrompt.text = "press E to pick up Pistol";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(t_hit.transform.gameObject);
                    Equip(1);
                    secondaryEquipped = true;
                    hasSecondary = true;
                    hasPrimary = false;
                    currentSecondaryAmmo = t_hit.transform.GetComponentInParent<pistolAmmo>().currentAmmo;
                }
            }
            
            //if interactable object is the MCX
            if (t_hit.transform.tag == "MCX")
            {
                InteractPrompt.text = "press E to pick up MCX";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(t_hit.transform.gameObject);
                    Equip(2);
                    hasPrimary = true;
                    hasSecondary = false;
                    primaryEquipped = true;
                    currentPrimaryAmmo = loadout[currentIndex].totalAmmo;
                }
            }

            if(t_hit.transform.tag == "button")
            {
                InteractPrompt.text = "Press E to start test";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameManager.TestStarted = true;
                    GameManager.TestEnded = false;
                    GameManager.TimerReset = true;
                }
            }
        }

        else
        {
            InteractPrompt.text = "";
        }
    }

    void Reload()
    {
        isReloading = true;
        if(currentIndex == 1) currentSecondaryAmmo = loadout[currentIndex].totalAmmo;
        if (currentIndex == 2) currentPrimaryAmmo = loadout[currentIndex].totalAmmo;
        reload_snd.PlayOneShot(reload, 2);

    }
}
