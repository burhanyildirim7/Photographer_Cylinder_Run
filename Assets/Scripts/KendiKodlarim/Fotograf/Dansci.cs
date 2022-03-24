using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dansci : MonoBehaviour
{
    [Header("DansciAyarlari")]
    public int karakterNumarasi;
    public int dansNumarasi;


    [Header("AnimasyonAyarlari")]
    private Animator anim;

    [Header("Rotasyon")]
    RaycastHit hit;
    private GameObject platform;

    void Start()
    {
        platform = GameObject.FindWithTag("Platform");
        anim = transform.GetChild(0).GetComponent<Animator>();

       // anim.SetInteger("DansNumarasiP", dansNumarasi);

       // KarakteriZemineGoreCevir();
    }


  /*  private void KarakteriZemineGoreCevir()
    {
        if (Physics.Raycast(transform.position, Vector3.up * (platform.transform.position.y - transform.position.y) + Vector3.right * (platform.transform.position.x - transform.position.x), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Platform"))
            {
                transform.position = hit.point;
                transform.rotation = Quaternion.FromToRotation(transform.right, hit.normal) * transform.rotation;
            }
        }
    }*/

}
