using UnityEngine;

namespace CardGame
{
    public abstract class Entity : MonoBehaviour
    {
        [Header("Entity Setting")]
        [SerializeField] protected ObjectStat stat;
        public ObjectStat GetStat => stat;
    }
}
