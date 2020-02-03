using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Varibales
    Node[,] grid;

    [SerializeField]
    float xzScale = 1.5f;
    [SerializeField]
    float yScale = 2;
    Vector3 minPos;

    int x;
    int y;
    int z;

    List<Vector3> nodeGizmo = new List<Vector3>();
    public Vector3 extends = new Vector3(.8f, .8f, .8f);

    #endregion

    private void Start()
    {
        ReadLevel();
    }

    void ReadLevel()
    {
        GridPosition[] gridPositions = GameObject.FindObjectsOfType<GridPosition>();

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minZ = minX;
        float maxZ = maxX;

        for (int i = 0; i < gridPositions.Length; i++)
        {
            Transform transform = gridPositions[i].transform;

            #region Read Positions

            if (transform.position.x < minX)
            {
                minX = transform.position.x;
            }
            if (transform.position.x > maxX)
            {
                maxX = transform.position.x;
            }


            if (transform.position.z < minZ)
            {
                minZ = transform.position.z;
            }
            if (transform.position.z > maxZ)
            {
                maxZ = transform.position.z;
            }

            #endregion
        }

        int posX = Mathf.FloorToInt((maxX - minX) / xzScale);
        int posZ = Mathf.FloorToInt((maxZ - minZ) / xzScale);

        minPos = Vector3.zero;
        minPos.x = minX;
        minPos.z = minZ;

        CreateGrid(posX, posZ);
    }

    void CreateGrid(int posX, int posZ)
    {
        grid = new Node[posX, posZ];

        for (int x = 0; x < posX; x++)
        {
            for (int z = 0; z < posZ; z++)
            {
                Node n = new Node();
                n.x = x; ;
                n.z = z;

                Vector3 tp = minPos;
                tp.x += x * xzScale + .5f;
                tp.z += z * xzScale + .5f;
                n.worldPosition = tp;

                nodeGizmo.Add(n.worldPosition);

                grid[x, z] = n;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < nodeGizmo.Count; i++)
        {
            Gizmos.DrawWireCube(nodeGizmo[i], extends);
        }
    }

    public void CreateGrid()
    {
        Node[,] freshGrid = new Node[0, 0];
        nodeGizmo.Clear();
        ReadLevel();
    }
}
