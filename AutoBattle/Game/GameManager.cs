using AutoBattle.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBattle.Game
{
    public class GameManager
    {
        private static GameManager _instance;

        private CharacterManager _characterManager;
        private GridManager _gridManager;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        private int _currentTurn = 0;
        //private int numberOfPossibleTiles = grid.grids.Count;
    }
}