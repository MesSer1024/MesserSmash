function WaveSpawner(enemyType, amount) {
	return {
	   EnemyType : enemyType,
	   SpawnCount : amount,
	   Criteria : {
			MaxEnemiesAlive : -1,
			MinSecondsInArena : -1,
			MinTotalEnemiesKilled : -1,
			WaveRepeatableCount : 0,
			SecondsBetweenRepeat : 1
		}
	}
};

function WaveView(model, index) {
	var _model = model;
	var _index = index;
	
	var clone = jQuery.extend({index: index}, model);
	var template = Handlebars.template(MesserEntertainment["asdf\\WaveSpawnView"])(clone);
	var _content = $(template);

	console.log("Model:", model);
	console.log("Content:", _content);
	console.log("-------------");
	
	var _button = _content.find('button');
	var dropdown = $(createDropdown(model.EnemyType));
	dropdown.appendTo(_content.find("#drop"));
	
	//add event handlers for input change
	dropdown.change(onEnemyType);
	_content.find("#spawn_count").change(onSpawnCount);
	_content.find("#max_enemies").change(onMaxEnemies);
	_content.find("#min_seconds").change(onMinSeconds);
	_content.find("#min_kill").change(onMinKills);

	function createDropdown(selectedType) {
		var s = "<select class=\"value\">";
		for(var i in EnemyTypes) {
			if( EnemyTypes[i] === selectedType )
				s += "<option selected>" + i + "</option>";
			else
				s += "<option>" + i + "</option>";
		}
		s += "</select>";
		return s;
	};
	
	function hide() {
		console.log("hide idx=", _index, this);
		var c = _content.children().not(_button).not(".waveheader")
		console.log("c=", c);
		c.toggle();
	};

	function getContent() {
		return _content;
	};

	function getButton() {
		return _button;
	};

	function onEnemyType() {
		var value = this.selectedIndex;
		console.log("[" + _index + "]onEnemyType value=" + value);
		_model.EnemyType = value;
		console.log(_model);
	};
	
	function onSpawnCount() {
		var value = $(this).val();
		console.log("[" + _index + "]onSpawnCount value=" + value);
		_model.SpawnCount = value;
		console.log(_model);
	};
	
	function onMaxEnemies() {
		var value = $(this).val();
		console.log("[" + _index + "]onMaxEnemies value=" + value);
		_model.Criteria.MaxEnemiesAlive = value;
		console.log(_model);
	};
	
	function onMinSeconds() {
		var value = $(this).val();
		console.log("[" + _index + "]onMinSeconds value=" + value);
		_model.Criteria.MinSecondsInArena = value;
		console.log(_model);
	};
	
	function onMinKills() {
		var value = $(this).val();
		console.log("[" + _index + "]onMinKills value=" + value);
		_model.Criteria.MinTotalEnemiesKilled = value;
		console.log(_model);
	};
	
	return {
		getContent: getContent,
		getButton: getButton,
		hide: hide
	}
};