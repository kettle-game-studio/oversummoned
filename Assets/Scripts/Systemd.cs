using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Systemd : MonoBehaviour
{
    [SerializeField]
    private List<DemonRequest> DemonRequest;
    public float Timeout = 15;

    private Queue<DemonRequest> Queue = new Queue<DemonRequest>();
    private int totalDemonsSent = 0;

    void Start() {
        StartCoroutine(DistributionCoroutine());
    }

    IEnumerator DistributionCoroutine() {
        foreach (var req  in DemonRequest) {
            Queue.Enqueue(req);
            Debug.Log("Enqueue demon");
            while(Queue.Count != 0) {
                yield return null;
            }
            yield return new WaitForSeconds(Timeout);
        }
        while(totalDemonsSent != DemonRequest.Count) {
            yield return null;
        }
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public DemonRequest PopDemonRequest()  {
        if (Queue.Count == 0) return null;

        return Queue.Dequeue();
    }

    public void DemonSent()
    {
        totalDemonsSent++;
    }
}