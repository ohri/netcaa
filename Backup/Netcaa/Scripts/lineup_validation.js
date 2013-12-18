function get_player_position( player_text )
{
    var pos = player_text.split( " " );
    return pos[pos.length - 3];
}

function lineup_validate( pos_text, lineup_spot )
{
    // NOTE: position validation turned off because the rules aren't
    // nearly as easy for netcaa
    //
    // dupe checking still works though


    //var pos = get_player_position( String( pos_text ) );
    var retval = "";

    // check player positions
//    if ( lineup_spot <= 10 )  // no need to check garbage for positions
//    {
//        if ( lineup_spot > 5 )
//        {
//            retval = validate_position( lineup_spot - 5, pos ); // shift for backups
//        }
//        else
//        {
//            retval = validate_position( lineup_spot, pos );
//        }
//    }

    // if that's ok, check for duplicates
    if ( retval.length == 0 )
    {
        retval = dupe_check( pos_text, lineup_spot );
    }

    return retval;
}

function validate_position( position, player_position )
{
    var ret = "";
    switch ( position )
    {
        case 1:
            if ( player_position != 'G' )
            {
                ret = "Only G are allowed at PG";
            }
            break;
        case 2:
            if ( player_position != 'G' && player_position != 'GF' && player_position != 'FG' )
            {
                ret = "Only G and GF are allowed at SG";
            }
            break;
        case 3:
            if ( player_position != 'G' && player_position != 'F' && player_position != 'GF' && player_position != 'FG' )
            {
                ret = "Only G, F and GF are allowed at SF";
            }
            break;
        case 4:
            if ( player_position != 'F' && player_position != 'FC' )
            {
                ret = "Only F and FC are allowed at PF";
            }
            break;
        case 5:
            if ( player_position != 'C' && player_position != 'CF' && player_position != 'FC' && player_position != 'F' )
            {
                ret = "Only F, C, and FC are allowed at C";
            }
            break;
    }
    return ret;
}

function dupe_check( pos_text, cur_index )
{
    var dupe_found = false;

    for ( var i = 0; i < 12; i++ )
    {
        if ( i != ( cur_index - 1 ) && $( pos_labels[i] ).html() == pos_text )
        {
            dupe_found = true;
            break;
        }
    }

    if ( dupe_found )
    {
        return "Duplicate!";
    }
    else
    {
        return "";
    }
}
