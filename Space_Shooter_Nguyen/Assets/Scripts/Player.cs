using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variable requirements - public or private reference, data type (int, float, bool, string), a name. Optional value assigned
    [SerializeField] //allows for changing in the inspector
    private float _speed = 6.0f; //float is a number a decimal and we're creating the _speed variable with a 4.5 value

    [SerializeField]
    private GameObject _laserPrefab; //create prefab variable for the laser

    [SerializeField]
    private int _lives = 3; //setting our lives to a value of 3 to start

    private SpawnManager _spawnManager;

    private UIManager _uiManager;

    void Start() // Start is called before the first frame update
    {
        //player start position = (0, 0, 0)
        transform.position = new Vector3(0, 0, 0); //access transform component to set new position to 0, 0, 0
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update() // Update is called once per frame
    {
       PlayerMovement();
       //if the space bar is pushed then the laser gets instantiated (created)
       if (Input.GetKeyDown(KeyCode.Space))
       {
           Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
       }   
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //defining the horizontal input variable and using Unity default name
        float verticalInput = Input.GetAxis("Vertical"); //define vertical input variable using unity default name

                    // new Vector 3 (1, 0, 0) * player horizontal input * speed variable * real time   
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime); 
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime); //create transform.Translate for vertical input
        
        //if the player's y value is > 5.9 or < -3.9 the player player will stop at theose location
        if (transform.position.y >= 5.9f)
        {
            transform.position = new Vector3(transform.position.x, 5.9f, 0);
        }
        else if (transform.position.y <= -3.9f)
        {
            transform.position = new Vector3(transform.position.x, -3.9f, 0);
        }
        
        if (transform.position.x >= 9.2f)
        {
            transform.position = new Vector3(9.2f,transform.position.y, 0);
        }
        else if (transform.position.x <= -9.2f)   
        {
            transform.position = new Vector3( -9.2f,transform.position.y, 0);
        }
    }
        public void Damage()
        {
            _lives -= 1;
            _uiManager.UpdateLives(_lives); //links to Ui Manager to update the current lives

            if (_lives < 1)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }
}

/*
        Optimizatized Options
        combine variales: transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed *Time.deltaTime);

        or combine variables and create a new direction variable:
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate((direction) * _speed *Time.deltaTime);           */
