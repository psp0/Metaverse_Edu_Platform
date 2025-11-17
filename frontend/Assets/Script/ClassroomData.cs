using System;

[System.Serializable]
public class SubUnitData
{
    public int subUnitId;
    public int subChapterNo;
    public string subTitle;
    public string description;
    public float progressPercentage;
    public float lectureProgress;
    public float quizProgress;
    public int lectureLastTimestampSec;
    public string contentUrl;
    public bool completed;
}

[System.Serializable]
public class UnitData
{
    public int unitId;
    public int chapterNo;
    public string title;
    public string description;
    public SubUnitData[] subUnits;
}

[System.Serializable]
public class SubjectData
{
    public int id;
    public string name;
    public UnitData[] units;
}

[System.Serializable]
public class ClassroomData
{
    public SubjectData[] subjects;
}
