using Assets.Scripts.Utils;
using System;

namespace Assets.CodeBase.ExplosiveCubes.Model
{
    public class SeparableEntity : ISeparableEntity
    {
        private float _baseSeparateChance;
        private float _separateChanceOverGenerationFactor;

        private int _minChildCount;
        private int _maxChildCount;

        public SeparableEntity
        (
            float baseSeparateChance,
            float separateChanceOverGenerationFactor,
            int minChildCount,
            int maxChildCount,
            int generation
        )
        {
            _baseSeparateChance = baseSeparateChance;
            _separateChanceOverGenerationFactor = separateChanceOverGenerationFactor;

            _minChildCount = minChildCount;
            _maxChildCount = maxChildCount;

            Generation = generation;
        }

        public int Generation { get; private set; }

        public bool TrySeparate(out int childCount)
        {
            if (UserUtils.GetRandomBool(GetSeparateChance()) == false)
            {
                childCount = -1;

                return false;
            }

            childCount = GetChildCount(_minChildCount, _maxChildCount);

            return true;
        }

        private float GetSeparateChance()
        {
            return _baseSeparateChance * (float)(Math.Pow(_separateChanceOverGenerationFactor, Generation));
        }

        private int GetChildCount(int minChildCount, int maxChildCount)
        {
            if (minChildCount > maxChildCount)
            {
                throw new ArgumentException();
            }

            return UserUtils.GetRandomInt(minChildCount, maxChildCount);
        }
    }
}
