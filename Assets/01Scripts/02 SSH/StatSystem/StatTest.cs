using UnityEngine;

namespace CardGame
{
    public class StatTest : MonoBehaviour
    {
        [SerializeField]private ObjectStat _testStat;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (_testStat == null)
            {
                _testStat = GetComponentInChildren<ObjectStat>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                Debug.Log(_testStat.GetStat("Damage").Value);
            }
            if (Input.GetKey(KeyCode.S))
            {
                _testStat.SetModifier("Damage", 20);
                Debug.Log("DamageIncreased");
            }
            if (Input.GetKey(KeyCode.D))
            {
                _testStat.RemoveModifier("Damage");
                Debug.Log("DamageDecreased");
            }
        }
    }
}
