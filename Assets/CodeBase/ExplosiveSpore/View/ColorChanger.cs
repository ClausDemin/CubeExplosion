using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.CodeBase.ExplosiveSpore.View
{
    public class ColorChanger
    {
        public void SetRandomColor(MeshRenderer renderer)
        {
            float randomRed = UserUtils.GetRandomFloat();
            float randomBlue = UserUtils.GetRandomFloat();
            float randomGreen = UserUtils.GetRandomFloat();
            float alpha = 1;

            Color cubeColor = new Color(randomRed, randomGreen, randomBlue, alpha);

            renderer.material.SetColor("_Color", cubeColor);
        }
    }
}
