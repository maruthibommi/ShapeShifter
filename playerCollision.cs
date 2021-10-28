using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerCollision : MonoBehaviour
{
    Animator ascore;
    public GameObject AddScorePanel;
    public GameObject gameOverScreen;
    public Camera MainCamera; 
    private Vector2 screenBounds;
    public GameObject scoretext;
    public GameObject highScoretext;
    private int score=0;
    private int HighScore = 0;
    private float objectWidth;
    private float objectHeight;
    public string shape;
    public GameObject cube;
    public GameObject sphere;
    public GameObject capsule;
    public GameObject cylinder;
    public GameObject player;
    public Vector3 cameraOffset;
    public GameObject particles;
    

    void Start()
    {
        particles.GetComponent<ParticleSystem>().enableEmission = false;
        cameraOffset = MainCamera.transform.position;

        

        ascore = AddScorePanel.GetComponent<Animator>();
        HighScore = PlayerPrefs.GetInt("highscore",HighScore);
        if(HighScore >= 1000)
        {
            transform.GetComponent<PlayerMovement>().tutorial1.SetActive(true);
        }
        highScoretext.GetComponent<TMPro.TextMeshProUGUI>().text = HighScore.ToString();
        scoretext.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
        shape = "Sphere";
        

        
        objectWidth = sphere.transform.GetComponent<MeshRenderer>().bounds.extents.z; 
        objectHeight = sphere.transform.GetComponent<MeshRenderer>().bounds.extents.y; 
        
    }
    
    
        
    

    void LateUpdate(){
        //--------------------------SCORE LOGIC---------------------------//
        if(transform.GetComponent<PlayerMovement>().isAlive)
        {
            MainCamera.transform.position = player.transform.position + cameraOffset - new Vector3(0,player.transform.position.y,player.transform.position.z);
        }

        if(transform.GetComponent<PlayerMovement>().isAlive && transform.GetComponent<PlayerMovement>().isScore)
        {
            score+=1;
            scoretext.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
        
            if(score >= PlayerPrefs.GetInt("highscore",HighScore))
            {
                PlayerPrefs.SetInt("highscore",score);
            
                highScoretext.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
                
            }

        }
        
        
    }   
    IEnumerator stopParticleEmission(GameObject pa, float timeDelay)//to control the time of particle collison//
    {
        yield return new WaitForSeconds(timeDelay);
        pa.transform.GetComponent<ParticleSystem>().enableEmission = false;
        Destroy(pa);
    }

    void OnCollisionEnter(Collision ColInfo)//handles collisons with different collidors of the player//
    {   
        
        
        if(ColInfo.collider.tag == "DeathBoundaries")
        {
            
            if( shape+"(Clone)"== ColInfo.collider.name)
            {
                AddScore();
            }
            else
            {
                Dead();
                PlayerPrefs.Save();
            }
            ColInfo.collider.enabled = false;
        }
        else if(ColInfo.collider.tag == "BotBoundary")
        {
                transform.GetComponent<PlayerMovement>().isBotBound = true;
                
        }
        else 
        {
            GameObject pa = Instantiate(particles,player.transform.position,player.transform.rotation);
            pa.transform.GetComponent<ParticleSystem>().enableEmission = true;
            StartCoroutine(stopParticleEmission(pa,1f));
            ColInfo.collider.enabled = false;
            buttonClick(ColInfo.collider.tag);
            
        }

    }
    public void AddScore()
    {
        score+=100;
        ascore.SetTrigger("addscoreTrigger");

    }
    public void Dead()
    {
        
        transform.GetComponent<PlayerMovement>().dead();
        gameOverScreen.SetActive(true);
        transform.GetComponent<PlayerMovement>().isAlive = false;
        
        
    }
    
    public void onchangeSpriteSphere()
    {
        Transform location = player.transform;
        cube.SetActive(false);
        sphere.SetActive(true);
        capsule.SetActive(false);
        cylinder.SetActive(false);
        sphere.transform.position = location.position;
        shape = "Sphere";
        player.GetComponent<MeshCollider>().sharedMesh = sphere.GetComponent<MeshFilter>().mesh;
        
    }
    public void onchangeSpriteCube()
    {
        Transform location = player.transform;
        sphere.SetActive(false);
        cube.SetActive(true);
        capsule.SetActive(false);
        cylinder.SetActive(false);
        cube.transform.position = location.position;
        shape = "Cube";
        player.GetComponent<MeshCollider>().sharedMesh = cube.GetComponent<MeshFilter>().mesh;
        
    }
    public void onchangeSpriteCylinder()
    {
        Transform location = player.transform;
        sphere.SetActive(false);
        cube.SetActive(false);
        capsule.SetActive(false);
        cylinder.SetActive(true);
        cube.transform.position = location.position;
        shape = "Cylinder";
        player.GetComponent<MeshCollider>().sharedMesh = cylinder.GetComponent<MeshFilter>().mesh;
        
    }
    public void onchangeSpriteCapsule()
    {
        Transform location = player.transform;
        sphere.SetActive(false);
        cube.SetActive(false);
        capsule.SetActive(true);
        cylinder.SetActive(false);
        cube.transform.position = location.position;
        shape = "Capsule";
        player.GetComponent<MeshCollider>().sharedMesh = capsule.GetComponent<MeshFilter>().mesh;
        
    }
    public void buttonClick(string a)
    {
        if(a == "cube")
        {
            
               onchangeSpriteCube();
        }
        if(a == "sphere")
        {
                onchangeSpriteSphere();
        }
        if(a == "cylinder")
        {
           
                onchangeSpriteCylinder();
        }
        if(a == "capsule")
        {
           
                onchangeSpriteCapsule();
        }
        if(a == "TransformingSpeed")
        {
            
        }
    }
}
