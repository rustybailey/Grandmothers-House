using System.Collections;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] GameObject titleGhost;
    [SerializeField] Sprite[] ghostSprites;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnGhosts());
    }

    private IEnumerator SpawnGhosts()
    {
        while (true)
        {
            // TODO: Get random scale
            // TODO: randomly flip on Y
            // TODO: Confine position to sides and behind text
            // TODO: Make sure they always appear behind text
            // TODO: Destroy after ghost fading out (may need a separate coroutine for that)
            var randomPosition = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
            var spawnedGhost = Instantiate(titleGhost, randomPosition, Quaternion.identity);
            spawnedGhost.GetComponent<SpriteRenderer>().sprite = ghostSprites[Random.Range(0, 2)];
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
