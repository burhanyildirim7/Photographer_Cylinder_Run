using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animasyon : MonoBehaviour
{
    [Header("AnimasyonIcinGerekli")]
    private Animator anim;


    private WaitForSeconds beklemeSuresi0 = new WaitForSeconds(1);

    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    public void BaslangicAnimasyonAyari()  //UIController/TapToStart() 
    {
        anim.ResetTrigger("SavrulmaP");
        anim.SetBool("KosmaP", true);
    }

    public void OyunSonuAyari()
    {
        anim.SetBool("KosmaP", false);
    }

    public void FotografCek()
    {
        anim.SetTrigger("FotoCekmeP");
        anim.SetBool("KosmaP", false);
        StartCoroutine(SaniyelikDurdur());
    }


    IEnumerator SaniyelikDurdur()
    {
        yield return beklemeSuresi0;
        anim.SetBool("KosmaP", true);
    }
}
