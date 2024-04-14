using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public SpriteRenderer[] FaceBlood;
    public SpriteRenderer[] HornsBlood;
    public SpriteRenderer[] Brains;
    public SpriteRenderer[] FacesClosed;

    private SpriteRenderer[][] partsArrays;
    private int[] sorting = new []{10, 20, 30, 10, 0, 40, 20, 40, 30};
    private DemonConfiguration _config;

    public DamageLevel damageLevel = new DamageLevel();

    void Start()
    {
        partsArrays = new SpriteRenderer[][]{ Bodies, Heads, Faces, Horns, Features, FaceBlood, HornsBlood, Brains };
        SpritePole.localScale = new Vector3(Scale, Scale, Scale);
        Shadowcaster.transform.localScale = new Vector3(Scale, Scale, Scale);
        Shuffle();
    }

    void Update() {
        DemonBody.transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);

        for (var i = 0; i < sorting.Length; i++)
        {
            foreach(var part in partsArrays[i])
            {
                part.sortingOrder = sorting[i] - ((int)Vector3.Distance(Camera.main.transform.position, transform.position) * 100);
            }
        }
    }

    void Shuffle()
    {
        _config = new DemonConfiguration {
            HeadId = Random.Range(0, Heads.Length),
            FaceId = Random.Range(0, Faces.Length),
            HornsId = Random.Range(0, Horns.Length),
            BodyId = Random.Range(0, Bodies.Length),
            FeatureId = Random.Range(0, Features.Length),
        };
        ApplyConfig();
    }

    void ApplyConfig()
    {
        for(var i = 0; i < Heads.Length; i++)
            Heads[i].enabled = _config.HeadId == i;

        for(var i = 0; i < FacesClosed.Length; i++)
            FacesClosed[i].enabled = _config.FaceId == i && damageLevel.EyesClosed;

        for(var i = 0; i < Faces.Length; i++)
            Faces[i].enabled = _config.FaceId == i && !damageLevel.EyesClosed;

        for(var i = 0; i < Horns.Length; i++)
            Horns[i].enabled = _config.HornsId == i;

        for(var i = 0; i < Bodies.Length; i++)
            Bodies[i].enabled = _config.BodyId == i;

        for(var i = 0; i < Features.Length; i++)
            Features[i].enabled = _config.FeatureId == i;
        
        for(var i = 0; i < FaceBlood.Length; i++)
            FaceBlood[i].enabled = _config.FaceId == i && damageLevel.FaceBlood;
        
        for(var i = 0; i < HornsBlood.Length; i++)
            HornsBlood[i].enabled = _config.HornsId == i && damageLevel.HornsBlood;
        
        for(var i = 0; i < Brains.Length; i++)
            Brains[i].enabled = /*_config.HornsId == i &&*/ damageLevel.Brains;

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
