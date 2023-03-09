using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] points;

    public Tilemap flowerTilemap;
    public TileBase flower;
    public GameObject finish;
    public int width, height;

    private int lastRand;

    private void Awake() {
        FlowerSet();
        FinishSet();
    }

    private void FlowerSet() {
        int rond = Random.Range(3, 5);

        for(int y = -((height / rond) - 1); y < ((height / rond) - 1); y++) {
            int rand = Random.Range(-(width / 2), (width / 2));
            rand = lastRand == rand ? Random.Range(-(width / 2), (width / 2)) : rand;
            lastRand = rand;

            Vector3Int setPos = new Vector3Int(rand, y * rond);
            flowerTilemap.SetTile(setPos, flower);
        }
    }

    private void FinishSet() {
        int rand = Random.Range(0, points.Length);
        finish.transform.position = points[rand].transform.position;
        points[rand].tag = "Finish";
    }
}
