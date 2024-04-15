using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Systemd : MonoBehaviour
{
    [SerializeField]
    private List<DemonRequest> DemonRequest;
    public float Timeout = 15;
    public int InitialQueueSurge = 1;
    public TextMeshProUGUI Text;

    private Queue<DemonRequest> Queue = new Queue<DemonRequest>();
    private int totalDemonsSent = 0;

    private Queue<DemonConfiguration> RespawnQueue = new Queue<DemonConfiguration>();

    void Start()
    {
        SetText();
        StartCoroutine(DistributionCoroutine());
    }

    IEnumerator DistributionCoroutine()
    {
        foreach (var req in DemonRequest)
        {
            Queue.Enqueue(req);
            if (InitialQueueSurge > 0)
            {
                InitialQueueSurge--;
                continue;
            }
            while (Queue.Count != 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(Timeout);
        }
        while (totalDemonsSent != DemonRequest.Count)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public DemonRequest PopDemonRequest()
    {
        if (Queue.Count == 0) return null;

        return Queue.Dequeue();
    }

    public DemonConfiguration PopRespawndDemon()
    {
        if (RespawnQueue.Count == 0) return null;

        return RespawnQueue.Dequeue();
    }


    public void DemonSent(DemonConfiguration config)
    {
        totalDemonsSent++;
        SetText();
        StartCoroutine(RespawnCoroutine(config));
    }

    void SetText()
    {
        Text.text = $"{totalDemonsSent}/{DemonRequest.Count}";
    }

    IEnumerator RespawnCoroutine(DemonConfiguration config)
    {
        yield return new WaitForSeconds(5);

        RespawnQueue.Enqueue(config);
    }
}
