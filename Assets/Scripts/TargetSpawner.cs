using UnityEngine;
using System.Collections.Generic;
public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    
    public float spawnInterval = 2f;
    public Vector2 spawnRangeX = new Vector2(-5, 5f);
    public Vector2 spawnRangeY = new Vector2(-5, 5f);

    public int maxTargets = 5;

    public List<GameObject> spawnTarget = new List<GameObject>();

    private float nextSpawnTime;
    private int targetCount;

    private void OnEnable()
    {
        Target.OnTargetHit += TargetHit;
    }
    private void OnDisable()
    {
        Target.OnTargetHit -= TargetHit;
    }
    private void Update()
    {
        if (Time.time >= nextSpawnTime && targetCount < maxTargets)
        {
            SpawnTarget();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }
    private void SpawnTarget()
    {
        Vector3 position = transform.position + new Vector3(transform.position.x + Random.Range(spawnRangeX.x, spawnRangeX.y), transform.position.y + Random.Range(spawnRangeY.x, spawnRangeY.y), 0f);

        TargetBuilder builder = new TargetBuilder(targetPrefab, position);

        switch (Random.Range(0, 3))
        {
            case 0:
                builder.SetPoints(10).SetSize(1f);
                break;
           
            case 1:
                builder.SetPoints(20).SetSize(0.5f);
                break;

            case 2:
                builder.SetPoints(30).SetSize(1f)
                       .SetMovement(true, 2f, 1f);
                break;

        }

        GameObject target = builder.Build();
        spawnTarget.Add(target);
        targetCount++;

    }
    private void TargetHit(int points)
    {
        targetCount--;
    }
    public void RestoreTargets(List<TransformSaver.TargetData> savedTargets)
    {
        foreach (GameObject target in spawnTarget)
        {
            if (target != null)
                Destroy(target);
        }

        spawnTarget.Clear(); 
        targetCount = 0;

        foreach (TransformSaver.TargetData data in savedTargets)
        {
            TargetBuilder builder = new TargetBuilder(targetPrefab, data.position);

            builder.SetPoints(data.points).SetSize(data.size).SetMovement(data.isMoving, data.moveSpeed, data.moveDistance);

            GameObject target = builder.Build();

            target.transform.position = data.position;

            spawnTarget.Add(target);
            targetCount++;
        }

        nextSpawnTime = Time.time + spawnInterval;
    }
}
