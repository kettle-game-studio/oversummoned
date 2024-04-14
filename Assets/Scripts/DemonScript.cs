using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : MonoBehaviour
{
    public string Name;
    public float Scale = 1;

    public Rigidbody DemonBody;

    public MeshRenderer Shadowcaster;
    public Transform SpritePole;
    public SpriteRenderer[] Bodies;
    public SpriteRenderer[] Heads;
    public SpriteRenderer[] Faces;
    public SpriteRenderer[] Horns;
    public SpriteRenderer[] Features;

    private SpriteRenderer[][] partsArrays;

    void Start()
    {
        partsArrays = new SpriteRenderer[][]{ Bodies, Heads, Faces, Horns, Features };
        SpritePole.localScale = new Vector3(Scale, Scale, Scale);
        Shadowcaster.transform.localScale = new Vector3(Scale, Scale, Scale);
        Shuffle();
    }

    void Update() {
        DemonBody.transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
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