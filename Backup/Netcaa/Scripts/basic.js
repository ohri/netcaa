    function JumpTR( selection, destination ) 
    {
        var tempIndex, selectedURL;
        tempIndex = selection.selectedIndex;
        if( selection.options[tempIndex].value != 'A' &&
            selection.options[tempIndex].value != '-1')
        {
            selectedURL = destination + selection.options[tempIndex].value;
            window.top.location.href = selectedURL; 
        }
    }
    
    var prev;
    var lastId;
    
    function playerInfo(id, data) 
    {
        var element;
     
        if(lastId) 
        {
            element = document.getElementById(lastId);
            element.innerHTML = prev;
        }
        element = document.getElementById(id);
        lastId = id;
        prev = element.innerHTML;
        element.innerHTML = data;
    }
    
    	function makeWin(URL, w, h ) 
    	{
    	    day = new Date();
    	    id = day.getTime();
    	    eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=" 
    	    + w + ",height=" + h + "');");
    	}
    
   
        
        function JumpURL( destination )
        {
            window.top.location.href = destination;
    }