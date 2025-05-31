using UnityEngine;

public class WizardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject wizard;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform tf = transform.GetChild(i).transform;
            Instantiate(wizard, tf.position + new Vector3(0,2,0), Quaternion.identity);
        }
    }
}
