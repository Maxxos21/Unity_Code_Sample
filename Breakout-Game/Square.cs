using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    // Config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject squareSparklesVFX;
    [SerializeField] Sprite[] hitSprites;
    
    // cached reference
    Level level;

    // state variables 
    [SerializeField] int timesHits; //TODO serialized for debug 

    private void Start()
    {
        CountingBreakAbleSquares();
    }

    private void CountingBreakAbleSquares()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHits++;
        int maxHits = hitSprites.Length + 1;
        if (timesHits >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHits - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("block sprite is missing from array" + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroy();
        TriggerSparklesVFX();

    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameStatus>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(squareSparklesVFX, transform.position, transform.rotation);
        gameObject.SetActive(false);
        Destroy(sparkles, 1f);
    }
}
