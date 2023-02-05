using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    //different statuses of the room:
    //1. unvisited: the player has not yet reached this room
    //2. infected: the player has just visited this room or the virus re-infects this room.
    //3. cleared: the player has killed all of the enemies in the room.

    //healing rooms should not be able to be infected
    public enum RoomStatus{
        unvisited,
        infected,
        cleared,

    }

    //name of current room
    [SerializeField]
    private string roomName = "";

    
    //list of rooms connected to current room; current_room -> next_room
    [SerializeField]
    private List<Room> roomConnections = new List<Room>();

    //layout or type of room; enemy, healing, root
    [SerializeField]
    private string layout;

    private RoomStatus roomStatus = RoomStatus.unvisited;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // if(roomName == "src"){
        //     playerIsInRoom();
        // }
    }
    public string getRoomName(){
        return roomName;
    }
    public Vector3 getLocation(){
        return transform.position;
    }
    public void setRoomStatus(RoomStatus status){
        roomStatus = status;
        if(status == RoomStatus.cleared){
            CameraController.Instance.setPlayerPositionState(CameraController.PlayerPositionState.PlayerIsInHallway, player.transform);
        }
    }

    private bool playerIsInRoom(){
        return false;
    }
    private void OnTriggerEnter2D(Collider2D other){
        // print(roomStatus);
        if(other.gameObject == player.gameObject){
            print("entering room");
            print("game status: " + roomStatus.ToString());
            foreach(Transform child in transform){
                Spawner spawner = child.gameObject.GetComponent<Spawner>();
                    if(spawner != null){
                        if(roomStatus != RoomStatus.cleared){   
                            spawner.activate();
                        }
                        else{
                            spawner.deactivate();
                        }
                }
            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D other){
        if(Input.GetKey(KeyCode.L)){
            setRoomStatus(RoomStatus.cleared);
        }
    }
    
}
