using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    public void CreateWalls()
    {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        CreateWall(new Vector2(-halfWidth - 0.5f, 0), new Vector2(1, halfHeight * 2));

        CreateWall(new Vector2(halfWidth + 0.5f, 0), new Vector2(1, halfHeight * 2));

        CreateWall(new Vector2(0, halfHeight + 0.5f), new Vector2(halfWidth * 2, 1));
        
        CreateWall(new Vector2(0, -halfHeight - 0.5f), new Vector2(halfWidth * 2, 1));
    }

    private void CreateWall(Vector2 pos, Vector2 size)
    {
        GameObject wall = new GameObject("Wall");
        wall.transform.position = pos;

        var col = wall.AddComponent<BoxCollider2D>();
        col.size = size;

        if (wall.transform.position.y < 0)
        {
            wall.tag = "BottomWall";
            col.isTrigger = true;
        }
    }

}
