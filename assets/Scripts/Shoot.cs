using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float recoilIntensity;
    private bool recoilLimit = true;
    
    private ParticleSystem muzzleFlash;
    private GameObject flash;
    public AudioSource Shot_snd;
    public AudioClip Shot;
    public Weapon weaponScript;


    private Vector3 origin_position;

    private void Start()
    {
        //I can't just make the particle system public and drag the prefab because apperantly it relies on the actual gameobject connected to the gun
        flash = GameObject.FindGameObjectWithTag("MuzzleFlash");
        muzzleFlash = flash.GetComponent<ParticleSystem>();
        //Shot_snd = GetComponent<AudioSource>();
        origin_position = transform.localPosition;
        weaponScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponScript.currentSecondaryAmmo >= 0 && !weaponScript.isReloading)
        {
            //puts GameObjects back into variables if current equipped gun was destroyed previously (I legit don't know how to properly word this) 
            if (flash == null)
            {
                flash = GameObject.FindGameObjectWithTag("MuzzleFlash");
                muzzleFlash = flash.GetComponent<ParticleSystem>();
            }

            //my shitty workaround because as soon as the ammo count reached zero it would stop making the noise and muzzle flash
            if (weaponScript.currentSecondaryAmmo == 0)
            {
                weaponScript.currentSecondaryAmmo--;
            }

            Recoil();
        }
       
    }

    private void Recoil()
    {

        //if rightMouseButton pressed and recoil limit reached, lerp to recoilTarget location
        if (Input.GetMouseButtonDown(0) && recoilLimit)
        {
            recoilLimit = false;

            muzzleFlash.Play();
            Shot_snd.PlayOneShot(Shot, 2);

            Vector3 recoilTarget = new Vector3();

            //changes recoil rate based on whether or not there's player movement
            //had to implement because recoil would be harsher whenever player moved
            if(Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0)
            {
                recoilTarget = new Vector3(origin_position.x, origin_position.y, origin_position.z - 3);
            }

            else
            {
                recoilTarget = new Vector3(origin_position.x, origin_position.y, origin_position.z - 6);
            }

            transform.localPosition = Vector3.Lerp(origin_position, recoilTarget, Time.deltaTime * recoilIntensity);
        }

        //if it's not being pressed, then 
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, origin_position, Time.deltaTime * recoilIntensity);
            recoilLimit = true;
        }
    }
}