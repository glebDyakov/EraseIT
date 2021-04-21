#target photoshop

function getSelectedLayers() {
	var selectedLayers = [];
	try {
		var backGroundCounter = activeDocument.artLayers[activeDocument.artLayers.length-1].isBackgroundLayer ? 0 : 1;
		var ref = new ActionReference();
		var keyTargetLayers = app.stringIDToTypeID( 'targetLayers' );
		ref.putProperty( app.charIDToTypeID( 'Prpr' ), keyTargetLayers );
		ref.putEnumerated( app.charIDToTypeID( 'Dcmn' ), app.charIDToTypeID( 'Ordn' ), app.charIDToTypeID( 'Trgt' ) );
		var desc = executeActionGet( ref );
		if ( desc.hasKey( keyTargetLayers ) ) {
			var layersList = desc.getList( keyTargetLayers );
			for ( var i = 0; i < layersList.count; i++) {
				var listRef = layersList.getReference( i );
				selectedLayers.push( listRef.getIndex() + backGroundCounter );
			}
		}
	}catch(e) {
	
	}
	return selectedLayers;
}

function setSelectedLayer( layerIndexOrName ) {
	try {
		var id239 = charIDToTypeID( "slct" );
		var desc45 = new ActionDescriptor();
		var id240 = charIDToTypeID( "null" );
		var ref43 = new ActionReference();
		var id241 = charIDToTypeID( "Lyr " );
		if ( typeof layerIndexOrName == "number" ) {
			ref43.putIndex( id241, layerIndexOrName );
		} else {
			ref43.putName( id241, layerIndexOrName );
		}
		desc45.putReference( id240, ref43 );
		var id242 = charIDToTypeID( "MkVs" );
		desc45.putBoolean( id242, false );
		executeAction( id239, desc45, DialogModes.NO );
	}
	catch(e) {
		alert(e + ":" + e.line); // do nothing
	}
}

var selectedLayers = getSelectedLayers();

for (var i = 0; i < selectedLayers.length; ++i) {

	setSelectedLayer(selectedLayers[i]);
	var layer = activeDocument.activeLayer;
	
	var left = layer.bounds[0].value;
	var right = layer.bounds[2].value;
	var top = layer.bounds[1].value;
	var bottom = layer.bounds[3].value;

	var region1 = [
		[left, top],
		[right, top],
		[right, bottom],
		[left, bottom],
		[left, top]
	];

	var newColor1 = new SolidColor();
	newColor1.rgb.red = 0.0;
	newColor1.rgb.green = 0.0;
	newColor1.rgb.blue = 0.0;

	activeDocument.selection.select(region1);
	activeDocument.selection.clear();
	activeDocument.selection.fill(newColor1, ColorBlendMode.NORMAL, 2.0);
	activeDocument.selection.deselect(); 
}

