using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FadeAudioSource
{

    public static IEnumerator StartFade(AudioSource myAudioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = myAudioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            myAudioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}