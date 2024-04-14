using System;

[Serializable]
public class DamageLevel {
    public bool HornsBlood;
    public bool FaceBlood;
    public bool Brains;
    public bool EyesClosed;
}

public class DemonConfiguration {
    public int BodyId;
    public int HeadId;
    public int FaceId;
    public int HornsId;
    public int FeatureId;
}

[Serializable]
public class DemonRequest {
    public int HornsId;
    public int FeatureId;
}