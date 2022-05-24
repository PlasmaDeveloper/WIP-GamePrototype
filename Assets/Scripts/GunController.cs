using UnityEngine;

public class GunController : MonoBehaviour
{
    //objects and components
    private Camera camera;
    [SerializeField] ParticleSystem muzzleFlash;
    
    //values
    private float damage = 10.0f;
    private float range = 100f;

    // Start is called before the first frame update
    private void Start()
    {
        SetupScript();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireGun();
        }

    }

    private void SetupScript()
    {
        camera = Camera.main;
    }

    private void FireGun() 
    {
        PlayMuzzleFlash();

        RaycastHit hitInfo;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);

            DamageTarget(hitInfo);
        }
    }

    private void DamageTarget(RaycastHit hit) 
    {
        TargetController target = hit.transform.GetComponent<TargetController>();
        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }
}
