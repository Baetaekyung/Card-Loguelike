using TMPro;
using UnityEngine;

namespace CardGame
{
    public class CardDrawTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timer;
        private float _time;
        private int _prevTime = 30;

        private void Start()
        {
            SetTimer();
        }

        private void SetTimer()
        {
            _time = 30f;
        }

        private void Update()
        {
            _time -= Time.deltaTime;
            if(_prevTime > (int)_time)
            {
                _prevTime -= 1;
                _timer.text = $"{_prevTime.ToString()}s left...";
                if(_prevTime <= 0f)
                {
                    SetTimer();
                    _timer.text = "Battle Start..";
                }
            }
        }
    }
}
