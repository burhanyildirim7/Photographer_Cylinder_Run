using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dansci : MonoBehaviour
{
    [Header("DansciAyarlari")]
    public int dansNumarasi;
    public int dansciNumarasi;


    [Header("AnimasyonAyarlari")]
    private Animator anim;
    private GameObject player;
    private bool posVerildiMi;

    [Header("Rotasyon")]
    RaycastHit hit;
    private GameObject platform;

    [Header("Fotograflar")]
    public GameObject Pose1;
    public GameObject Pose2;

    private WaitForSeconds beklemeSuresi0 = new WaitForSeconds(.25f);

    void Start()
    {
        platform = GameObject.FindWithTag("Platform");
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

        posVerildiMi = false;




        if (Random.Range(0, 2) == 0)
        {
            dansNumarasi = 0;
        }
        else
        {
            dansNumarasi = 1;
        }

        StartCoroutine(UzaklikKontrol());
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


    IEnumerator UzaklikKontrol()
    {
        while (!posVerildiMi)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= 10)
            {
                posVerildiMi = true;

                if (dansNumarasi == 0)
                {
                    anim.SetBool("Pose1", true);
                }
                else if (dansNumarasi == 1)
                {
                    anim.SetBool("Pose2", true);
                }
            }
            yield return beklemeSuresi0;
        }
    }


    public void ResimOlustur()
    {

    }
}
