using UnityEngine;
using UnityEngine.Events;
using Spettro.HealthSystem;
#if UNITY_EDITOR
namespace Spettro.Editors
{
    [UnityEditor.CustomEditor(typeof(Health))]
    public class HealthEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Health h = (Health)target;

            if (UnityEditor.EditorApplication.isPlaying)
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
                UnityEditor.EditorGUILayout.HelpBox("You can control the health script from the inspector if you're in Play Mode.", UnityEditor.MessageType.Info);
        }
    }
}
#endif
namespace Spettro.HealthSystem
{
    public class Health : MonoBehaviour
    {
        public int HP;
        public int maxHP;
        public int livesCount;

        [Header("Options")]
        public bool log = true;
        public bool lives = false;
        public bool resetOnDeath = false;

        [Header("Events")]
        public UnityEvent<int> OnDamage;
        public UnityEvent<int> OnHeal;
        public UnityEvent<int> OnDeath;
        public UnityEvent OnLivesOver;
        public void Start()
        {
            HealthSet();
        }
        public void HealthSet()
        {
            HP = maxHP;
        }

        public void Damage(int amount)
        {
            Debug.Log("Recieved damage of " + amount);
            HP -= amount;
            if (HP <= 0)
            {
                HP = 0;
                if (OnDeath != null)
                {
                    OnDeath.Invoke(HP);
                    if (log)
                        Debug.LogWarning(gameObject.name + " has died.");
                    if (resetOnDeath)
                        HealthSet();
                    if (lives)
                    {
                        livesCount--;
                        if (livesCount < 0)
                            OnLivesOver.Invoke();
                    }

                }

            }
            if (OnDamage != null)
            {
                OnDamage.Invoke(HP);
                if (log)
                    Debug.LogWarning($"{gameObject.name} recieved damage ({amount}). New HP: {HP}");
            }

        }
        public void Heal(int amount)
        {
            HP += amount;
            if (HP > maxHP)
            {
                HP = maxHP;
            }
            if (OnHeal != null)
            {
                OnHeal.Invoke(HP);
                if (log)
                    Debug.LogWarning($"{gameObject.name} has been healed by {amount}. New HP: {HP}");
            }
        }

        public float GetHPNormalized()
        {
            return (float)HP / maxHP;
        }

    }
}