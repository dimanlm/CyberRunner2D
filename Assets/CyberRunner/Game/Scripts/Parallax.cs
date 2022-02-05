using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    private Vector3 position;
    public GameObject cam;
    public float parallexEffect;
    void Start()
    {
        // startpos = transform.position.x;
        // getting the position of the gameobject
        position = transform.position;
        startpos = position[0];
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float dist = (cam.transform.position.x * parallexEffect);

        // transform.position = new Vector3(startpos + dist, transform.position.z);
        // we'll only change the X position of the gameobject
        position[0] = startpos + dist;
        transform.position = position;
        
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}