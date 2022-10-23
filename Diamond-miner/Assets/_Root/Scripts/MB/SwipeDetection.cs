using TMPro;
using UnityEngine;

namespace MB
{
    public class SwipeDetection : MonoBehaviour
    {
        public event OnSwipeInput SwipeEvevt;
        public delegate void OnSwipeInput(Vector2 direction);

        private Vector2 _tapPosition;
        private Vector2 _swipeDelta;

        private float _deadZone = 10f;

        private bool isSwiping;
        private bool isMobile;
        private float _timeConst = 0.5f;
        private float _time;

        void Start()
        {
            isMobile = Application.isMobilePlatform;
        }

        void Update()
        {
            var deltaTime = Time.deltaTime;
            _time += Time.deltaTime;
            if (!isMobile)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isSwiping = true;
                    _tapPosition = Input.mousePosition;
                    Debug.Log("Начало записи");
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    ResetSwipe();
                    Debug.Log("Конец записи");
                }
                   
            }
            else
            {
                if (Input.touchCount >0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        isSwiping = true;
                        _tapPosition = Input.GetTouch(0).position;
                    }
                    
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended)
                    ResetSwipe();
            }

            CheckSwipe();
        }

        private void CheckSwipe()
        {
            _swipeDelta = Vector2.zero;

            if (isSwiping)
            {
                if(!isMobile && Input.GetMouseButton(0))
                    _swipeDelta = (Vector2)Input.mousePosition - _tapPosition;
                else if (Input.touchCount > 0)
                    _swipeDelta = Input.GetTouch(0).position - _tapPosition;
            }

            if (_time >= _timeConst)
            {
                if (_swipeDelta.magnitude > _deadZone)
                {
                    if (SwipeEvevt != null)
                    {
                        if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                            SwipeEvevt(_swipeDelta.x > 0 ? Vector2.right : Vector2.left);
                        else
                            SwipeEvevt(_swipeDelta.y > 0 ? Vector2.up : Vector2.down);
                        Debug.Log("Отправка данных");
                    }
                    _time = 0;
                }
            }
        }

        private void ResetSwipe()
        {
            isSwiping = false;

            _tapPosition = Vector2.zero;
            _swipeDelta = Vector2.zero;
            Debug.Log("Oбнуление");
           
        }
    }
}
