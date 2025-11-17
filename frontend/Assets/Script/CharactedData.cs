using Layer_lab._3D_Casual_Character;

[System.Serializable]
public class SerializablePart
{
    public PartsType type;
    public int index;
}

[System.Serializable]
public class CharacterData
{
    public SerializablePart[] parts;
}
