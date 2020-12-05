namespace AdventOfCode.Five
{
    public class BoardingPass
    {
        public int Column;
        public int Row;

        public BoardingPass(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int GetSeat()
        {
            return Row * 8 + Column;
        }
    }
}