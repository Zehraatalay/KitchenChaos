using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlatesCounterVisual : MonoBehaviour
{

    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    [SerializeField] private PlatesCounter platesCounter;


    //tabaklarýn üst üste yýðýlmasý visual bir durum oldugundan visual script'ine yazýyoruz
    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = .1f;//tabaklar arasýndaki mesafe
        //kac tane tabak olduguna baðlý olarak visual'ýn deðiþmesi lazým
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject); //plates list'ine tabak ekler

    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);

    }




}
