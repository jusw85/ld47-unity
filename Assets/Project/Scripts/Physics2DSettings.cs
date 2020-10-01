using UnityEditor;
using UnityEngine;

public class Physics2DSettings : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 4;
    [SerializeField] private float timeToJumpApex = 0.4f;
    private float gravity;
    private float jumpVelocity;

    private void Awake()
    {
        UpdateGravity();
        Physics2D.autoSimulation = true;
        Physics2D.queriesStartInColliders = false;
        Physics2D.autoSyncTransforms = false;
        Physics2D.gravity = new Vector2(0, gravity);
    }

    private void OnValidate()
    {
        UpdateGravity();
    }

    private void UpdateGravity()
    {
        gravity = (2 * -jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = -(gravity * timeToJumpApex);
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Physics2DSettings))]
    [CanEditMultipleObjects]
    public class Physics2DSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Physics2DSettings obj = (Physics2DSettings) target;
            EditorGUILayout.LabelField("Gravity", obj.gravity.ToString());
            EditorGUILayout.LabelField("Jump Velocity", obj.jumpVelocity.ToString());
        }
    }
#endif
}