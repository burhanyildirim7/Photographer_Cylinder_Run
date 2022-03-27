using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotoAnim : MonoBehaviour
{
    [Header("FotografIcin")]
    private FotografController fotografController;


    private Vector3 baslangicPos;
    public bool firlatildiMi = false;

    private float donusHizi;
    void Start()
    {
        fotografController = GameObject.FindObjectOfType<FotografController>();


        baslangicPos = transform.position;

        donusHizi = Random.Range(300, 700);

      
    }

    // Update is called once per frame
    void Update()
    {
        if(firlatildiMi)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -7);
            transform.localScale *= 1.04f;
            transform.Rotate((/*Vector3.forward * donusHizi +*/ Vector3.up * 100) * Time.deltaTime);

            if(transform.localScale.y >= 2)
            {
                Destroy(gameObject);
            }
        }

        
    }

    public void ResimEkle()
    {
        StartCoroutine(Geciktir());
    }

    IEnumerator Geciktir()
    {
        yield return new WaitForSeconds(.02f);
        fotografController.FotografYerineKoy();
        transform.parent.transform.gameObject.SetActive(false);
    }



    public void KonumaGonder()
    {
        StartCoroutine(HaraketEttir());
        StartCoroutine(BoyutuAyarla());
    }

    IEnumerator HaraketEttir()
    {
        while(Vector3.Distance(transform.localPosition, Vector3.zero) >= .1f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * 2f);
            transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.Euler(Vector3.up * 90 + Vector3.forward * 15), Time.deltaTime * 10);
            yield return null;
        }
    }

    IEnumerator BoyutuAyarla()
    {
        Vector3 baslangicBoyutu = transform.localScale;

        transform.localScale *= .25f;

        while(true)
        {
            if(transform.localScale.y <= baslangicBoyutu.y)
            {
                transform.localScale *= 1.015f;
            }
            yield return null;
        }
    }
}
