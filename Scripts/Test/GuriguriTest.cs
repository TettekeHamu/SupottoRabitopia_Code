using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PullAnimals.Test
{
    public class GuriguriTest : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;

        [SerializeField] private int _count;

        private float _beforeInput;

        private void Awake()
        {
            _count = 0;
            _beforeInput = 0;
        }

        private void Update()
        {
            var inp = _playerInput.actions["PlayerPull"].ReadValue<Vector2>();
            if (inp.x * _beforeInput < 0) _count++;
            _beforeInput = inp.x;
        }
    }
}
