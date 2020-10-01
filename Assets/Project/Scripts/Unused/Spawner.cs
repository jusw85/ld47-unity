// using System.Collections;
// using k;
// using UnityEngine;
//
// public class Spawner : MonoBehaviour
// {
//     public float initialDelay = 3.0f;
//     public float spawnMin = 1.0f;
//     public float spawnMax = 2.0f;
//
//     public bool runForever;
//     public int spawnInstances = 3;
//     public int numPerSpawn = 1;
//
//     public GameObject objectToSpawn;
//     public Animator anim;
//     public float walkSpeed;
//
//     private void Start()
//     {
//         StartCoroutine(SpawnCoroutine());
//     }
//
//     IEnumerator SpawnCoroutine()
//     {
//         yield return new WaitForSeconds(initialDelay);
//         while (runForever || spawnInstances > 0)
//         {
//             anim.SetBool(AnimatorParams.SPAWNING, true);
//             yield return new WaitForSeconds(2f);
//
//             spawnInstances -= numPerSpawn;
//             for (int i = 0; i < numPerSpawn; i++)
//             {
//                 GameObject obj = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
//                 obj.GetComponent<EnemyMover>().WalkSpeed = walkSpeed;
//             }
//
//             yield return new WaitForSeconds(1f);
//             anim.SetBool(AnimatorParams.SPAWNING, false);
//             yield return new WaitForSeconds(1f);
//             yield return new WaitForSeconds(Random.Range(spawnMin, spawnMax));
//         }
//     }
// }