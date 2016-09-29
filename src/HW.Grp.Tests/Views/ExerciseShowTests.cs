/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/26/2016
 * Time: 1:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Models;
using NUnit.Framework;

namespace HW.Grp.Tests.Views
{
	[TestFixture]
	public class ExerciseShowTests
	{
		ExerciseShow v;
		
		[SetUp]
		public void Setup()
		{
			v = new ExerciseShow();
		}
		
		[Test]
		public void TestSetSponsor()
		{
			v.SetSponsor(new Sponsor { Id = 1 });
		}
		
		[Test]
		public void TestShow()
		{
			var evl = new ExerciseVariantLanguage {
				Variant = new ExerciseVariant {
					Exercise = new HW.Core.Models.Exercise {
						ReplacementHead = ""
					},
					Type = new ExerciseType {
						Id = ExerciseType.Text
					}
				}
			};
			v.Show(evl);
		}
	}
}
