using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{

    [SerializeField] private Image barImage; // cutting counter uzerindeki cutting process bar ile iliþkilendiriyoruz
    [SerializeField] private GameObject hasProgressGameObject; //cutting counter referansý
    //GameObject olarak döndüðünden bu object'in IHasProgress'e sahip bir component'i olmayabilir 
    //so start fonksiyonunda bunu kontrol ederiz 



    private IHasProgress hasProgress;


    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError("Game object " + hasProgressGameObject + "does not have a component that implements IHasProgress");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        //Bar emptyken veya full iken hide, diger türlü show 
        if(e.progressNormalized == 0f || e.progressNormalized == 1f ) 
        { 
            Hide();
        }else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }




}
