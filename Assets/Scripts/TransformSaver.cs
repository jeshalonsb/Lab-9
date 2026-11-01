using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class TransformSaver : MonoBehaviour, ISaveable
{
    public Transform player;

    private string filePath;

    public TargetSpawner spawner;

    public ScoreManager scoreManager;

    private string scoreFilePath;

    [System.Serializable]
    public class SaveData
    {
        public Vector3 playerPosition;

        public List <TargetData> targets = new List <TargetData> ();
    }
    [System.Serializable]
    public class TargetData
    {
        public Vector3 position;
        public int points;
        public float size;
        public bool isMoving;
        public float moveSpeed;
        public float moveDistance;
    }
    public void Awake()
    {
        filePath = Application.persistentDataPath + "/positions.json";
        scoreFilePath = Application.persistentDataPath + "/score.dat";
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
            Debug.Log("Saved Game");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
            Debug.Log("Game Loaded");
        }
    }
    public void Save()
    {
        SaveData data = new SaveData();

        data.playerPosition = player.position;

        foreach (GameObject targetObject in spawner.spawnTarget)
        {
            if (targetObject == null)
                continue;

            Target target = targetObject.GetComponent<Target>();

            TargetData targetData = new TargetData ();

            targetData.position = targetObject.transform.position;
            targetData.points = target.points;
            targetData.size = targetObject.transform.localScale.x;
            targetData.isMoving = target.isMoving;
            targetData.moveSpeed = target.moveSpeed;
            targetData.moveDistance = target.moveDistance;

            data.targets.Add (targetData);
        }

        Debug.Log("Targets being saved: " + data.targets.Count);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

        using (BinaryWriter writer = new BinaryWriter(File.Open(scoreFilePath, FileMode.Create)))
        {
            writer.Write(scoreManager.GetScore());
        }

    }
    public void Load()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            player.position = data.playerPosition;

            spawner.RestoreTargets(data.targets);

        if (File.Exists(scoreFilePath))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(scoreFilePath, FileMode.Open)))
            {
                scoreManager.SetScore(reader.ReadInt32());
            }

        }
        
    }
}
