using System;
using UnityEditor;

[Serializable]
public class DamageLevel {
    public bool HornsBlood;
    public bool FaceBlood;
    public bool Brains;
    public bool EyesClosed;
}

[Serializable]
public class DemonConfiguration {
    public int BodyId;
    public int HeadId;
    public int FaceId;
    public int HornsId;
    public int FeatureId;

    public bool MeetsRequest(DemonRequest request) {
        if (request.FeatureId != -1 && request.FeatureId != FeatureId)
            return false;

        return request.HornsId == -1 ||  request.HornsId == HornsId;
    }
}

[Serializable]
public class DemonRequest {
    public int HornsId;
    public int FeatureId;
}