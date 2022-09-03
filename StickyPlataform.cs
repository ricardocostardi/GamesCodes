using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlataform : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
             Debug.Log ("Collided");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
             Debug.Log ("Not Collided");
        }
    }
}
