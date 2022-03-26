using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotoAnim : MonoBehaviour
{
    [Header("FotografIcin")]
    private FotografController fotografController;


    void Start()
    {
        fotografController = GameObject.FindObjectOfType<FotografController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void KonumaGonder(Vector3 konum)
    {
        StartCoroutine(HaraketEttir(konum));
    }

    IEnumerator HaraketEttir(Vector3 konum)
    {
        while(Vector3.Distance(transform.position, konum) >= .1f)
        {
            transform.position = Vector3.Lerp(transform.position, konum, Time.deltaTime * 2.5f);
            transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.Euler(Vector3.up * 90 + Vector3.forward * 15), Time.deltaTime * 10);
            yield return null;
        }
    }
}
