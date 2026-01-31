using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;
    public float RangeX = 15.0f;
    public float RangeZ = 15.0f;
    [SerializeField] private BallShooter ballShooter;

    private int Count = 0;
    public bool scored = false;

    private void Start()
    {
        Count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        scored = true;

        Count += 1;
        CounterText.text = "Count : " + Count;

        ballShooter.availableBalls = 3;

        transform.position = new Vector3(UnityEngine.Random.Range(-RangeX, RangeX), 0f, UnityEngine.Random.Range(-RangeZ, RangeZ));
    }
}
