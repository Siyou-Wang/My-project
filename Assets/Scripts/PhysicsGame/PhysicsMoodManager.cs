using System;
using UnityEngine;

namespace PhysicsGame
{
    public class PhysicsMoodManager : MonoBehaviour
    {
        private const int AngerPerHit = 1;
        private const int CalmPerTick = 1;
        private const float SecondsPerCalmTick = 5f;
        private const float SurviveSeconds = 5f;
        
        [SerializeField] private CorneliusCollision cornelius;

        public static bool Won {get; private set;}
        
        private float _calmTimer;
        private float _surviveTimer;

        private void OnEnable() => cornelius.Hit += OnHit;
        private void OnDisable() => cornelius.Hit -= OnHit;

        private void Update()
        {
            if (Won) return;

            if (!StartGameTp.StartPhys)
            {
                _surviveTimer = 0f;
                _calmTimer = 0f;
                return;
            }
            
            _surviveTimer += Time.deltaTime;
            if (_surviveTimer >= SurviveSeconds)
            {
                Won = true;
                StartGameTp.StartPhys = false;
                Camera.main.transform.position = StartGameTp.MainScreen;
                return;
            }
            
            _calmTimer += Time.deltaTime;
            if (_calmTimer < SecondsPerCalmTick) return;
            
            _calmTimer -= SecondsPerCalmTick;
            AngerAndCalm.setState(CalmPerTick);
        }

        private void OnHit()
        {
            AngerAndCalm.setState(-AngerPerHit);
            _calmTimer = 0f; 
        }
    }
}
