//From codebase
var EnemyTypes = {
	Melee : 0,
	Range : 1,
	Charger : 2
}

//'global' variables
var cells = [];
var waves = [];

function Tester() {
	this.testBasicFunctionality = function() {
		console.log("Total waves: " + waves.length);

		var c0 = waves[0].Criteria;
		c0.MinTotalEnemiesKilled = 9;
		c0.MinSecondsInArena = 15;

		console.log("EnemyType: " + waves[0].EnemyType);
		console.log("SpawnCount: " + waves[0].SpawnCount);
		console.log(waves[0].Criteria);

		console.log("EnemyType: " + waves[1].EnemyType);
		console.log("SpawnCount: " + waves[1].SpawnCount);
		console.log(waves[1].Criteria);	
	};
};

window.onload = function() {

	var table = document.getElementById('levels');

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

	new Tester().testBasicFunctionality();

	// bind a click-handler to the 'tr' elements with the 'header' class-name:
	$('tr.header').click(function(){
	    /* get all the subsequent 'tr' elements until the next 'tr.header',
	       set the 'display' property to 'none' (if they're visible), to 'table-row'
	       if they're not: */
	    $(this).nextUntil('tr.header').css('display', function(i,v){
	        return this.style.display === 'table-row' ? 'none' : 'table-row';
	    });
	});

	function createWave() {
		$("#levels").append("<tr class=\"header\"><td>{wave}</td></tr>");
		$("#levels").append("<tr><td>EnemyType:</td><td>{enemy_type}</td></tr>");
		$("#levels").append("<tr><td>SpawnCount:</td><td>{spawn_count}</td></tr>");
	}	

	createWave();
}
