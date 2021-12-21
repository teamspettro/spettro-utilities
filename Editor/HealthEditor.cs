using UnityEditor;
using Spettro.HP;

namespace Spettro.Editors
{
    [CustomEditor(typeof(Health))]
    public class HealthEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Health h = (Health)target;

            if (EditorApplication.isPlaying)
            {

                if (GUILayout.Button("Damage"))
                {
                    h.Damage(1);
                }
                if (GUILayout.Button("Heal"))
                {
                    h.Heal(1);
                }
                if (GUILayout.Button("Kill"))
                {
                    h.Damage(h.maxHP);
                }
            }
            else
                EditorGUILayout.HelpBox("You can control the health script from the inspector if you're in Play Mode.", MessageType.Info);
        }
    }
}