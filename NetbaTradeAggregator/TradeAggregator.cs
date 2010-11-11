using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.IO;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate( Format.UserDefined, MaxByteSize=8000 )]
public struct TradeAggregator : IBinarySerialize
{
    private List<string> values;

    public void Init()
    {
        values = new List<string>();
    }

    public void Accumulate( SqlString Value )
    {
        values.Add( Value.Value );
    }

    public void Merge( TradeAggregator Group )
    {
        values.AddRange( Group.values );
    }

    public SqlString Terminate()
    {
        // Put your code here
        return new SqlString( string.Join( "<br>", values.ToArray() ) );
    }

    public void Read( BinaryReader r )
    {
        int itemCount = r.ReadInt32();
        this.values = new List<string>( itemCount );
        for ( int i = 0; i <= itemCount - 1; i++ )
        {
            this.values.Add( r.ReadString() );
        }
    }

    public void Write( BinaryWriter w )
    {
        w.Write( this.values.Count );
        foreach ( string s in this.values )
        {
            w.Write( s );
        }
    }

    // This is a place-holder member field
    private int var1;

}
