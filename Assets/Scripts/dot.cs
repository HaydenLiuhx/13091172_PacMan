﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dot : MonoBehaviour
{
    // Start is called before the first frame update
    

    // Update is called once per frame 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Pacm")
        {
            collision.gameObject.GetComponent<Pacman>().curnum += 1;
            Destroy(gameObject);
        }
        
    }
}
