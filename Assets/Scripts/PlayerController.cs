using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public int collectibleDegeri;
    public bool xVarMi = true;
    public bool collectibleVarMi = true;


    [Header("AyarDuzenleyiciComponentler")]
    private FotografController fotografController;
    private Animasyon animasyon;
    private KarakterPaketiMovement karakterPaketiMovement;

    [Header("ResimAyarlari")]
    private TailDemo_SegmentedTailGenerator tailDemo_SegmentedTailGenerator;

    [Header("Animasyon")]
    private Animator anim;

    private void Awake()
    {
        if (instance == null) instance = this;
        //else Destroy(this);

        fotografController = GameObject.FindObjectOfType<FotografController>();
        animasyon = GameObject.FindObjectOfType<Animasyon>();
        karakterPaketiMovement = GameObject.FindObjectOfType<KarakterPaketiMovement>();
        anim = animasyon.transform.gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    void Start()
    {
        StartingEvents();
    }

    /// <summary>
    /// Playerin collider olaylari.. collectible, engel veya finish noktasi icin. Burasi artirilabilir.
    /// elmas icin veya baska herhangi etkilesimler icin tag ekleyerek kontrol dongusune eklenir.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("collectible"))
        {
            // COLLECTIBLE CARPINCA YAPILACAKLAR...
            GameController.instance.SetScore(collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku

        }
        else if (other.CompareTag("engel"))
        {
            // ENGELELRE CARPINCA YAPILACAKLAR....
            /*  GameController.instance.SetScore(-collectibleDegeri); // ORNEK KULLANIM detaylar icin ctrl+click yapip fonksiyon aciklamasini oku
              if (GameController.instance.score < 0) // SKOR SIFIRIN ALTINA DUSTUYSE
              {
                  // FAİL EVENTLERİ BURAYA YAZILACAK..
                  GameController.instance.isContinue = false; // çarptığı anda oyuncunun yerinde durması ilerlememesi için
                  UIController.instance.ActivateLooseScreen(); // Bu fonksiyon direk çağrılada bilir veya herhangi bir effect veya animasyon bitiminde de çağrılabilir..
                  // oyuncu fail durumunda bu fonksiyon çağrılacak.. 
              }*/

            tailDemo_SegmentedTailGenerator.ResimDusur(); //Eğer 3 veya daha fazla resim varsa yeni resim olusturulur
            anim.SetTrigger("SavrulmaP");

            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        }
        else if (other.CompareTag("finish"))
        {
            animasyon.OyunSonuAyari();
            karakterPaketiMovement.kosuDurumu = false;

            // finishe collider eklenecek levellerde...
            // FINISH NOKTASINA GELINCE YAPILACAKLAR... Totalscore artırma, x işlemleri, efektler v.s. v.s.
            GameController.instance.isContinue = false;
            GameController.instance.ScoreCarp(7);  // Bu fonksiyon normalde x ler hesaplandıktan sonra çağrılacak. Parametre olarak x i alıyor. 
            // x değerine göre oyuncunun total scoreunu hesaplıyor.. x li olmayan oyunlarda parametre olarak 1 gönderilecek.
            StartCoroutine(OyunSonuEkraniniGeciktirir());
            // normal de bu kodu x ler hesaplandıktan sonra çağıracağız. Ve bu kod çağrıldığında da kazanılan puanlar animasyonlu şekilde artacak..

            tailDemo_SegmentedTailGenerator.BolumBitir();
        }
    }

    IEnumerator OyunSonuEkraniniGeciktirir()
    {
        yield return new WaitForSeconds(.5f);
        UIController.instance.ActivateWinScreen(); // finish noktasına gelebildiyse her türlü win screen aktif edilecek.. ama burada değil..
    }


    /// <summary>
    /// Bu fonksiyon her level baslarken cagrilir. 
    /// </summary>
    public void StartingEvents()
    {
        transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.parent.transform.position = Vector3.zero;
        GameController.instance.isContinue = false;
        GameController.instance.score = 0;
        transform.position = new Vector3(0, transform.position.y, 0);
        GetComponent<Collider>().enabled = true;

        YeniTaileEris();
    }

    public void YeniTaileEris()
    {
        tailDemo_SegmentedTailGenerator = GameObject.FindObjectOfType<TailDemo_SegmentedTailGenerator>();
    }

}
