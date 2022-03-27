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
