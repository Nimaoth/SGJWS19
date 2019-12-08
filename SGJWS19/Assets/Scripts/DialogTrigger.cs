using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public string[] Text;
    public bool Kill = true;

    private void OnTriggerEnter2D(Collider2D other) {
        foreach (var text in Text)
            DialogSystem.Instance.QueueText(text);

        if (Kill)
            GameObject.Destroy(gameObject);
    }
}
