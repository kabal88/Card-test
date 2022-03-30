using UnityEditor;
using UnityEngine;
using View;

namespace Helpers
{
    [CustomEditor(typeof(HandView))]
    public class HandViewEditor : Editor
    {
        private HandView _handView;

        public virtual void OnSceneGUI()
        {
            _handView = (HandView) target;
            var from = GetPoint(_handView.AngleOffset, _handView.Radius);
            Handles.color = Color.blue;
            Handles.DrawWireArc(
                _handView.transform.position,
                Vector3.forward,
                from,
                _handView.Angle,
                _handView.Radius);

            Handles.color = Color.magenta;
            var angle = _handView.Angle;

            if (_handView.CardCount > 1)
            {
                angle /= (_handView.CardCount - 1);

                for (var i = 0; i < _handView.CardCount; i++)
                {
                    var angleTemp = angle * i + _handView.AngleOffset;
                    var pointPos = (Vector3) GetPoint(angleTemp, _handView.Radius);
                    var position = _handView.transform.position + pointPos;
                    Handles.DrawSolidDisc(position, Vector3.forward, 0.1f);
                }
            }
            else if (_handView.CardCount == 1)
            {
                angle /= 2;
                var angleTemp = angle + _handView.AngleOffset;
                var pointPos = (Vector3) GetPoint(angleTemp, _handView.Radius);
                var position = _handView.transform.position + pointPos;
                Handles.DrawSolidDisc(position, Vector3.forward, 0.1f);
            }
        }

        private Vector2 GetPoint(float angle, float radius)
        {
            angle *= Mathf.Deg2Rad;

            var x = radius * Mathf.Cos(angle);
            var y = radius * Mathf.Sin(angle);

            var result = new Vector2(x, y);

            return result;
        }
    }
}