using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bu class'da // ile isaretledigim satýrlarý kendim ekledim

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;

    //[SerializeField] private Color defaultColor;//
    //[SerializeField] private Color selectedColor;//

    //private MeshRenderer objectRenderer;//

    //singleton instance'ýna eriþmek icin start (Player'daki awake ile singleton object set edilir önce)
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;

        //foreach (GameObject visualGameObject in visualGameObjectArray)
        //{
            //objectRenderer = visualGameObject.GetComponent<MeshRenderer>();//
            // Baþlangýçta objenin rengini defaultColor olarak ayarla
            //objectRenderer.material.color = defaultColor;//
        //}

    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        //visualGameObject.SetActive(true); //bu satir gameobject'in game ekranýnda gözükmesini sagliyor 
        //visualGameObject.GetComponent<Renderer>().enabled = true; //bu satir gameobject'in game ekranýnda gözükmesini sagliyor
        // Eðer Player, bu obje ile etkileþime geçtiyse rengi koyulaþtýr
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            //objectRenderer.material.color = selectedColor;//
            visualGameObject.SetActive(true);
        }

    }

    private void Hide()
    {
        //visualGameObject.SetActive(false); //bu satir gameobject'in game ekranýnda saklanmasýný sagliyor
        //visualGameObject.GetComponent<Renderer>().enabled = false;  //bu satir gameobject'in game ekranýnda saklanmasýný sagliyor
        // Eðer Player bu obje ile etkileþime geçmediyse rengi açýk býrak
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            //objectRenderer.material.color = defaultColor;//
            visualGameObject.SetActive(false);

        }

    }
}
