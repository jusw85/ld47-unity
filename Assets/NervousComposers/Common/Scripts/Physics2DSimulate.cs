using UnityEngine;

namespace Jusw85.Common
{
    /// <summary>
    /// - Set script execution after other scripts have updated their rb2ds 
    /// </summary>
    public class Physics2DSimulate : MonoBehaviour
    {
        private void Awake()
        {
            Physics2D.autoSimulation = false;
            Physics2D.queriesStartInColliders = false;
            Physics2D.autoSyncTransforms = false;
        }

        private void Update()
        {
            Physics2D.Simulate(Mathf.Epsilon);
        }
    }
}