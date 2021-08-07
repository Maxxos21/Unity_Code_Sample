using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableSquares;

    //cached ref
    SceneLoader sceneLoader;


    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks()
    {
        breakableSquares++;
    }

    public void BlockDestroy()
    {
        breakableSquares--;
        if (breakableSquares <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
