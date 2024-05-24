using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHasProgress 
{
    //ProgressBarUI classýnda listen + bar davranýþý gerçekleþtiriliyor
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }



}
