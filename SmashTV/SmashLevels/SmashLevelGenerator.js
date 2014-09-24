//From codebase
var EnemyTypes = {
	Melee : 0,
	Range : 1,
	Charger : 2
}

//'global' variables
var cells = [];
var waves = [];

window.onload = function() {

	var table = document.getElementById('board');

	function SpawnCriteria() {
        this.MaxEnemiesAlive = -1;
        this.MinSecondsInArena = -1;
        this.MinTotalEnemiesKilled = -1;
        this.WaveRepeatableCount = 0;
        this.SecondsBetweenRepeat = 1;
	}; 

	function WaveSpawner(enemyType, amount) {
	   this.EnemyType = enemyType;
	   this.SpawnCount = amount;
	   this.Criteria = new SpawnCriteria();
	};

	waves.push(new WaveSpawner(EnemyTypes.Melee, 37));
	waves.push(new WaveSpawner(EnemyTypes.Range, 21));
	var c0 = waves[0].Criteria;
	c0.MinTotalEnemiesKilled = 9;
	c0.MinSecondsInArena = 15;

	console.log(waves[0].EnemyType);
	console.log(waves[0].EnemyAmount);
	console.log(waves[0].Criteria);

	console.log(waves[1].EnemyType);
	console.log(waves[1].EnemyAmount);
	console.log(waves[1].Criteria);
}