//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
using NUnit.Framework;

namespace HW.Tests.Views
{
	[TestFixture]
	public class StatsTests
	{
		SqlLanguageRepository langRepository;
		SqlSponsorRepository sponsorRepository;
		
		[SetUp]
		public void Setup()
		{
			langRepository = new SqlLanguageRepository();
			sponsorRepository = new SqlSponsorRepository();
		}
		
		[Test]
		public void TestNotPostBack()
		{
			int sponsorID = 1;
			foreach (var l in langRepository.FindBySponsor(sponsorID)) {
				// Add to language combo box
			}
			int langID = 1;
			foreach (var p in sponsorRepository.FindBySponsorAndLanguage(sponsorID, langID)) {
				// Add project round unit to combo box
			}
			foreach (var s in sponsorRepository.FindBySponsor(sponsorID)) {
				// Add background questions to combo box
			}
		}
	}
}
