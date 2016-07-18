/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/18/2016
 * Time: 2:43 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Rooko.Core;

namespace HW.Grp.Migrations
{
	public class AddLangIDToUserSession : Migration
	{
		public AddLangIDToUserSession() : base("08CB5E27-3711-4B5F-A316-24EB41D56917")
		{
		}
		
		public override void Migrate()
		{
			AddColumn("UserSession", new Column("LangID", "integer"));
		}
		
		public override void Rollback()
		{
			base.Rollback();
		}
	}
}
