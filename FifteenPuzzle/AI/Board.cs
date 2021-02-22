using System.Windows;

namespace FifteenPuzzle.AI
{
    /// <summary>
    /// Содержит методы для работы с доской
    /// </summary>
    class Board
    {
        private int[,] board;

        public static int BlocksPerLine { get; private set; }
        public static int BlocksCount { get; private set; }

        private static Board instance;

        private Board() { }

        public static Board Instance => instance ?? (instance = new Board());

        /// <summary>
        /// Производит инициализацию доски
        /// </summary>
        /// <param name="startBoard">Начальное расположение пятнашек</param>
        public void Create(int[,] startBoard)
        {
            board = startBoard;
            BlocksPerLine = board.GetLength(0);
            BlocksCount = BlocksPerLine * BlocksPerLine;
        }
    }
}
