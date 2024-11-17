using UnityEngine;

namespace CardGame.Players
{
    [CreateAssetMenu(menuName = "OJYSO/PlayerSingletonSO", order = int.MinValue)]
    public class PlayerSingletonSO : ScriptableObject
    {
        public Player Instance { get; private set; }
        public void SetPlayer(Player player)
        {
            Instance = player;
        }
    }
}
