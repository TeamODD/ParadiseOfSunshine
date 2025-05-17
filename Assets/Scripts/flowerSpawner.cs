using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowerSpawner : MonoBehaviour
{
    public static flowerSpawner Instance;
    public List<FlowerData> flowerDatas;
    public GameObject flowerPrefab;
    public List<Transform> spawnPoints;
    private bool[] isOccupied;
    private bool MariIsOccupied;
    public Transform spawnMariPoint;
    public GameObject startFlower;

    bool isStarted = false;
    bool isMariStarted = false;
    public float cooltime = 30f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOccupied = new bool[spawnPoints.Count];
        MariIsOccupied = false;
        Instance = this;
        //initspawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(startFlower == null && !isStarted)
        {
            initspawn();
            isStarted = true;
        }
        if (InventoryManager.Instance.isGiven[InventoryManager.Instance.bouquetDatas[0]] == true &&
            InventoryManager.Instance.isGiven[InventoryManager.Instance.bouquetDatas[1]] == true &&
            !isMariStarted)
        {
            spawnMari();
            isMariStarted = true;
        }
    }
    void spawnFlower(Transform point)
    {
        if (isOccupied[spawnPoints.IndexOf(point)] == true)
            return;
        int randomDataIndex = Random.Range(0, flowerDatas.Count - 1);
        GameObject newFlower = Instantiate(flowerPrefab, point);
        float isPoison = Random.Range(0f, 1f);
        if(isPoison < 0.35f)
        {
            newFlower.GetComponent<Flower>().isPoison = true;
        }
        else
        {
            newFlower.GetComponent<Flower>().isPoison = false;
        }
        newFlower.GetComponent<Flower>().ApplyData(flowerDatas[randomDataIndex]);
        isOccupied[spawnPoints.IndexOf(point)] = true;
    }
    void spawnMari()
    {
        if (MariIsOccupied == true)
            return;
        GameObject newFlower = Instantiate(flowerPrefab, spawnMariPoint);
        newFlower.GetComponent<Flower>().ApplyData(flowerDatas[6]);
        MariIsOccupied = true;
    }
    void initspawn()
    {
        foreach(var point in spawnPoints)
        {
            if (point == spawnPoints[8])
            {
                StartCoroutine(RespawnCool(point));
                continue;
            }
            spawnFlower(point);
        }
    }
    public void NotifyNull(Transform position)
    {
        if (spawnMariPoint.name == position.name)
        {
            MariIsOccupied = false;
            StartCoroutine(RespawnMari());
            return;
        }
        foreach (var point in spawnPoints)
        {
            if(point.name == position.name)
            {
                isOccupied[spawnPoints.IndexOf(point)] = false;
                StartCoroutine(RespawnCool(point));
                break;
            }
        }
    }
    IEnumerator RespawnCool(Transform point)
    {
        yield return new WaitForSeconds(cooltime);
        spawnFlower(point);
    }
    IEnumerator RespawnMari()
    {
        yield return new WaitForSeconds(cooltime);
        spawnMari();
    }
}
