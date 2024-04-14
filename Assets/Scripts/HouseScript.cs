using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    public Transform House;
    public GameObject DemonPrefab;
    public Transform Spawnpoint;
    public SphereCollider GuardSphere;
    public float TimeToWork = 5; 

    public Action action;
    public int LimbId;

    enum State
    {
        Idle,
        Working,
    }

    [Serializable]
    public enum Action {
        None,
        Shuffle,
        ReplaceHorns,
        ReplaceFeature,
        CleanBlood,
    } 

    private State state = State.Idle;
    private DemonConfiguration demonConfig;
    private DamageLevel damageLevel;
    private float _timeLeft;

    void Start(){
        GuardSphere.enabled = false;
    }

    void Update()
    {
        if (state == State.Working)
        {
            var s = Mathf.Sin(Time.time * 10) * 0.1f + 0.9f;
            var c = Mathf.Cos(Time.time * 10) * 0.1f + 0.9f;
            House.localScale = new Vector3(s, c, s);
            _timeLeft -= Time.deltaTime;
            if (_timeLeft < 0) {
                state = State.Idle;
                GuardSphere.enabled = false;

                var demon = Instantiate(DemonPrefab, Spawnpoint.position, Spawnpoint.rotation).GetComponent<DemonScript>();
                switch (action)
                {
                    case Action.None:
                        demon.SetConfig(demonConfig, damageLevel);
                        break;
                    
                    case Action.Shuffle:
                        break;
                    
                    case Action.ReplaceHorns:
                        demonConfig.HornsId = LimbId;
                        demon.SetConfig(demonConfig, damageLevel);
                        break;
                    
                    case Action.ReplaceFeature:
                        demonConfig.FeatureId = LimbId;
                        demon.SetConfig(demonConfig, damageLevel);
                        break;

                    case Action.CleanBlood:
                        demon.SetConfig(demonConfig);
                        break;
                }
                demonConfig = null;
                damageLevel = null;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (state != State.Idle) return;

        var demon = other.GetComponent<DemonScript>();
        if (demon != null){
            GuardSphere.enabled = true;
            demonConfig = demon.config;
            damageLevel = demon.damageLevel;
            state = State.Working;
            Destroy(demon.gameObject);
            _timeLeft = TimeToWork;
        }
    } 
}
