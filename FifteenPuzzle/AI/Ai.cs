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

        // Проверяет разрешимость пятнашек
        public static bool IsSolvable(int[,] startBoard)
        {
            Board.Create(startBoard);

            var count = 0;
            var transpos = 0;

            var ar = new int[Board.BlocksCount];

            for (var i = 0; i < Board.BlocksPerLine; i++)
            {
                int value;
                if (i % 2 == 0)
                {
                    for (var j = 0; j < Board.BlocksPerLine; j++)
                    {
                        value = Board[i, j];
                        if (value > 0)
                        {
                            ar[count] = value;
                            count++;
                        }
                    }
                }
                else
                {
                    for (var j = Board.BlocksPerLine - 1; j >= 0; j--)
                    {
                        value = Board[i, j];
                        if (value > 0)
                        {
                            ar[count] = value;
                            count++;
                        }
                    }
                }
            }
            for (var i = 0; i < count - 1; i++)
            {
                for (var j = i + 1; j < count; j++)
                {
                    if (ar[i] > ar[j]) transpos++;
                }
            }

            return transpos % 2 == 1;
        }

        // Проверяет, является ли доска startBoard уже собранной
        public static bool IsAlreadySolved(int[,] startBoard)
        {
            Board.Create(startBoard);
            goalX = new int[Board.BlocksCount];
            goalY = new int[Board.BlocksCount];
            InitGoalArrays();

            return GetEstimate() == 0;
        }

        // Алгоритм поиска IDA*
        private static bool IdaStar()
        {
            const int infinity = int.MaxValue;
            const int maxDeepness = 50;

            var result = false;
            deepness = GetEstimate();

        }


        }
    }