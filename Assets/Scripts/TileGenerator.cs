using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public Transform[] points;

    public static TileGenerator instance;

    public Tilemap tilemap, fenceTilemap, flowerTilemap;
    public TileBase flower, fence;
    public RuleTile grass;
    public GameObject finish, player;
    public Transform pointGroup;
    public int width, height;
    public int deleteWidth, deleteHeight;

    private int lastRand;

    private void Awake() {
        instance = this;
        TileSet();
    }

    public void TileSet() {
        Delete();
        GrassSet();
        FenceSet();
        FlowerSet();
        FinishSet();
        PointSet();
    }

    public void Delete() {
        for (int y = -((deleteHeight / 2) + 1); y < ((deleteHeight / 2) + 1); y++) {
            for (int x = -((deleteWidth / 2) + 1); x < ((deleteWidth / 2) + 1); x++) {
                Vector3Int setPos = new Vector3Int(x, y);
                tilemap.SetTile(setPos, null);
                fenceTilemap.SetTile(setPos, null);
                flowerTilemap.SetTile(setPos, null);
            }
        }
    }

    private void GrassSet() {
        for (int y = -(height / 2); y < (height / 2); y++) {
            for (int x = -(width / 2); x < (width / 2); x++) {
                Vector3Int setPos = new Vector3Int(x, y);
                tilemap.SetTile(setPos, grass);
            }
        }
    }

    private void FenceSet() {
        for (int y = -((height / 2) + 1); y < ((height / 2) + 1); y++) {
            for (int x = -((width / 2) + 1); x < ((width / 2) + 1); x++) {
                Vector3Int setPos = new Vector3Int(x, y);
                if(!tilemap.HasTile(setPos)) {
                    fenceTilemap.SetTile(setPos, fence);
                }
            }
        }
    }

    private void FlowerSet() {
        for (int y = -(height / 2); y < (height / 2); y++) {
            int doRand = Random.Range(0, 6);

            if(doRand == 1) {
                int rand = Random.Range(-(width / 2), (width / 2));

                if(!(lastRand == rand || (lastRand-1) == rand || (lastRand+1) == rand)) {
                    lastRand = rand;

                    Vector3Int setPos = new Vector3Int(rand, y);
                    flowerTilemap.SetTile(setPos, flower);
                } else {
                    rand = Random.Range(0, width);
                }
            }
        }
    }

    private void PointSet() {
        for(int i = 0; i < points.Length; i++) {
            string pointName = points[i].name;

            Vector2 pos = new Vector3(width / 2, height / 2);
            Vector2 finishScale = new Vector3(finish.transform.localScale.x / 2, finish.transform.localScale.y / 2);

            float setPosX = 0;
            float setPosY = 0;
            float scale = 5;

            char nameStart = pointName[0];
            if(nameStart.ToString() == "R") setPosX += pos.x - scale;
            else if(nameStart.ToString() == "L") setPosX -= pos.x - scale;

            char nameEnd = pointName[1];
            if(nameEnd.ToString() == "U") setPosY += pos.y - scale;
            else if(nameEnd.ToString() == "D") setPosY -= pos.y - scale;

            Vector3 setPos = new Vector3(setPosX, setPosY);
            points[i].transform.position = setPos;
        }
    }

    private void FinishSet() {
        int rand = Random.Range(0, points.Length);
        finish.transform.position = points[rand].position;
        player.transform.position = points[rand].position;
    }
}
 