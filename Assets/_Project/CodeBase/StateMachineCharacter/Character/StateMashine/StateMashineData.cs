﻿using System;

namespace Assets.ProjectLesson2.Scripts.Character.StateMashine
{
    public class StateMashineData
    {
        public float XVelocity;
        public float YVelocity;

        private float _speed;
        private float _xInput;

        public float XInput
        {
            get => _xInput;
            set
            {
                if (_xInput < -1 || _xInput > 1)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _xInput = value;
            }
        }

        public float Speed
        {
            get => _speed;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _speed = value;
            }
        }
    }
}