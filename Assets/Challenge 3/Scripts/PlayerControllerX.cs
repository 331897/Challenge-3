using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    

    
    
    private Rigidbody playerRb;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public float floatForce = 10;
    public float gravityModifier;
    public bool gameOver;
    public bool isLowEnough;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
      

        // Apply a small upward force at the start of the game
   

    }

    // Update is called once per frame
    void Update()
    { 
        if (transform.position.y > 13)
        {
            transform.position = new Vector3(transform.position.x, 13, transform.position.z);
            isLowEnough = false;
        }
        else
        {
            isLowEnough = true;
        }
        // While space is pressed and player is low enough, float up
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && isLowEnough)
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }
    }
 private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

    }

}
