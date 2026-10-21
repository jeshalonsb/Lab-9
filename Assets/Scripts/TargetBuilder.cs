using UnityEngine;

public class TargetBuilder 
{
    private GameObject targetPrefab;
    private Vector3 position;

    private int points = 10;
    private float size = 1f;
    private float moveSpeed = 3f;
    private float moveDistance = 2f;
    private bool isMoving = false;

    public TargetBuilder(GameObject prefab, Vector3 spawnPosition)
    {
        targetPrefab = prefab;
        position = spawnPosition;
    }
    public TargetBuilder SetPoints(int value)
    {
        points = value;
        return this;
    }
    public TargetBuilder SetSize(float value)
    {
        size = value; 
        return this;
    }
    public TargetBuilder SetMovement(bool moving, float speed = 3f, float distance = 2f)
    {
        isMoving = moving;
        moveSpeed = speed;
        moveDistance = distance;
        return this;
    }
    public GameObject Build()
    {
        GameObject target = Object.Instantiate(targetPrefab, position, Quaternion.identity);

        Target targetScript = target.GetComponent<Target>();

        targetScript.points = points;
        targetScript.isMoving = isMoving;
        targetScript.moveSpeed = moveSpeed;
        targetScript.moveDistance = moveDistance;

        target.transform.localScale = Vector3.one * size;
        target.transform.position += Vector3.back * (size / 2f);

        return target;
    }
}
