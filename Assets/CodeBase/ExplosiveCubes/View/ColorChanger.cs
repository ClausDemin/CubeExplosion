using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveCubes.View
{
    public class ColorChanger: MonoBehaviour
    {
        private MeshRenderer _renderer;

        private void Start()
        {
            SetRandomColor();
        }

        private void SetRandomColor()
        {
            if (gameObject.TryGetComponent(out _renderer))
            {
                float redComponent = UserUtils.GetRandomFloat();
                float greenComponent = UserUtils.GetRandomFloat();
                float blueComponent = UserUtils.GetRandomFloat();

                Color randomColor = new Color(redComponent, greenComponent, blueComponent);

                _renderer.material.color = randomColor;
            }
        }
    }
}
