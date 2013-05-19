using MesserSmash.Enemies;
namespace MesserSmash.Commands {
    public class AttackPlayerCommand : Command {
        public const string NAME = "AttackPlayerCommand";
        private IEnemy _enemy;
        private Player _player;

        public AttackPlayerCommand(IEnemy enemy, Player player) : base(NAME) {
            _enemy = enemy;
            _player = player;
        }

        public IEnemy Enemy {
            get { return _enemy; }
        }

        public Player Player {
            get { return _player; }
        }
    }
}
