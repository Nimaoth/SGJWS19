using System.Collections;
using UnityEngine;

public class Sandman : MonoBehaviour
{
    public static Sandman Instance;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize() {
        var go = new GameObject("==== Sandman ====");
        Instance = go.AddComponent<Sandman>();
        GameObject.DontDestroyOnLoad(go);
    }

    public void Sleep(GameObject go, float duration) {
        IEnumerator coro() {
            go.SetActive(false);
            yield return new WaitForSeconds(duration);
            go.SetActive(true);
        }

        StartCoroutine(coro());
    }
}
