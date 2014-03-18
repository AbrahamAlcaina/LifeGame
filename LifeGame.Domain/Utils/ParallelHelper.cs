namespace LifeGame.Domain.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class ParallelHelper
    {
        #region Public Methods and Operators

        public static void While(Func<bool> condition, Action action)
        {
            Parallel.ForEach(WhileTrue(condition), _ => action());
        }

        #endregion

        #region Methods

        private static IEnumerable<bool> WhileTrue(Func<bool> condition)
        {
            while (condition())
            {
                yield return true;
            }
        }

        #endregion
    }
}