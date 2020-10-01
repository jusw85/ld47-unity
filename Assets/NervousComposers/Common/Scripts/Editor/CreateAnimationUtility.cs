using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Jusw85.Common
{
    public class CreateAnimationUtility : EditorWindow
    {
        private AnimationClip clip;
        private int targetFrameInterval;

        [MenuItem("Tools/Nervous Composers/Create Animation Utility", false, 100)]
        private static void CreateAnimationUtilityMenu()
        {
            GetWindow(typeof(CreateAnimationUtility));
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Set clip timings");
            clip = (AnimationClip) EditorGUILayout.ObjectField("Animation Clip", clip, typeof(AnimationClip), false);
            targetFrameInterval = EditorGUILayout.IntField("Num frames between sprites", targetFrameInterval);
            if (GUILayout.Button("Set clip timings")) SetClipTimings();

            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("Generate Animator Parameter Settings");
            if (GUILayout.Button("Reset Default Settings")) GenerateAnimatorParameters.DefaultSettings();
            GenerateAnimatorParameters.searchPath =
                EditorGUILayout.TextField("Search path", GenerateAnimatorParameters.searchPath);
            GenerateAnimatorParameters.outFolder =
                EditorGUILayout.TextField("Out Folder", GenerateAnimatorParameters.outFolder);
            GenerateAnimatorParameters.fileName =
                EditorGUILayout.TextField("Filename", GenerateAnimatorParameters.fileName);
        }

        private void SetClipTimings()
        {
            targetFrameInterval = (int) Mathf.Clamp(targetFrameInterval, 1f, int.MaxValue);
            if (clip == null)
            {
                Debug.Log("No clip selected");
                return;
            }

            EditorCurveBinding? spriteCurveBindingOpt = getSpriteCurveBinding(clip);
            if (!spriteCurveBindingOpt.HasValue)
            {
                Debug.Log("Unable to find sprite curve");
                return;
            }

            EditorCurveBinding spriteCurveBinding = spriteCurveBindingOpt.Value;

            ObjectReferenceKeyframe[]
                keyFrames = AnimationUtility.GetObjectReferenceCurve(clip, spriteCurveBinding);
            int l = keyFrames.Length;
            bool lastFrameRepeated = l > 1 && (keyFrames[l - 1].value == keyFrames[l - 2].value);
            int numSprites = lastFrameRepeated ? l - 1 : l;
            bool repeatLastFrame = numSprites > 1 && targetFrameInterval > 1;
            float frameRate = clip.frameRate;
            float frameTime = 1f / frameRate;

            ObjectReferenceKeyframe[] newKeyFrames =
                new ObjectReferenceKeyframe[repeatLastFrame ? numSprites + 1 : numSprites];
            int i = 0;
            ObjectReferenceKeyframe keyFrame;
            for (; i < numSprites; i++)
            {
                keyFrame = new ObjectReferenceKeyframe
                {
                    value = keyFrames[i].value,
                    time = (i * targetFrameInterval) * frameTime
                };
                newKeyFrames[i] = keyFrame;
            }

            if (repeatLastFrame)
            {
                keyFrame = new ObjectReferenceKeyframe
                {
                    value = keyFrames[i - 1].value,
                    time = ((i * targetFrameInterval) - 1) * frameTime
                };
                newKeyFrames[i] = keyFrame;
            }

            AnimationUtility.SetObjectReferenceCurve(clip, spriteCurveBinding, newKeyFrames);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private EditorCurveBinding? getSpriteCurveBinding(AnimationClip clip)
        {
            EditorCurveBinding[] objectCurveBindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);
            IEnumerator<EditorCurveBinding> spriteCurveBindings =
                objectCurveBindings.Where(obj => obj.propertyName.Equals("m_Sprite")).GetEnumerator();
            if (!spriteCurveBindings.MoveNext())
            {
                return null;
            }

            return spriteCurveBindings.Current;
        }
    }
}