using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed ;
    public float playerSensitivity;
    public float horizontalMovement ;
    public float verticalMovement ;
    public bool isScore;//score value doesnt increase if false//
    public bool isAlive;//player wont move if false//
    public bool isBotBound;//gravity wont work on player if true//
    private Touch touch;
    public GameObject tutorial1;
    Vector3 moveDirection;
    Rigidbody rb;
    public GameObject top,bot,left,right;
    float maxy,miny,maxx,minx;
    public void dead()
    {
        isAlive = false;
    }
    void Start()
    {
        //determining the max boundaries of the player//
        maxy = top.transform.position.y;
        maxx = right.transform.position.z;
        miny = bot.transform.position.y;
        minx = left.transform.position.z;
    
        isScore = false;
        isAlive = true;
        isBotBound = false;
        if(isAlive)
        {
            transform.gameObject.GetComponent<Rigidbody>().useGravity =false;
            rb = GetComponent<Rigidbody>();          
        }
    
    }
    
    void Update()
    {//--------------------------------INPUT AND MOVEMENT LOGIC--------------------------------------------//
        if(isAlive )
        {
           transform.position = new Vector3(transform.position.x + moveSpeed  , transform.position.y , transform.position.z );
           
            if(Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    isScore = true;
                    rb.drag = 6;
                    tutorial1.SetActive(false);
                    isBotBound = false;
                }
                if(touch.phase == TouchPhase.Ended )
                {
                    transform.gameObject.GetComponent<Rigidbody>().useGravity =true;
                    rb.drag = 0;
                    isScore = false;
                }
                if(touch.phase == TouchPhase.Stationary)
                {
                    transform.gameObject.GetComponent<Rigidbody>().useGravity =false;
                    
                }
                if(touch.phase == TouchPhase.Moved )
                {
                    transform.gameObject.GetComponent<Rigidbody>().useGravity =false;
                    transform.position = new Vector3(transform.position.x , Mathf.Clamp(transform.position.y + touch.deltaPosition.y  *playerSensitivity,miny,maxy),Mathf.Clamp(transform.position.z + touch.deltaPosition.x *-1* playerSensitivity,maxx,minx) );
                    
                    isBotBound = false;
                }
                if(isBotBound)
                {
                    transform.gameObject.GetComponent<Rigidbody>().useGravity =false;
                    
                }
                
            } 
            
        }
        
    }
    
}
