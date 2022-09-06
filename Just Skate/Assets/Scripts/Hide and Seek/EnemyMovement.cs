using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Player;
    public int deltaX = -171;
    public int deltaY = 57;
    public List<Collider2D> colliders;
    public float moveSpeed;
    public Rigidbody2D RB;
    private Vector2 moveDirection;
    public AStar astar = new AStar();
    public GridGenerator Grid;
    public List<Node> path;
    public int counter = 1;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        Grid = new GridGenerator(colliders);
        path = astar.FindPath(Grid.Grid[2, 2], Grid.Grid[2, 50], Grid);
        transform.position = new Vector3(path[0].Xcoord + deltaX, path[0].Ycoord + deltaY, 0);
        foreach (var node in path)
        {
           // transform.position = Vector3.MoveTowards(transform.position, new Vector3(node.Xcoord  +deltaX, node.Ycoord + deltaY, 0), moveSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, moveSpeed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[counter].Xcoord + deltaX, path[counter].Ycoord + deltaY, 0), moveSpeed * Time.deltaTime);
        //if(transform.position == new Vector3(path[counter].Xcoord + deltaX, path[counter].Ycoord + deltaY, 0) && counter < path.Count - 1)
        //{
        //    counter++;
       // }
    }

    private void FixedUpdate()
    {
        
    }

/*    public void MoveToPoint(float Xcoord,float Ycoord)
    {
        int counter = 0;
        Vector3 point = new Vector3(Xcoord, Ycoord , 0);
        float XDirection;
        float YDirection;
        while((transform.position.x > point.x + 3) || (transform.position.x < point.x - 3) || (transform.position.y > point.y + 3) || (transform.position.y < point.y - 3))
        {
            XDirection = Xcoord - transform.position.x;
            YDirection = Ycoord - transform.position.y;
            if (Mathf.Abs(XDirection) > 1 && Mathf.Abs(YDirection) > 1)
            {
                while (Mathf.Abs(XDirection) > 1 && Mathf.Abs(YDirection) > 1)
                {
                    XDirection /= 2;
                    YDirection /= 2;
                }
            }
            else if (Mathf.Abs(XDirection) < 1 && Mathf.Abs(YDirection) < 1)
            {
                while (Mathf.Abs(XDirection) < 1 && Mathf.Abs(YDirection) < 1)
                {
                    XDirection *= 2;
                    YDirection *= 2;
                }
            }
                RB.velocity = new Vector2(XDirection *moveSpeed, YDirection * moveSpeed);
            if((transform.position.x < point.x + 3) && (transform.position.x > point.x - 3) && (transform.position.y < point.y + 3) && (transform.position.y > point.y - 3))
            {
                RB.velocity = new Vector2(0, 0);
                return;
            }
            counter++;
        }
    }*/

}
