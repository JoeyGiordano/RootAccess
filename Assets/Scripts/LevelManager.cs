using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    #region Singleton
    private static LevelManager _LevelManager;

    public static LevelManager Instance { get { return _LevelManager; } }

    [SerializeField] GameObject terminalObject;
    [SerializeField] Terminal terminal;

    private void Awake()
    {
        if (_LevelManager != null && _LevelManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _LevelManager= this;
        }
    }
    #endregion 

    GameObject player;
    List<string> roomNames = new List<string>();
    List<Room> rooms = new List<Room>();

    // Start is called before the first frame update
    void Start()
    {
        setRoomNames();
        // foreach(string s in roomNames){
        //     print(s);
        // }
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void reinfectRoom(string roomName){
        List<string> availableRooms = CommandManager.Instance.getAvailableRooms();
        if(availableRooms.Count > 0){
            foreach(Room rm in rooms){
                if(rm.getRoomName() == roomName){
                    rm.resetEnemies();
                    string[] message = { "The Virus has reinfected " + roomName };
                    terminalObject.SetActive(true);
                    terminal.addToQueue(message);
                    break;
                }
            }
        }


    }
    //get all rooms and room names into list
    private void setRoomNames(){
        foreach(Transform child in this.transform){
            Room go = child.gameObject.GetComponent<Room>();
            if(go != null){
                rooms.Add(go);
                roomNames.Add(go.getRoomName());
            }
        }
    }
    public void teleport(string roomName){
        foreach(Room rm in rooms){
            if(rm.getRoomName() == roomName){
                player.transform.position = rm.getLocation();
                return;
            }
        }
    }
    public List<string> getRoomNames(){
        return roomNames; 
    }
    public List<Room> getRooms(){
        return rooms;
    }
}
