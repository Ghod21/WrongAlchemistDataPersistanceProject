using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundJugDelete : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(ObjectDelete());
    }

    IEnumerator ObjectDelete()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
