this["MesserEntertainment"] = this["MesserEntertainment"] || {};
this["MesserEntertainment"]["asdf\\WaveSpawnView"] = {"compiler":[6,">= 2.0.0-beta.1"],"main":function(depth0,helpers,partials,data) {
  var stack1, helper, functionType="function", helperMissing=helpers.helperMissing, escapeExpression=this.escapeExpression, lambda=this.lambda;
  return "<div class=\"wave\">\r\n	<div class=\"waveheader\">\r\n		<button>+/-</button>\r\n		<label>Wave_"
    + escapeExpression(((helper = (helper = helpers.index || (depth0 != null ? depth0.index : depth0)) != null ? helper : helperMissing),(typeof helper === functionType ? helper.call(depth0, {"name":"index","hash":{},"data":data}) : helper)))
    + "</label>\r\n	</div>\r\n	<div>\r\n		<div id=\"drop\" class=\"keyvaluepair\">\r\n			<label class=\"key\">EnemyType</label>\r\n		</div>	\r\n		<div class=\"keyvaluepair\">\r\n			<label class=\"key\">SpawnCount</label>\r\n			<input id=\"spawn_count\" class=\"value\" type=\"text\" value=\""
    + escapeExpression(((helper = (helper = helpers.SpawnCount || (depth0 != null ? depth0.SpawnCount : depth0)) != null ? helper : helperMissing),(typeof helper === functionType ? helper.call(depth0, {"name":"SpawnCount","hash":{},"data":data}) : helper)))
    + "\" />\r\n		</div>\r\n		<div class=\"keyvaluepair\">\r\n			<label class=\"key\">MaxEnemiesAlive</label>\r\n			<input id=\"max_enemies\" class=\"value\" type=\"text\" value=\""
    + escapeExpression(lambda(((stack1 = (depth0 != null ? depth0.Criteria : depth0)) != null ? stack1.MaxEnemiesAlive : stack1), depth0))
    + "\" />	\r\n		</div>\r\n		<div class=\"keyvaluepair\">\r\n			<label class=\"key\">MinSecondsInArena</label>\r\n			<input id=\"min_seconds\" class=\"value\" type=\"text\" value=\""
    + escapeExpression(lambda(((stack1 = (depth0 != null ? depth0.Criteria : depth0)) != null ? stack1.MinSecondsInArena : stack1), depth0))
    + "\" />	\r\n		</div>\r\n		<div class=\"keyvaluepair\">\r\n			<label class=\"key\">MinEnemiesKilled</label>\r\n			<input id=\"min_kill\" class=\"value\" type=\"text\" value=\""
    + escapeExpression(lambda(((stack1 = (depth0 != null ? depth0.Criteria : depth0)) != null ? stack1.MinTotalEnemiesKilled : stack1), depth0))
    + "\" />		\r\n		</div>\r\n	</div>\r\n</div>";
},"useData":true};