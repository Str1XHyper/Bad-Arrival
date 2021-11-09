using Cinemachine;
using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] private GameObject physicalBulletImpact;
    [SerializeField] private AudioSource GunShotSource;

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
            EventInstance GunShot = FMODUnity.RuntimeManager.CreateInstance(heldGun.ItemObject.fmodDirectory);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(GunShot, Camera.main.transform);
            GunShot.setVolume(2);
            GunShot.start();
            GunShot.release();
            //GunShotSource.PlayOneShot(heldGun.ItemObject.gunshotSFX, heldGun.ItemObject.volumeScale);
            if (heldGun.ItemObject.rackingSFX != null)
            {
                StartCoroutine(Rack(heldGun.ItemObject.fmodDirectoryRacking));
            }
        }
        heldGun.item.ammoInMag--;
        cooldown = Mathf.RoundToInt(60f / (heldGun.ItemObject.GetRPM(heldGun.item)) / Time.fixedDeltaTime);

        float recoilStrengthMapped = (float)((decimal)heldGun.ItemObject.GetRecoilStrength(heldGun.item)).Map(1, 100, 0, 3);

        //cameraShake.GenerateImpulse(Camera.main.transform.forward);
        cameraShake.GenerateImpulse(recoilStrengthMapped);

    }

    private IEnumerator Rack(string rackingDirectory)
    {
        int RPM = Player.instance.GetHeldSlot().ItemObject.GetRPM(Player.instance.GetHeldSlot().item);
        FMOD.Studio.EventInstance GunShot = FMODUnity.RuntimeManager.CreateInstance(rackingDirectory);
        float timeBetweenShotsInSeconds = 1f / (RPM / 60f);
        Debug.Log(timeBetweenShotsInSeconds);
        yield return new WaitForSeconds(timeBetweenShotsInSeconds - 0.760f);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(GunShot, Camera.main.transform);
        GunShot.start();
        GunShot.release();
    }

    public IEnumerator Reload(InventorySlot heldGun)
    {
        isReloading = true;
        yield return new WaitForSeconds(heldGun.ItemObject.GetReloadTime(heldGun.item));
        heldGun.item.ammoInMag = heldGun.ItemObject.GetMagCapacity(heldGun.item);
        isReloading = false;
    }
}
