using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animasyon : MonoBehaviour
{
    [Header("AnimasyonIcinGerekli")]
    private Animator anim;

    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    public void BaslangicAnimasyonAyari()  //UIController/TapToStart() 
    {
        anim.SetBool("KosmaP", true);
    }

    public void OyunSonuAyari()
    {
        anim.SetBool("KosmaP", false);
    }

    public void FotografCek()
    {
        anim.SetTrigger("FotoCekmeP");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
