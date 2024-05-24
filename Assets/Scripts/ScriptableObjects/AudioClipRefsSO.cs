using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bu classýn olusturulma amacý tüm ses efektlerinin bir arada yer almasýdýr 
//farklý versiyonlarý olan ses efektleri için array oluþturulmuþtur 


[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footstep;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip stoveSizzle; 
    public AudioClip[] trash;
    public AudioClip[] warning;




}
