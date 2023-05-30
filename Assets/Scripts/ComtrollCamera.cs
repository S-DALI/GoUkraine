using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComtrollCamera : MonoBehaviour
{
    [SerializeField]  private Transform PlayerPos;
    [SerializeField] private Vector3 distance;
    void Start()
    {
        distance = transform.position - PlayerPos.position;
    }

    void FixedUpdate()
    {
        Vector3 NewPos = new Vector3(transform.position.x, distance.y+PlayerPos.position.y, distance.z + PlayerPos.position.z);
        transform.position = NewPos;
    }
    private void Update()
    {
        Vector3 NewPosX = new Vector3(PlayerPos.position.x, transform.position.y, transform.position.z);
        transform.position = NewPosX;
    }
}
