using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static int deathCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startDying()
    {
        animator.SetBool("isDying", true);
    }
    public void finishDying()
    {
        animator.SetBool("isDying", false);
        animator.SetBool("isDead", true);
        deathCount++;

    }

    public static int getDeaths()
    {
        return deathCount;
    }

    public void finishSpawn()
    {
        animator.SetBool("isSpawning", false);
    }
}
