using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class PassiveDamage : MonoBehaviour
    {
        [SerializeField] private DamageDealer _damageDealer;

        private IHealth<ScientificNotation> _enemyHealth;

        [Inject]
        private void Construct(IHealth<ScientificNotation> enemyHealth)
        {
            _enemyHealth = enemyHealth;
        }

        private void Update()
        {
            _enemyHealth.GetDamage(_damageDealer.Value * Time.deltaTime);
        }
    }
}
