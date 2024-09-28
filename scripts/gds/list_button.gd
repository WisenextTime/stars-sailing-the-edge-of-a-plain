extends Button


func _on_toggled(toggled_on: bool) -> void:
	if toggled_on:
		get_node("../../..").set("Map",self.tooltip_text)
