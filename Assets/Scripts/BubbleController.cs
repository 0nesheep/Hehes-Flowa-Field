using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip spawnSound;

    private static int inCount = 0;
    public bool showSpeechBubble = false;
    public float minSpawnInterval = 10f;
    public GameObject bubble;
    public GameObject flowerPart;
    private PlayerScript playerScript;
    private FlowerController flowerControl;
    private bool isPermaDeath = false;

    private bool playerIn = false;

    private float timer = 0f;

    private float timeIgnored = 0f;

    private bool isPlayerWet;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        bubble = transform.Find("Bubble").gameObject;
        bubble.gameObject.SetActive(false);

        flowerPart = transform.Find("FlowerPart").gameObject;

        GameObject player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        isPlayerWet = playerScript.checkIsWet();
        flowerControl = this.GetComponent<FlowerController>();

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = true;
            isPlayerWet = playerScript.checkIsWet();

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = false;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > minSpawnInterval)
        {
            showSpeechBubble = true;
            minSpawnInterval = minSpawnInterval * 2;
            audioSource.PlayOneShot(spawnSound);
        }
        if (isPlayerWet && playerIn && Input.GetKeyDown(KeyCode.Space) && showSpeechBubble)
        {
            inCount++;
            showSpeechBubble = false;
            timer = 0f;
            playerScript.water();
            timeIgnored = 0f;
            
        }
        if (!isPermaDeath) 
        {

            if (!showSpeechBubble)
            {
                if (bubble != null)
                {
                    bubble.gameObject.SetActive(false);
                }
            }
            else
            {
                if (bubble != null)
                {
                    bubble.gameObject.SetActive(true);
                }
                timeIgnored += Time.deltaTime;
                if (timeIgnored >= 30f)
                {
                    killPlant();
                    bubble.gameObject.SetActive(false);
                    isPermaDeath = true;
                }
            }
        
        } 
            

    }

    private void killPlant()
    {
        flowerControl.startDying();
    }
}
