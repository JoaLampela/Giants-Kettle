using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPrefab : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(startLoading());
        }
        IEnumerator startLoading()
        {
            GameObject.FindGameObjectWithTag("UI").GetComponent<PauseMenu>().StartLoad();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();



            Debug.Log("EXITING LEVEL");
            GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<MapGeneration>().ExitLevel();
            GameObject.Find("Game Manager").GetComponent<GameEventManager>().ExitLevel();

        }
    }
}
