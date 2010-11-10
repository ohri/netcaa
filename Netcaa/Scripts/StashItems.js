function StashItems( ctrlSource, ctrlTarget )
{
    contents = "";

    var source = document.getElementById(ctrlSource);
    var target = document.getElementById(ctrlTarget);
    
    if( source == null )
    {
        alert( "source is null" );
    }
    if( target == null )
    {
        alert( "target is null" );
    }
    for( i = 0; i < source.length; i++ )
    {
        contents = contents + source[i].value + ";";
        alert( "found one: " + source[i].value );
    }
    target.value = contents;
}