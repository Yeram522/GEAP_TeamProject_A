using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleMobSpawner : MonoBehaviour
{
    public List<GameObject> meleeMobTypes;
    public List<GameObject> rangedMobTypes;

    public float intervalSpawnMobAndPlayer = 20.0f;
    
    public int amountMobs = 10;

    // n(RangedMobs) / {n(MeleeMobs) + n(RangedMobs)}
    public float ratioRangedMobs = 0;

    public float spawnRange = 15.0f;
    
    private Dictionary<int, GameObject> Mobs = new Dictionary<int, GameObject>();

    [SerializeField]
    private GameObject[] MobsList;
    
    private Transform playerTransform;

    private float spawnTime = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        GetComponent<Renderer>().enabled = false;
        
        for (int i = 0; i < amountMobs; i++)
        {
            GameObject mob;
            // reason of -1 is that if ratio is 0, this for loop will not activate
            if ((int)(ratioRangedMobs * amountMobs) - 1 >= i)
            {
                mob = Instantiate(
                    rangedMobTypes[Random.Range(0, rangedMobTypes.Count)],
                    new Vector3(0, 0, 0),
                    Quaternion.identity
                ) as GameObject;
                mob.SetActive(false);
                Mobs.Add(mob.GetInstanceID(), mob);
            }
            else
            {
                mob = Instantiate(
                    meleeMobTypes[Random.Range(0, meleeMobTypes.Count)],
                    new Vector3(0, 0, 0),
                    Quaternion.identity
                ) as GameObject;
                mob.SetActive(false);
                Mobs.Add(mob.GetInstanceID(), mob);
            }
        }

        MobsList = Mobs.Values.ToArray();
        
        SpawnMobs();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime <= 3.0f)
            return;
        
        foreach (var mob in Mobs.Values)
        {
            if (!mob.activeSelf)
            {
                SpawnMob(mob);
            }
        }

        spawnTime = 0.0f;
    }

    private void SpawnMobs()
    {
        foreach (GameObject mob in Mobs.Values)
            SpawnMob(mob);
    }

    private void DeactivateMobs()
    {
        foreach (GameObject mob in Mobs.Values)
        {
            Vector3 position = new Vector3(0, 0, 0);
            mob.transform.position = position;
            mob.SetActive(false);
        }
    }

    private void SpawnMob(GameObject mob)
    {
        Vector3 position;
        while (true)
        {
            position = new Vector3(
                transform.position.x + Random.Range(-spawnRange, spawnRange),
                0,
                transform.position.z + Random.Range(-spawnRange, spawnRange)
            );
            if (Vector3.SqrMagnitude(playerTransform.position - position) >
                intervalSpawnMobAndPlayer * intervalSpawnMobAndPlayer)
                break;
        }

        // reference : https://docs.unity3d.com/ScriptReference/Terrain.SampleHeight.html
        position.y = Terrain.activeTerrain.SampleHeight(position) + 0.5f;
        mob.transform.position = position;
        
        ObjectStatus mobStatus = mob.GetComponent<ObjectStatus>();
        mobStatus.HP = mobStatus.MaxHP;
        
        mob.SetActive(true);
    }
}
