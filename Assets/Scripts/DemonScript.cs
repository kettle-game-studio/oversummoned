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
    public SpriteRenderer[] FaceBlood;
    public SpriteRenderer[] HornsBlood;
    public SpriteRenderer[] Brains;
    public SpriteRenderer[] FacesClosed;

    public ParticleSystem Particles;

    private SpriteRenderer[][] partsArrays;
    private int[] sorting = new []{10, 20, 30, 10, 0, 40, 20, 40, 30};

    [SerializeField]
    private DemonConfiguration _config;

    public DemonConfiguration config => _config;

    public DamageLevel damageLevel = new DamageLevel();

    private float timeOffset; 
    private float animationSpeed;
    private float angleOffset = 0;
    
    private float _timeUntilBlink;

    void Start()
    {
        _timeUntilBlink = Random.Range(1.0f, 3.0f);
        timeOffset = Random.Range(0f, 5f); 
        animationSpeed = Random.Range(0.9f, 1.1f);
        partsArrays = new SpriteRenderer[][]{ Bodies, Heads, Faces, Horns, Features, FaceBlood, HornsBlood, Brains, FacesClosed };
        SpritePole.localScale = new Vector3(Scale, Scale * Mathf.Sqrt(2f), Scale);
        Shadowcaster.transform.localScale = new Vector3(Scale, Scale, Scale);
        if (_config == null) {
            Shuffle();
        } else {
            _config = new DemonConfiguration {
                HeadId = _config.HeadId == -1 ? Random.Range(0, Heads.Length) : _config.HeadId,
                FaceId = _config.FaceId == -1 ? Random.Range(0, Faces.Length) : _config.FaceId,
                HornsId = _config.HornsId == -1 ? Random.Range(0, Horns.Length) : _config.HornsId,
                BodyId = _config.BodyId == -1 ? Random.Range(0, Bodies.Length) : _config.BodyId,
                FeatureId = _config.FeatureId == -1 ? Random.Range(0, Features.Length) : _config.FeatureId,
            };
            ApplyConfig();
        }
    }

    void Update() {
        var angle = System.MathF.Sin(Time.time * animationSpeed + timeOffset) * 10 + angleOffset;
        DemonBody.transform.rotation = Quaternion.identity; //Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        DemonBody.transform.Rotate(Camera.main.transform.forward, angle);
        DemonBody.transform.Rotate(Vector3.up, Camera.main.transform.rotation.eulerAngles.y);

        for (var i = 0; i < sorting.Length; i++)
        {
            foreach(var part in partsArrays[i])
            {
                part.sortingOrder = sorting[i] - ((int)Vector3.Distance(Camera.main.transform.position, transform.position) * 100);
            }
        }

        HandleBlink();
    }

    void HandleBlink()
    {
        _timeUntilBlink -= Time.deltaTime;
        if (_timeUntilBlink > 0) return;
            
        if (damageLevel.EyesClosed)
        {
            _timeUntilBlink = Random.Range(1.0f, 3.0f);
        } else {
            _timeUntilBlink = 0.1f;
        }

        damageLevel.EyesClosed = !damageLevel.EyesClosed;
        ApplyConfig();
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

    public void SetConfig(DemonConfiguration config, DamageLevel damageLevel = null) 
    {
        _config = config;
        if (damageLevel != null)
            this.damageLevel = damageLevel;
    
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
        // DemonBody.isKinematic = true;
        angleOffset = 180;
        animationSpeed *= 3;
        Particles.Play();
    }

    public void StopBeHeld()
    {
        // DemonBody.isKinematic = false;
        angleOffset = 0;
        animationSpeed /= 3;
        Particles.Play();
    }
}
