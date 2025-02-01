using Assets.CodeBase.ExplosiveCubes.Model.Interfaces;

namespace Assets.CodeBase.ExplosiveCubes.Model.Infrastructure
{
    public class SeparableEntityFactory : ISeparableEntityFactory
    {
        private float _baseSeparateChance;
        private float _separateOverGenerationFactor;
        private int _minChildCount;
        private int _maxChildCount;

        public SeparableEntityFactory(float baseSeparateChance, float separateOverGenerationFactor, int minChildCount, int maxChildCount)
        {
            _baseSeparateChance = baseSeparateChance;
            _separateOverGenerationFactor = separateOverGenerationFactor;
            _minChildCount = minChildCount;
            _maxChildCount = maxChildCount;
        }

        public ISeparableEntity Create(int generation)
        {
            SeparableEntity entity =
                new SeparableEntity(_baseSeparateChance, _separateOverGenerationFactor, _minChildCount, _maxChildCount, generation);

            return entity;
        }
    }
}
