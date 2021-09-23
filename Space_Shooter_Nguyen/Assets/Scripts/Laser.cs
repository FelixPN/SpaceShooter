using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable and set to 8 value
    private float _speed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate (move) the laser up
        transform.Translate (Vector3.up * _speed * Time.deltaTime);

        //if the laser is at a 8 or higher y value then destroy the laser
        if (transform.position.y >= 8.0f)
        {
            Destroy(this.gameObject);
        }
        
    }
}
