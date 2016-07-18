/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/18/2016
 * Time: 2:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Rooko.Core;

namespace HW.Grp.Migrations
{
	public class CreateSponsorAdminExercise : Migration
	{
		public CreateSponsorAdminExercise() : base("005E7349-A026-4915-A0A6-C1171318070F")
		{
		}
		
		public override void Migrate()
		{
			CreateTable(
				"SponsorAdminExercise",
				new Column("SponsorAdminExerciseID", "integer", true, true, true),
				new Column("Date", "datetime"),
				new Column("SponsorAdminID", "integer"),
				new Column("ExerciseVariantLangID", "integer")
			);
		}
		
		public override void Rollback()
		{
			DropTable("SponsorAdminExercise");
		}
	}
}
