using TMPro;
using UnityEngine;

public class CommentItem : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI explanationText;

    public void SetData(string title, string explanation)
    {
        titleText.text = title;
        explanationText.text = explanation;
    }
}
