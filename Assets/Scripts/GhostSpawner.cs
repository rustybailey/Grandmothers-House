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
        var lastPosIndex = 0;
        while (true)
        {
            // Get 3 random vectors in the ranges of "top half", "left third", "right third"
            // Add each of those to an array
            // Randomly pick one to be the winning random position
            var rightThird = new Vector2(Random.Range(5.0f, 8.0f), Random.Range(-3.0f, 3.0f));
            var leftThird = new Vector2(Random.Range(-5.0f, -8.0f), Random.Range(-3.0f, 3.0f));
            var topMiddle = new Vector2(Random.Range(-4f, 4f), Random.Range(2.5f, 3.5f));
            var randomPositions = new Vector2[] { rightThird, leftThird, topMiddle };
            var randomPosIndex = Random.Range(0, randomPositions.Length);
            // Prevent spawning in the same section back-to-back
            if (randomPosIndex == lastPosIndex)
            {
                randomPosIndex -= 1;
                randomPosIndex = randomPosIndex < 0 ? randomPositions.Length - 1 : randomPosIndex;
                lastPosIndex = randomPosIndex;
            }
            var randomPos = randomPositions[randomPosIndex];

            // Spawn ghost with a random position, sprite, scale, flipX
            var spawnedGhost = Instantiate(titleGhost, randomPos, Quaternion.identity);
            var spriteRenderer = spawnedGhost.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = ghostSprites[Random.Range(0, ghostSprites.Length)];
            spriteRenderer.flipX = Random.value > 0.5f;
            float randomScale = Random.Range(0.6f, 1f);
            spawnedGhost.transform.localScale = new Vector3(randomScale, randomScale);
            transform.parent = spawnedGhost.transform;

            StartCoroutine(DestroyGhost(spawnedGhost));
            yield return new WaitForSeconds(.75f);
        }
    }

    private IEnumerator DestroyGhost(GameObject spawnedGhost)
    {
        yield return new WaitForSeconds(2f);
        Destroy(spawnedGhost);
    }
}
