using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunks;
    PlayerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpdist; //
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;

    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if(!currentChunks)
        {
            return;
        }

        if(pm.moveDir.x > 0 && pm.moveDir.y == 0) //오른쪽으로 갈시
        {
            if(!Physics2D.OverlapCircle(currentChunks.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunks.transform.Find("Right").position;
                SpwanChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0) //왼쪽으로 갈시
        {
            if (!Physics2D.OverlapCircle(currentChunks.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunks.transform.Find("Left").position;
                SpwanChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y > 0) //위쪽으로 갈시
        {
            if (!Physics2D.OverlapCircle(currentChunks.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunks.transform.Find("Up").position;
                SpwanChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y < 0) //아래쪽으로 갈시
        {
            if (!Physics2D.OverlapCircle(currentChunks.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunks.transform.Find("Down").position;
                SpwanChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0) //오른쪽 위 대각선으로 갈시
        {
            if (!Physics2D.OverlapCircle(currentChunks.transform.Find("Right Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunks.transform.Find("Right Up").position;
                SpwanChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0) //오른쪽 아래 대각선으로 갈시
        {
            if (!Physics2D.OverlapCircle(currentChunks.transform.Find("Right Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunks.transform.Find("Right Down").position;
                SpwanChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0) //왼쪽 위 대각선으로 갈시
        {
            if (!Physics2D.OverlapCircle(currentChunks.transform.Find("Left Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunks.transform.Find("Left Up").position;
                SpwanChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0) //왼쪽 아래 대각선으로 갈시
        {
            if (!Physics2D.OverlapCircle(currentChunks.transform.Find("Left Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunks.transform.Find("Left Down").position;
                SpwanChunk();
            }
        }
    }

    void SpwanChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if(optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }

        foreach(GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpdist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
