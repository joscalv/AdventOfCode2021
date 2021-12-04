namespace AdventOfCode2021.Day4
{
    public class BingoCard
    {
        private int[][] _card;
        private bool[][] _marked;

        public BingoCard(int[][] card)
        {
            _card = card;
            _marked = card.Select(line => line.Select(value => false).ToArray()).ToArray();
            SumUnmarked = card.SelectMany(l => l).Sum();
        }

        public int SumMarked { get; set; }
        public int SumUnmarked { get; set; }
        public int LastValue { get; set; }
        public bool IsCompleted { get; private set; }

        public bool Play(int value)
        {
            if (IsCompleted)
            {
                return true;
            }

            for (int line = 0; line < _card.Length; line++)
            {
                for (int column = 0; column < _card[line].Length; column++)
                {
                    if (_card[line][column] == value && !_marked[line][column])
                    {
                        _marked[line][column] = true;
                        SumMarked = SumMarked + value;
                        SumUnmarked = SumUnmarked - value;
                        LastValue = value;
                        IsCompleted = CheckCompleted();
                    }
                }
            }

            return IsCompleted;
        }

        public bool CheckCompleted()
        {
            if (IsCompleted)
            {
                return true;
            }

            for (int line = 0; line < _card.Length; line++)
            {
                bool isRowCompleted = true;
                for (int column = 0; column < _card[line].Length; column++)
                {
                    isRowCompleted = isRowCompleted && _marked[line][column];

                }
                if (isRowCompleted)
                {
                    return true;
                }
            }

            for (int column = 0; column < _card[0].Length; column++)
            {
                bool isColumnCompleted = true;
                for (int line = 0; line < _card.Length; line++)
                {
                    isColumnCompleted = isColumnCompleted && _marked[line][column];
                }
                if (isColumnCompleted)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
