using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{
    public string Name;
    public Rigidbody DemonBody;

    public SpriteRenderer[] Bodies;
    public SpriteRenderer[] Heads;
    public SpriteRenderer[] Faces;
    public SpriteRenderer[] Horns;
    public SpriteRenderer[] Features;

    private SpriteRenderer[][] partsArrays;

    void Start()
    {
        partsArrays = new SpriteRenderer[][]{ Bodies, Heads, Faces, Horns, Features };
        Shuffle();
    }

    void Shuffle()
    {
        foreach (var parts in partsArrays)
        {
            foreach(var part in parts)
            {
                part.enabled = false;
            }
            var idx = Random.Range(0, parts.Length);
            parts[idx].enabled = true;
        }
    }

    public void StartBeHeld()
    {
        DemonBody.isKinematic = true;
    }

    public void StopBeHeld()
    {
        DemonBody.isKinematic = false;
    }
}
