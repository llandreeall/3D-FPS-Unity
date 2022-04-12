/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public float fireRate = 0;
    float timeToFire = 0;
    public GameObject gun;
    public GameObject flash;
    public GameObject impactEffect;
    public float impactForce = 30.0f;

    public int maxAmmo = 50;
    [SerializeField]
    private int currentAmmo;
    public float reloadTime = 1f;
    public int reloads = 50;
    public int ammoFound = 20;
    bool isReloading = false;
    public Text curr;
    public Text rel;


    // Start is called before the first frame update
    void Start()
    {
        gun = gameObject;
        currentAmmo = maxAmmo;
        SetCurrentAmmo();
        SetReloadsAmmo();
    }

    public void SetCurrentAmmo()
    {
        curr.text = currentAmmo.ToString();
    }
    public void SetReloadsAmmo()
    {
        rel.text = reloads.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading == true)
            return;
        if(currentAmmo <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(reloads > 0)
                    StartCoroutine(Reload());
                return;
            }
        }
        if (fireRate == 0)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Fire();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                //flash.SetActive(false);
            }
        }
    }

    public void Fire()
    {
        
        RaycastHit hit;
        StartCoroutine(Flash());
        if (currentAmmo > 0 && Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            //Debug.Log(target);
            if (target != null)
            {
                target.TakeDamage(damage);
            
                if(hit.rigidbody != null)
                {
                    Debug.Log(target);
                    if (target.type == 1)
                    {
                        EnemyAI ai = hit.transform.GetComponent<EnemyAI>();
                        if(ai != null)
                        {
                            ai.hitt = hit;
                            ai.hit = true;
                        }
                    }
                    else if (target.type == 0)
                        hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);
            
        }
        currentAmmo--;
        if (currentAmmo <= 0)
            currentAmmo = 0;
        SetCurrentAmmo();
    }


    IEnumerator Flash()
    {
        if(currentAmmo > 0)
            flash.SetActive(true);
        gun.GetComponent<Animator>().SetTrigger("Shoot");
        yield return new WaitForSeconds(0.05f);
        gun.GetComponent<Animator>().SetTrigger("Shoot");
        flash.SetActive(false);

    }

    IEnumerator Reload()
    {
        isReloading = true;
        gun.GetComponent<Animator>().SetTrigger("DoReload");
        yield return new WaitForSeconds(reloadTime);
        if (reloads > 0 && reloads >= ammoFound)
        {
            currentAmmo += ammoFound;
            reloads -= ammoFound;
        } else if(reloads >= 0 && reloads <= ammoFound)
        {
            currentAmmo += reloads;
            reloads = 0;
        }
        if (currentAmmo >= maxAmmo)
            currentAmmo = maxAmmo;
        if(reloads <= 0)
        {
            reloads = 0;
        }
        SetCurrentAmmo();
        SetReloadsAmmo();
        isReloading = false; 

    }

    public void AddToReloads(int val)
    {
        reloads += val;
        if (reloads >= maxAmmo)
            reloads = maxAmmo;
        SetReloadsAmmo();
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    public PhotonView view;

    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public float fireRate = 0;
    float timeToFire = 0;
    public GameObject gun;
    public GameObject flash;
    public GameObject impactEffect;
    public float impactForce = 30.0f;

    public int maxAmmo = 50;
    [SerializeField]
    private int currentAmmo;
    public float reloadTime = 1f;
    public int reloads = 50;
    public int ammoFound = 20;
    bool isReloading = false;
    public Text curr;
    public Text rel;


    // Start is called before the first frame update
    void Start()
    {
        view = transform.root.GetComponent<PhotonView>();
        if (view.IsMine)
        {
            gun = gameObject;
            currentAmmo = maxAmmo;
            SetCurrentAmmo();
            SetReloadsAmmo();
        }
        
    }

    public void SetCurrentAmmo()
    {
        curr.text = currentAmmo.ToString();
    }
    public void SetReloadsAmmo()
    {
        rel.text = reloads.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (isReloading == true)
                return;
            if (currentAmmo <= 0)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (reloads > 0)
                        StartCoroutine(Reload());
                    return;
                }
            }
            if (fireRate == 0)
            {

                if (Input.GetButtonDown("Fire1"))
                {
                    Fire();
                }
            }
            else
            {
                if (Input.GetButton("Fire1") && Time.time > timeToFire)
                {
                    timeToFire = Time.time + 1 / fireRate;
                    Fire();
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    //flash.SetActive(false);
                }
            }
        }
    }

    public void Fire()
    {

        RaycastHit hit;
        StartCoroutine(Flash());
        if (currentAmmo > 0 && Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            //Debug.Log(target);
            if (target != null)
            {
                target.TakeDamage(damage);

                if (hit.rigidbody != null)
                {
                    //Debug.Log(target);
                    if (target.type == 1)
                    {
                        EnemyAI ai = hit.transform.GetComponent<EnemyAI>();
                        if (ai != null)
                        {
                            ai.hitt = hit;
                            ai.hit = true;
                        }
                    }
                    else if (target.type == 0)
                        hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.5f);

        }
        currentAmmo--;
        if (currentAmmo <= 0)
            currentAmmo = 0;
        SetCurrentAmmo();
    }


    IEnumerator Flash()
    {
        if (currentAmmo > 0)
            flash.SetActive(true);
        gun.GetComponent<Animator>().SetTrigger("Shoot");
        yield return new WaitForSeconds(0.05f);
        gun.GetComponent<Animator>().SetTrigger("Shoot");
        flash.SetActive(false);

    }

    IEnumerator Reload()
    {
        isReloading = true;
        gun.GetComponent<Animator>().SetTrigger("DoReload");
        yield return new WaitForSeconds(reloadTime);
        if (reloads > 0 && reloads >= ammoFound)
        {
            currentAmmo += ammoFound;
            reloads -= ammoFound;
        }
        else if (reloads >= 0 && reloads <= ammoFound)
        {
            currentAmmo += reloads;
            reloads = 0;
        }
        if (currentAmmo >= maxAmmo)
            currentAmmo = maxAmmo;
        if (reloads <= 0)
        {
            reloads = 0;
        }
        SetCurrentAmmo();
        SetReloadsAmmo();
        isReloading = false;

    }

    public void AddToReloads(int val)
    {
        reloads += val;
        if (reloads >= maxAmmo)
            reloads = maxAmmo;
        SetReloadsAmmo();
    }
}