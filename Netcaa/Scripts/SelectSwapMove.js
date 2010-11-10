//<script language="javascript"> 
 
function moveIt( index, select ) 
{ 
	if( select.length >= 2 ) 
	{ 
		var item	= select.selectedIndex; 
		var value	= select[item+index].value; 
		var text	= select[item+index].text; 
 
		select[item + index].value = select[item].value; 
		select[item + index].text  = select[item].text; 
		select[item].value         = value; 
		select[item].text          = text; 
		select.selectedIndex      += index; 
	} 
	else 
	{ 
		alert( "You need at least two items, so you can change their order." ); 
	} 
} 
 
function addIt( fieldTo, fieldFrom ) 
{ 
	if( fieldFrom.selectedIndex >= 0 ) 
	{ 
 		for( var i = fieldFrom.selectedIndex; i < fieldFrom.length; i++ ) 
		{ 
 			var skip = false; 
 
			if( fieldFrom[i].selected ) 
			{ 
				if( i == -1 || fieldFrom[i].value == '--' ) 
				{ 
					return; 
				} 
 
				for( var j = 0; j < fieldTo.options.length; j++ ) 
				{ 
					if( fieldFrom[i].text == fieldTo[j].text ) 
					{ 
						alert( "'" + fieldFrom[i].text + "' is already in your list." ); 
						skip = true; 
						break; 
					} 
				} 
 
				if( skip ) 
				{ 
					continue; 
				} 
 
				var o = new Option( fieldFrom[i].text, fieldFrom[i].value ); 
				fieldTo.options[fieldTo.options.length] = o; 
			} 
		} 
	} 
	else 
	{ 
 		alert ( "Please select item to be added." ); 
 	}
	 
} 
 
function removeIt( fieldFrom ) 
{ 
	if( fieldFrom.selectedIndex >= 0 ) 
	{ 
		for( var i = fieldFrom.length-1, s = fieldFrom.selectedIndex; i >=s ; i-- ) 
		{ 
			if( fieldFrom.options[i].selected ) 
			{ 
				fieldFrom.options[i] = null; 
			} 
		} 
 	} 
	else 
	{ 
		alert( "Please select item to be removed." ); 
	} 
} 
 
function transferIt( fieldTo, fieldFrom )
{
	addIt( fieldTo, fieldFrom );
	removeIt( fieldFrom );
}

//</script>
