using AutoBattle.Game;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle.Characters
{
    public abstract class BaseCharacter
    {
        public GridBox currentBox;
        private readonly bool _isPlayer;
        private bool _isDead;
        private readonly GameManager _gameManager;

        public string Name { get; set; }
        public float Health { get; set; }
        public float BaseDamage { get; set; }
        public float DamageMultiplier { get; set; }
        public int PlayerIndex { get; set; }
        public Character Target { get; set; }

        public bool IsPlayer
        { get { return _isPlayer; } }

        public bool IsDead
        { get { return _isDead; } }
    }
}