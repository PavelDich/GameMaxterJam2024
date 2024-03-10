using System.Collections;
using UnityEngine;
using Mirror;
using Unity.Mathematics;
using System;
using UnityEngine.Events;

namespace Minicop.Library.Stats
{
    [Serializable]
    public class Parameter
    {
        public Parameter() =>
            OnValidate.AddListener(Validate);
        [HideInInspector]
        public static UnityEvent OnValidate = new UnityEvent();

        protected void Validate()
        {
            ChangeValue(_valueInspector);
            ChangeMax(_maxInspector);
        }

        [SerializeField]
        protected float _maxInspector = 100f;
        [SerializeField, HideInInspector]
        protected float _max;
        public virtual float Max
        {
            get => _max;
            set
            {
                if (Application.isPlaying)
                    value = math.max(value, Max);
                else value = math.max(value, 0);

                float oldValue = _max;
                _max = value;
                _maxInspector = value;

                Value = Value / oldValue * value;
            }
        }

        public void ChangeMax(float newValue)
        {
            float oldValue = Max;
            Max = newValue;
            OnChenges.Max.Invoke(oldValue, newValue);
        }

        [SerializeField]
        protected float _valueInspector = 100f;
        [SerializeField, HideInInspector]
        protected float _value;
        public virtual float Value
        {
            get => _value;
            set
            {
                value = math.min(value, Max);
                value = math.max(value, 0);

                _value = value;
                _valueInspector = value;
            }
        }

        public void ChangeValue(float newValue)
        {
            float oldValue = Value;
            Value = newValue;
            OnChenges.Value.Invoke(oldValue, newValue);
        }



        protected Coroutine Regenerator;
        public void Regeneration(MonoBehaviour monoBehaviour)
        {
            if (Regenerator != null)
                monoBehaviour.StopCoroutine(Regenerator);
            if (Application.isPlaying)
                Regenerator = monoBehaviour.StartCoroutine(Start());

            IEnumerator Start()
            {
                yield return new WaitForSeconds(RegenerationDelay);
                while (Value < Max && RegenerationPerSecond != 0)
                {
                    yield return new WaitForEndOfFrame();
                    ChangeValue(Value + RegenerationPerSecond * Time.deltaTime);
                }
                yield break;
            }
        }

        [field: SerializeField]
        public virtual float RegenerationPerSecond { get; protected set; } = 0;
        [field: SerializeField]
        public virtual float RegenerationDelay { get; protected set; } = 0;

        public Events OnChenges;
        [Serializable]
        public struct Events
        {
            public UnityEvent<float, float> Max;
            public UnityEvent<float, float> Value;
        }
    }
}
