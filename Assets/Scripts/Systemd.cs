using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systemd : MonoBehaviour
{
    [SerializeField]
    private List<DemonRequest> DemonRequest;

    public DemonRequest PopDemonRequest()  {
        if (DemonRequest.Count == 0) return null;

        var res = DemonRequest[0];
        DemonRequest.RemoveAt(0);
        return res;
    }
}
