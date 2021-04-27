using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int layerToDamage;
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 5;
    [SerializeField] Camera FpsCamera;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = FpsCamera.transform.rotation;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(FpsCamera.transform.position, FpsCamera.transform.forward, out hit, range))
        {
            if(hit.transform.gameObject.layer == layerToDamage)
            {
                hit.transform.gameObject.GetComponent<IHitable>().OnHit(damage);
            }
        }
    }
}
