/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/19/2016
 * Time: 1:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class ExerciseService
	{
		SqlSponsorRepository sr = new SqlSponsorRepository();
		SqlExerciseRepository er = new SqlExerciseRepository();
		
		public ExerciseService()
		{
		}
		
		public SponsorAdminExercise ReadSponsorAdminExercise(int sponsorAdminExerciseID)
		{
			var a = sr.ReadSponsorAdminExercise(sponsorAdminExerciseID);
			a.ExerciseVariantLanguage = er.ReadExerciseVariant(a.ExerciseVariantLanguage.Id);
			a.DataInputs = sr.FindSponsorAdminExerciseDataInputs(sponsorAdminExerciseID);
			return a;
		}
		
		public Sponsor ReadSponsor3(int sponsorId)
		{
			return sr.ReadSponsor3(sponsorId);
		}
		
		public void SaveStats(int exerciseVariantLangId, int userId, int userProfileId)
		{
			er.SaveStats(exerciseVariantLangId, userId, userProfileId);
		}
	}
}
