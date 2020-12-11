
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shootscript : MonoBehaviour

{

    public float damage = 10f;
    public float range = 100f;
    public float firerate = 15f;

    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;

    private bool isReloading = false;
    private float nextTimeToFire = 0f; 

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject ImpactEffect;
    public AudioSource gunshot;
    public AudioSource Reload;
    public bool isFiring;
    public Text ammoDisplay;
    public Text reserveammo;

    // Update is called once per frame

    private void Start()
    {
        currentAmmo = maxAmmo;

        

    }
    void Update()
    {
        if (isReloading)
            return;
    
    
        if (currentAmmo <= -1)
        {
            StartCoroutine(Reload1());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / firerate;
            Shoot();
        }
        if (Input.GetButtonDown("Reload2"))
        {
            Reload1();
        }

    }

    
    IEnumerator Reload1()
    {
        isReloading = true;
        Reload.Play();

        Debug.Log("Reloading..");

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;


    }

    void Shoot()
    {
       ammoDisplay.text = currentAmmo.ToString();
       reserveammo.text = maxAmmo.ToString();

        muzzleFlash.Play();
        gunshot.Play();

        currentAmmo--;
        maxAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) { }
        {
            Debug.Log(hit.transform.name);


            traget target = hit.transform.GetComponent<traget>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            
        }
    }


}



