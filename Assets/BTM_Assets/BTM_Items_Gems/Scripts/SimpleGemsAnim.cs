using UnityEngine;
using System.Collections;

namespace Benjathemaker
{
    public class SimpleGemsAnim : MonoBehaviour
    {
        public bool isRotating = false;
        public bool roX = false;
        public bool roY = false;
        public bool roZ = false;
        public float roS = 90f; 



        public bool isFloating = false;
        public bool useEasingForFloating = false; 
        public float floatH = 1f;
        public float floatS = 1f;
        private Vector3 initialPosition;
        private float floatT;



        private Vector3 initialScale;
        public Vector3 startScale;
        public Vector3 endScale;



        public bool isScaling = false;
        public bool useEasingForScaling = false; 
        public float scaleLerpSpeed = 1f;
        private float scaleTimer;

        void Start()
        {
            initialScale = transform.localScale;
            initialPosition = transform.position;
            startScale = initialScale;
            endScale = initialScale * (endScale.magnitude / startScale.magnitude);
        }

        void Update()
        {
            if (isRotating)
            {
                Vector3 rotationVector = new Vector3(
                    roX ? 1 : 0,
                    roY ? 1 : 0,
                    roZ ? 1 : 0
                );
                transform.Rotate(rotationVector * roS * Time.deltaTime);
            }


            if (isScaling)
            {
                scaleTimer += Time.deltaTime * scaleLerpSpeed;
                float tmp = Mathf.PingPong(scaleTimer, 1f); // Oscillates between 0 and 1

                if (useEasingForScaling)
                {
                    tmp = EaseInOutQuad(tmp);
                }

                transform.localScale = Vector3.Lerp(startScale, endScale, tmp);
            }


            if (isFloating)
            {
                roS += Time.deltaTime * floatS;
                float tmp = Mathf.PingPong(floatT, 1f);
                if (useEasingForFloating) tmp = EaseInOutQuad(tmp);

                transform.position = initialPosition + new Vector3(0, tmp * floatH, 0);
            }
        }

        float EaseInOutQuad(float tmp)
        {
            return tmp < 0.5f ? 2 * tmp * tmp : 1 - Mathf.Pow(-2 * tmp + 2, 2) / 2;
        }
    }
}

