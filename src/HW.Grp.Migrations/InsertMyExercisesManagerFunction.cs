using System;
using Rooko.Core;

namespace HW.Grp.Migrations
{
	public class InsertMyExercisesManagerFunction : Migration
	{
		public InsertMyExercisesManagerFunction() : base("F2C64D60-2CC5-4F3A-95BF-B2F2AE20F3CD")
		{
		}
		
		public override void Migrate()
		{
			Insert(
				"ManagerFunction",
				new Column { Name = "ManagerFunctionID", Value = 9 },
				new Column { Name = "ManagerFunction", Value = "My Exercises" },
				new Column { Name = "URL", Value = "myexercise.aspx" },
				new Column { Name = "Expl", Value = "" }
			);
			Insert(
				"ManagerFunctionLang",
				new Column { Name = "ManagerFunctionID", Value = 9 },
				new Column { Name = "ManagerFunction", Value = "Chef övningar" },
				new Column { Name = "URL", Value = "myexercise.aspx" },
				new Column { Name = "Expl", Value = "Chef övningar" },
				new Column { Name = "LangID", Value = 1 }
			);
			Insert(
				"ManagerFunctionLang",
				new Column { Name = "ManagerFunctionID", Value = 9 },
				new Column { Name = "ManagerFunction", Value = "My Exercises" },
				new Column { Name = "URL", Value = "myexercise.aspx" },
				new Column { Name = "Expl", Value = "My Exercises" },
				new Column { Name = "LangID", Value = 2 }
			);
		}
		
		public override void Rollback()
		{
			Delete(
				"ManagerFunction",
				new Column { Name = "ManagerFunctionID", Value = 9 }
			);
			Delete(
				"ManagerFunctionLang",
				new Column { Name = "ManagerFunctionID", Value = 9 }
			);
		}
	}
}
