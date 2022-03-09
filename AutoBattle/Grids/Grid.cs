using System;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>(); // Change to private
        private readonly int _xLength;
        private readonly int _yLength;

        public int XLenght
        { get { return _xLength; } }

        public int YLength
        { get { return _yLength; } }

        public List<GridBox> Grids
        { get { return grids; } }

        public Grid(int Lines, int Columns)
        {
            _xLength = Lines;
            _yLength = Columns;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (Columns * i + j));
                    grids.Add(newBox);
                    Console.Write($"{newBox.Index}\n");
                }
            }
        }

        public GridBox GetGridBoxByIndex(int index)
        {
            return grids.FirstOrDefault(cell => cell.Index == index);
        }
    }
}