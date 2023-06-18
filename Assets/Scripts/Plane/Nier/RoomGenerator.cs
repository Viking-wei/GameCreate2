using System.Collections.Generic;
using UnityEngine;


public class RoomGenerator : MonoBehaviour
{
    public enum Direction {up,down,left,right};
    public Direction direction;

    [Header("������Ϣ")]
    public GameObject roomPrefab;
    public static int roomNumber = 7;
    public Color startColor, endColor;
    private GameObject endRoom;
    public GameObject endDoor;

    [Header("λ�ÿ���")]
    public Transform generatorPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;
    public int maxStep;

    public List<Room>rooms= new List<Room>();

    List<GameObject>farRooms= new List<GameObject>();
    List<GameObject>lessFarRooms= new List<GameObject>();
    List<GameObject>oneWayRooms= new List<GameObject>();

    public WallType wallType;
    private void Start()
    {
        roomNumber += 1;
        for(int i = 0;i<roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab,generatorPoint.position,Quaternion.identity).GetComponent<Room>());

            //�ı�pointλ��
            ChangePointPos();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startColor;


        endRoom = rooms[0].gameObject;
        foreach(var room in rooms)
        {
            SetupRoom(room, room.transform.position);
        }

        FindEndRoom();
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
        //PoolManager.Release(endDoor, DefinedPointInBounds(endRoom.GetComponent<Collider2D>().bounds));
        Instantiate(endDoor,DefinedPointInBounds(endRoom.GetComponent<Collider2D>().bounds),Quaternion.identity);
    }

    //private void Update()
    //{
    //    if(Input.anyKeyDown)
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //    }
    //}

    public Vector3 DefinedPointInBounds(Bounds bounds)
    {
        return new Vector3(
            (bounds.min.x+3),
            (bounds.min.y+5),
            (bounds.min.z)
        );
    }
    public void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);
            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffset, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position,0.2f,roomLayer));
    }

    public void SetupRoom(Room newRoom,Vector3 roomPosition)
    {
        newRoom.roomUp=Physics2D.OverlapCircle(roomPosition+new Vector3(0,yOffset,0),0.2f,roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);

        newRoom.UpdateRoom(xOffset,yOffset);

        switch (newRoom.doorNumber)
        {
            case 1:
                if (newRoom.roomUp)
                    Instantiate(wallType.singleUp, roomPosition, Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(wallType.singleBottom, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft)
                    Instantiate(wallType.singleLeft, roomPosition, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(wallType.singleRight, roomPosition, Quaternion.identity);
                break;
            case 2:
                if (newRoom.roomUp&&newRoom.roomLeft)
                    Instantiate(wallType.doubleLU, roomPosition, Quaternion.identity);
                if (newRoom.roomDown&&newRoom.roomLeft)
                    Instantiate(wallType.doubleLB, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomLeft)
                    Instantiate(wallType.doubleLR, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomRight)
                    Instantiate(wallType.doubleUR, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomRight)
                    Instantiate(wallType.doubleRB, roomPosition, Quaternion.identity);
                if (newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.doubleUB, roomPosition, Quaternion.identity);
                break;
            case 3:
                if (newRoom.roomUp&&newRoom.roomLeft&&newRoom.roomRight)
                    Instantiate(wallType.tripleLUR, roomPosition, Quaternion.identity);
                if (newRoom.roomDown&&newRoom.roomLeft&&newRoom.roomRight)
                    Instantiate(wallType.tripleLRB, roomPosition, Quaternion.identity);
                if (newRoom.roomLeft && newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.tripleLUB, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomUp && newRoom.roomDown)
                    Instantiate(wallType.tripleURB, roomPosition, Quaternion.identity);
                break;
            case 4:
                if(newRoom.roomUp && newRoom.roomLeft && newRoom.roomRight&&newRoom.roomDown)
                    Instantiate(wallType.fourDoors,roomPosition, Quaternion.identity);
                break;
        }
    }

    public void FindEndRoom() 
    {
        for(int i = 0;i<rooms.Count;i++)
        {
            if (rooms[i].stepToStart>maxStep) 
                maxStep= rooms[i].stepToStart;
        }

        foreach(var room in rooms)
        {
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
            if (room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room.gameObject);
        }

        for(int i = 0;i<farRooms.Count;i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);
        }
        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessFarRooms[i]);
        }

        if(oneWayRooms.Count!=0)
        {
            endRoom = oneWayRooms[Random.Range(0,oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0,farRooms.Count)];
        }
    }
}
[System.Serializable]
public class WallType
{
    public GameObject singleLeft, singleRight, singleUp, singleBottom,
                      doubleLU, doubleLR, doubleLB, doubleUR, doubleUB, doubleRB,
                      tripleLUR, tripleLUB, tripleURB, tripleLRB,
                      fourDoors;
}
