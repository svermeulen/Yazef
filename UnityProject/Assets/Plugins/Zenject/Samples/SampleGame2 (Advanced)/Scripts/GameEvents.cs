using System;

namespace Zenject.SpaceFighter
{
    public class GameEvents
    {
        public Action EnemyKilledSignal = delegate{};
        public Action PlayerDiedSignal = delegate{};
    }
}
