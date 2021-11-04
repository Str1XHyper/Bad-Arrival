using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVisualEffect : MonoBehaviour
{
    [SerializeField] private GameObject magazine;
    private InventorySlot heldGun;

    public void StartReloadAnimation(InventorySlot heldGun)
    {
        this.heldGun = heldGun;
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        var obj = Instantiate(magazine, transform.position, Quaternion.identity);
        obj.AddComponent<Rigidbody>();
        obj.AddComponent<MeshCollider>();
        obj.GetComponent<MeshCollider>().convex = true;
        magazine.SetActive(false);
        yield return new WaitForSeconds(heldGun.ItemObject.GetReloadTime(heldGun.item));
        magazine.SetActive(true);
    }
}
