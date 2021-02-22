using System;
using System.Collections.Generic;
using System.Windows;
using FifteenPuzzle.Helpers;

namespace FifteenPuzzle.AI
{
    /// <summary>
    ///     Содержит методы для поиска и построения решения пятнашек
    /// </summary>
    internal static class Ai
    {
        private static readonly Board Board = Board.Instance;
        private static int minPrevIteration, deepness;
        private static Stack<Direction> wayStack;
        private static int[] goalX, goalY;

        /// <summary>
        ///     Пытается построить последовательность шагов к цели
        /// </summary>
        /// <param name="startBoard">Начальное состояние доски</param>
        /// <param name="result">Содержит последовательность шагов к цели в случае успеха. Иначе - null</param>
        /// <returns>Результат поиска решения</returns>
        public static SolvingResult BuildSolution(int[,] startBoard, out Direction[] result)
        {
            Board.Create(startBoard);

            goalX = new int[Board.BlocksCount];
            goalY = new int[Board.BlocksCount];
            InitGoalArrays();

            wayStack = new Stack<Direction>();
            result = null;

            if (!IsSolvable(startBoard))
                return SolvingResult.Unsolvable;
            if (GetEstimate() == 0)
                return SolvingResult.AlreadyDone;
            if (!IdaStar())
                return SolvingResult.IdaStarError;

            result = wayStack.ToArray();
            return SolvingResult.SolveFound;
        }

    }
}