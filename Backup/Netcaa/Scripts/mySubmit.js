function mySubmit(select) 
{
//    for  (var i=0;i<select.length;i++)
//    {
//        select[i].selected=true;
//    }
    var listBoxOps = select.options;
    for  (var i=0;i<listBoxOps.length;i++)
    {
        listBoxOps[i].selected=true;
    }
//    listBoxOps.submit();

//     var iLoop;
//     for (iLoop = 0; iLoop < document.ListEditor.SelectedPlayers.length; iLoop++) 
//     {
//      	document.ListEditor.SelectedPlayers[iLoop].selected = true;
//     }
//     document.ListEditor.submit();

}

