using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotografController : MonoBehaviour
{

    [Header("FotografCekmeAyarlari")]
    RaycastHit hit;
    private List<GameObject> fotografCekilenler = new List<GameObject>();

    [Header("Efektler")]
    [SerializeField] private ParticleSystem fotografEfekt;

    [Header("Animasyon")]
    private Animasyon animasyon;// karakterin yurume moduna hemen baslamamasi icin kullanilmak
    private KarakterPaketiMovement karakterPaketiMovement; //Karakterin 1sn ligine haraket etmemsi icin kullanilmaktadir

    [Header("Fotografci")]
    private GameObject player;

    [Header("FotografEkleme")]
    [SerializeField] private GameObject tailExampeObject;
    private TailDemo_SegmentedTailGenerator tailGenerator;
    [SerializeField] private GameObject[] fotograflar;

    private WaitForSeconds beklemeSuresei1 = new WaitForSeconds(.25f);



    void Start()
    {
        player = GameObject.FindWithTag("Player");
        karakterPaketiMovement = GameObject.FindObjectOfType<KarakterPaketiMovement>(); 

        StartCoroutine(FotografCek());
        animasyon = player.GetComponent<Animasyon>();
        BaslangicAyarlari();
    }

    public void BaslangicAyarlari() //Oyun tekrar basladiginda buraya gelinir
    {
        if (tailExampeObject.transform.parent.childCount >= 2)
        {
            Destroy(tailExampeObject.transform.parent.transform.GetChild(1).transform.gameObject);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).transform.gameObject);
        }

        GameObject obje = Instantiate(tailExampeObject, tailExampeObject.transform.position, tailExampeObject.transform.rotation);
        obje.transform.parent = tailExampeObject.transform.parent;
        obje.SetActive(true);

        fotografCekilenler.Clear();
        tailGenerator = GameObject.FindObjectOfType<TailDemo_SegmentedTailGenerator>();
        tailGenerator.BaslangicAyarlari();
    }



    IEnumerator FotografCek()
    {
        while (true)
        {
            if (Physics.Raycast(player.transform.position, transform.TransformDirection(Vector3.forward), out hit, 4))
            {
                if (hit.transform.CompareTag("Dansci"))
                {
                    hit.transform.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    Instantiate(fotografEfekt, player.transform.position + Vector3.up * .8f + Vector3.forward * 1.35f, Quaternion.identity).Play();
                    fotografCekilenler.Add(hit.transform.gameObject);
                    animasyon.FotografCek();
                    karakterPaketiMovement.StartCoroutine("SaniyelikDurdur");


                    Dansci dansci = hit.transform.gameObject.GetComponent<Dansci>();
                    FotografEkle(dansci.dansNumarasi);
                }
            }
            yield return beklemeSuresei1;
        }
    }

    public void DanscilariGetir()
    {
        for (int i = 0; i < fotografCekilenler.Count; i++)
        {
            fotografCekilenler[i].SetActive(true);
            fotografCekilenler[i].transform.rotation = Quaternion.Euler(Vector3.up * 90 + Vector3.forward * 90);
            fotografCekilenler[i].transform.position = player.transform.position + Vector3.forward * (i + 1) * 3 - Vector3.up * .95f;

            fotografCekilenler[i].transform.parent = transform.root;
        }
    }


    private IEnumerator Bekleme0(int fotografNumarasi)
    {
        yield return new WaitForSeconds(1);
        Instantiate(fotograflar[fotografNumarasi], player.transform.position, Quaternion.identity);
    }

    public void FotografEkle(int fotografNumarasi)
    {
        GameObject obje = Instantiate(fotograflar[fotografNumarasi], player.transform.position, Quaternion.identity);

        // tailGenerator.SegmentModel = fotograflar[fotografNumarasi];
        tailGenerator.SegmentModel = obje.transform.GetChild(0).transform.gameObject;
        tailGenerator.FotografEkle();
    }
}
