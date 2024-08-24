using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    MapController mc;
    public GameObject targetMap;

    void Start()
    {
        mc = FindObjectOfType<MapController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            mc.currentChunks = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(mc.currentChunks == targetMap)
            {
                mc.currentChunks = null;
            }
        }
    }
}
