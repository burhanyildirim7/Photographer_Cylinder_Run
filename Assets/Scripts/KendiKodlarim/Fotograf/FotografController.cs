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
    private Animasyon animasyon;

    [Header("Fotografci")]
    private GameObject player;


    private WaitForSeconds beklemeSuresei1 = new WaitForSeconds(.25f);



    void Start()
    {
        player = GameObject.FindWithTag("Player");

        StartCoroutine(FotografCek());
        animasyon = player.GetComponent<Animasyon>();
    }

    public void BaslangicAyarlari()
    {
        fotografCekilenler.Clear();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void DanscilariGetir()
    {
        Debug.Log(fotografCekilenler.Count);
        for (int i = 0; i < fotografCekilenler.Count; i++)
        {
            fotografCekilenler[i].SetActive(true);
            fotografCekilenler[i].transform.rotation = Quaternion.Euler(Vector3.up * 180);
            fotografCekilenler[i].transform.position = player.transform.position + Vector3.forward * (i + 1) * 3;
        }
    }

    IEnumerator FotografCek()
    {
        while (true)
        {
            if (Physics.Raycast(player.transform.position, transform.TransformDirection(Vector3.forward), out hit, 4))//Layer kullanýlýyorsa sontrafa virgül koyulup layerMask yazýlmaýlýdr.
            {
                if (hit.transform.CompareTag("Dansci"))
                {
                    Instantiate(fotografEfekt, player.transform.position + Vector3.up * 1.25f + Vector3.forward * 2f, Quaternion.identity).Play();
                    hit.transform.gameObject.SetActive(false);
                    fotografCekilenler.Add(hit.transform.gameObject);
                    animasyon.FotografCek();
                }
            }
            yield return beklemeSuresei1;
        }
    }
}
