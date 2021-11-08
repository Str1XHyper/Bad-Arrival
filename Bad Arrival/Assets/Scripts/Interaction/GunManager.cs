using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private GameObject physicalBulletImpact;

    [SerializeField] private LayerMask IgnoredLayer;
    [HideInInspector] public int cooldown { get; private set; }

    private CinemachineImpulseSource cameraShake;
    [HideInInspector] public bool isReloading { get; private set; }

    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }
    private void FixedUpdate()
    {
        cooldown--;
    }

    public void Shoot(InventorySlot heldGun)
    {
        var equippedGun = Player.instance.equippedGunVisual;
        Transform transform = Camera.main.transform;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ~IgnoredLayer, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().ApplyDamage(heldGun.ItemObject.GetDamage(heldGun.item));
                UIManager.instance.ShowHitmarker();
            }

            Instantiate(physicalBulletImpact, hit.point, transform.rotation);
            equippedGun.GetComponent<AudioSource>().PlayOneShot(equippedGun.GetComponent<AudioSource>().clip);
            if (equippedGun.GetComponentInChildren<AudioSource>() != null)
            {
                StartCoroutine(Rack(equippedGun.transform.GetChild(0).GetComponent<AudioSource>()));
            }
        }
        heldGun.item.ammoInMag--;
        cooldown = Mathf.RoundToInt(60f / (heldGun.ItemObject.GetRPM(heldGun.item)) / Time.fixedDeltaTime);

        float recoilStrengthMapped = (float)((decimal)heldGun.ItemObject.GetRecoilStrength(heldGun.item)).Map(1, 100, 0, 3);

        //cameraShake.GenerateImpulse(Camera.main.transform.forward);
        cameraShake.GenerateImpulse(recoilStrengthMapped);

    }

    private IEnumerator Rack(AudioSource equippedGunRerackClip)
    {
        
        int RPM = Player.instance.GetHeldSlot().ItemObject.GetRPM(Player.instance.GetHeldSlot().item);
        float timeBetweenShotsInSeconds = 1f / (RPM / 60f);
        Debug.Log(timeBetweenShotsInSeconds);
        yield return new WaitForSeconds(timeBetweenShotsInSeconds - equippedGunRerackClip.clip.length);
        Debug.Log(equippedGunRerackClip.clip.name);
        equippedGunRerackClip.PlayOneShot(equippedGunRerackClip.clip);
    }

    public IEnumerator Reload(InventorySlot heldGun)
    {
        isReloading = true;
        yield return new WaitForSeconds(heldGun.ItemObject.GetReloadTime(heldGun.item));
        heldGun.item.ammoInMag = heldGun.ItemObject.GetMagCapacity(heldGun.item);
        isReloading = false;
    }
}
