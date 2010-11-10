using System;
using System.Reflection;

namespace TextRepresentation
{
	public class TextRepAttribute : Attribute
	{
		public readonly string Text;
		public TextRepAttribute( string text )
		{
			Text = text;
		}
	}

	public class TextualRepresentation
	{
		public static string GetDescription( Enum e )
		{
			string ret = "";
			Type t = e.GetType();
			MemberInfo[] members = t.GetMember(e.ToString());
			if( members != null && members.Length == 1 )
			{
				object[] attrs = members[0].GetCustomAttributes(typeof(TextRepAttribute), false);
				if( attrs.Length == 1 )
				{
					ret = ((TextRepAttribute)attrs[0]).Text;
				}
			}
			return ret;
		}

		static object GetEnumValue(string text, Type enumType)
		{
			MemberInfo[] members = enumType.GetMembers(); 
			foreach( MemberInfo mi in members ) 
			{ 
				object[] attrs = mi.GetCustomAttributes(typeof(TextRepAttribute), false); 
				if( attrs.Length == 1 ) 
				{ 
					if( ((TextRepAttribute)attrs[0]).Text == text ) 
						return Enum.Parse(enumType, mi.Name); 
				} 
			} 
			throw new ArgumentOutOfRangeException("text", text, "The text passed does not correspond to an attributed enum value"); 
		}

	}

}
