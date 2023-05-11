using UnityEngine;

public class IsEnable : MonoBehaviour
{
    public int needToUnlock;
    public Material blackMaterial;

    private void Start() {
        if (PlayerPrefs.GetInt("score") < needToUnlock)
            GetComponent<MeshRenderer>().material = blackMaterial;
    }
}