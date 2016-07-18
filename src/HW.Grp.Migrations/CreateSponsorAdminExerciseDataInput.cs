/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/18/2016
 * Time: 2:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Rooko.Core;

namespace HW.Grp.Migrations
{
	public class CreateSponsorAdminExerciseDataInput : Migration
	{
		public CreateSponsorAdminExerciseDataInput() : base("5B4B933C-4D34-48B3-B391-F71B22BC1EE3")
		{
		}
		
		public override void Migrate()
		{
			CreateTable(
				"SponsorAdminExerciseDataInput",
				new Column("SponsorAdminExerciseDataInputID", "integer", true, true, true),
				new Column("SponsorAdminExerciseID", "integer"),
				new Column("Content", "text"),
				new Column("[Order]", "integer")
			);
		}
		
		public override void Rollback()
		{
			DropTable("SponsorAdminExerciseDataInput");
		}
	}
}
