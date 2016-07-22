/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/22/2016
 * Time: 3:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;
using HW.Core.Models;
using NUnit.Framework;

namespace HW.Grp.Tests
{
	[TestFixture]
	public class ExerciseExportTests
	{
		ExerciseExport v =  new ExerciseExport();
		ExerciseVariantLanguage evl;
		
		[SetUp]
		public void Setup()
		{
			evl = new ExerciseVariantLanguage {
				Variant = new ExerciseVariant {
					Exercise = new HW.Core.Models.Exercise {
						Languages = new [] {
							new ExerciseLanguage {
								ExerciseName = "Celebrating success"
							}
						}
					}
				},
				Content = @"<div class='block'>
<p>Tids&aring;tg&aring;ng: F&ouml;rberedelse 2 min; genomf&ouml;rande ca 20 min tillsammans med medarbetarna</p>
<p>Om du som chef f&ouml;rst&aring;r dina medarbetares olika preferenser och &ouml;nskem&aring;l &ouml;kar dina m&ouml;jligheter att hitta s&auml;tt att fira framg&aring;ng som motiverar medarbetarna.</p>
<p>Det h&auml;r &auml;r en blankett som du kan dela ut till dina medarbetare f&ouml;r att f&aring; reda p&aring; hur de skulle &ouml;nska att ni firade framg&aring;ngar.</p>
<p>Du kan v&auml;lja mellan att dela ut den som den &auml;r &ndash; eller &auml;nnu hellre: g&ouml;r en brainstorming i grupp, och sammanst&auml;ll sedan favoriterna p&aring; blanketten.</p>
<h2>Favoritfirande</h2>
<p>Framg&aring;ng b&ouml;r firas! Fira kan man b&ouml;ra p&aring; flera s&auml;tt &ndash; och vad som upplevs som relevanta s&auml;tt att fira p&aring; varierar mellan olika personer. Nedan kan du fylla i vad du tycker ni kan g&ouml;ra f&ouml;r att fira n&aring;got som varit bra. Syftet &auml;r att ge din chef insyn i vad just du uppskattar.</p>
<ul>
<li>B&ouml;rja med att generera s&aring; m&aring;nga olika f&ouml;rslag p&aring; firande du kan komma p&aring;.</li>
</ul>
<ul class='sortable1'><!-- <li><input type='text' name='text0' value=''></li> --></ul>
<p style='text-align: center; margin: 10px; margin-right: 40px;'><a class='btn btnAddDataInput'>L&auml;gg till</a> <a class='btn btnSaveSponsorAdminExercise'>Spara</a></p>
<ul>
<li>Rangordna dina f&ouml;rslag genom att skriva en siffra vid f&ouml;rslagen; 1 f&ouml;r det f&ouml;rslag som skulle k&auml;nnas b&auml;st f&ouml;r dig, 2 f&ouml;r det n&auml;st b&auml;sta osv.</li>
</ul>
<form id='frm1' action='' method='post'>
<ul class='sortable'><!-- <li><input type='text' name='text0' value=''></li> --></ul>
<div id='print'>&nbsp;</div>
</form></div>"
			};
		}
		
		[Test]
		public void TestExport()
		{
			using (var f = new FileStream("@exercise.pdf", FileMode.Create)) {
				var s = v.Export(evl, "", "");
				s.WriteTo(f);
			}
			Process.Start("@exercise.pdf");
		}
		
//		[Test]
//		public void TestMethod()
//		{
//			using (FileStream f = new FileStream(@"test.pdf", FileMode.Create)) {
//				MemoryStream s = x.Export(evl);
//				s.WriteTo(f);
//			}
//			Process.Start(@"test.pdf");
//		}
	}
}
