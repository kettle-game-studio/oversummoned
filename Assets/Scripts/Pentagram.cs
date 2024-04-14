using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentragramScript : MonoBehaviour
{
    public ParticleSystem Particles;

    void OnTriggerEnter(Collider other)
    {
        var demon = other.GetComponent<DemonScript>();
        if (demon != null)
        {
            Particles.Play();
            Destroy(demon.gameObject);
            StartCoroutine(AnimationCoroutine());
        }
    }

    IEnumerator AnimationCoroutine()
    {
        const float timeForChange = 0.5f;
        var timeLeft = timeForChange;
        while (timeLeft > 0)
        {
            var s = Mathf.Sin(timeLeft / timeForChange) * 0.1f + 0.9f;
            var c = Mathf.Cos(timeLeft / timeForChange) * 0.1f + 0.9f;
            transform.localScale = new Vector3(c, s, c);
            timeLeft -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = new Vector3(1,1,1);
    }
}
