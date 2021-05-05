using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    [SerializeField] int maxAmmo = 12;
    [SerializeField] int currentAmmo = 0;
    [SerializeField] float reloadTime = 1f;
    [SerializeField] bool isReloading = false;
    [SerializeField] int layerToDamage;
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 5;
    [SerializeField] Camera FpsCamera;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = FpsCamera.transform.rotation;
        if (isReloading)
            return;
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo<maxAmmo)
        {
            StartCoroutine( Reload());
            return;
        }
        if (Input.GetButtonDown("Fire1") && currentAmmo>0)
        {
            Shoot();
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    void Shoot()
    {
        currentAmmo--;
        RaycastHit hit;
        if(Physics.Raycast(FpsCamera.transform.position, FpsCamera.transform.forward, out hit, range))
        {
            if(hit.transform.gameObject.layer == layerToDamage)
            {
                hit.transform.gameObject.GetComponent<IHitable>().OnHit(damage);
            }
        }
    }
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}
