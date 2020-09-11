using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExplode : MonoBehaviour    
{

    public GameObject pickupEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp()
    {
        GameObject newPickUpEffect = (GameObject)Instantiate
            (pickupEffect, transform.position, transform.rotation);
        Destroy(newPickUpEffect, 1);
    }
}
