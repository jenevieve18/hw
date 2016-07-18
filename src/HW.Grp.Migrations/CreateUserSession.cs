/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/18/2016
 * Time: 2:29 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Rooko.Core;

namespace HW.Grp.Migrations
{
	public class CreateUserSession : Migration
	{
		public CreateUserSession() : base("CE512AEA-6B54-490E-B924-0FC226AD532B")
		{
		}
		
		public override void Migrate()
		{
			CreateTable(
				"UserSession",
				new Column("UserSessionID", "integer", true, true, true),
				new Column("UserHostAddress"),
				new Column("UserAgent")
			);
		}
		
		public override void Rollback()
		{
			DropTable("UserSession");
		}
	}
}
