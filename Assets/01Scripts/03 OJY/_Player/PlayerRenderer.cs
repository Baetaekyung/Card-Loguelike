using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Players
{
    public class PlayerRenderer : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private Transform playerVisual;
        public Transform GetPlayerVisualTransform => playerVisual;
        private MeshRenderer meshRenderer;
        [SerializeField] private Material matOnNormal;
        [SerializeField] private Material matOnRoll;
        public Material GetMatOnNormal => matOnNormal;
        public Material GetMatOnRoll => matOnRoll;
        public void Init(Player _player)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        public void Dispose(Player _player)
        {

        }
        public void SetVisulDirectionSmooth(Vector3 dir)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
            float multiplier = Quaternion.Angle(targetRot, playerVisual.rotation);
            playerVisual.rotation = Quaternion.RotateTowards(playerVisual.rotation, targetRot, Time.deltaTime * multiplier * 10);
        }
        public void SetVisualDirection(Vector3 dir)
        {
            playerVisual.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
        public void SetMaterial(Material _material)
        {
            meshRenderer.material = _material;
        }


    }
}
