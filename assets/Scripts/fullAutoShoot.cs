using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fullAutoShoot : MonoBehaviour
{

    public float recoilIntensity;
    private bool recoilLimit = true;

    private ParticleSystem muzzleFlash;
    private GameObject flash;
    public AudioSource Shot_snd;
    public AudioClip Shot;
    public Weapon weaponScript;


    private Vector3 origin_position;

    // Start is called before the first frame update
    void Start()
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
        if (weaponScript.currentPrimaryAmmo >= 0)
        {
            //puts GameObjects back into variables if current equipped gun was destroyed previously (I legit don't know how to properly word this) 
            if (flash == null)
            {
                flash = GameObject.FindGameObjectWithTag("MuzzleFlash");
                muzzleFlash = flash.GetComponent<ParticleSystem>();
            }

            Recoil();
        }
    }

    private void Recoil()
    {

        //if rightMouseButton pressed and recoil limit reached, lerp to recoilTarget location
        //also different from regular shoot script because now it checks to see if shoot function is being called in eapon script
        if (weaponScript.fullAutoUpdater && recoilLimit)
        {
            weaponScript.fullAutoUpdater = false;
            recoilLimit = false;
            muzzleFlash.Play();
            Shot_snd.PlayOneShot(Shot, 2);

            Vector3 recoilTarget = new Vector3(origin_position.x, origin_position.y, origin_position.z - 6);

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
