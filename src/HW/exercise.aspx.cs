using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace HW
{
    public partial class exercise : System.Web.UI.Page
    {
        protected int AX = 0, BX = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UserID"] == null)
            {
                HttpContext.Current.Response.Redirect("home.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            int EAID = (HttpContext.Current.Request.QueryString["EAID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["EAID"]) : 0);
            int TYPE = (HttpContext.Current.Request.QueryString["TYPE"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["TYPE"]) : 0);
            int SORT = (HttpContext.Current.Request.QueryString["SORT"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SORT"]) : 0);
            string sortQS = "&SORT=" + SORT;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader rs;
            int rExerciseAreaID = 0, rExerciseID = 0;//, BX = 0, CX = 0, rVariantCount = 0;
            //string rExerciseImg = "";

            if (!IsPostBack)
            {
                if (EAID == 0)
                {
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1: CategoryID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Visa alla</span></a></dt><dd><ul>")); break;
                        case 2: CategoryID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Show all</span></a></dt><dd><ul>")); break;
                    }
                }
                string s = "";
                rs = Db.rs("SELECT " +
                     "eal.ExerciseArea, " +          // 0
                     "eal.ExerciseAreaID " +
                     "FROM [ExerciseArea] ea " +
                     "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
                     "WHERE eal.Lang = " + (Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1) + " " +
                     "AND (" +
                     "SELECT COUNT(*) " +
                     "FROM Exercise e " +
                     "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
                     "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
                     "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
                     "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
                     "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
                     "WHERE e.ExerciseAreaID = ea.ExerciseAreaID " +
                     "AND eal.Lang = el.Lang " +
                     "AND e.RequiredUserLevel = 0 " +
                     "AND el.Lang = evl.Lang " +
                     "AND evl.Lang = etl.Lang " +
                     ") > 0 " +
                     "ORDER BY CASE eal.ExerciseAreaID WHEN " + EAID + " THEN NULL ELSE ea.ExerciseAreaSortOrder END");
                while (rs.Read())
                {
                    if (EAID == rs.GetInt32(1))
                    {
                        CategoryID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>" + rs.GetString(0) + "</span></a></dt><dd><ul>"));
                        switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                        {
                            case 1: CategoryID.Controls.Add(new LiteralControl("<li id=\"EAID0\"><a href=\"exercise.aspx?EAID=0" + sortQS + "#filter\">Visa alla</a></li>")); break;
                            case 2: CategoryID.Controls.Add(new LiteralControl("<li id=\"EAID0\"><a href=\"exercise.aspx?EAID=0" + sortQS + "#filter\">Show all</a></li>")); break;
                        }
                    }
                    else
                    {
                        if (s != "")
                        {
                            CategoryID.Controls.Add(new LiteralControl("<li" + s));
                        }
                        s = " id=\"EAID" + rs.GetInt32(1) + "\"><a href=\"exercise.aspx?EAID=" + rs.GetInt32(1) + "" + sortQS + "#filter\">" + rs.GetString(0) + "</a></li>";
                    }
                }
                rs.Close();
                CategoryID.Controls.Add(new LiteralControl("<li class=\"last\"" + s));
                CategoryID.Controls.Add(new LiteralControl("</ul></dd>"));

                if (TYPE == 0)
                {
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1:
                            TypeID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Visa alla</span></a></dt><dd><ul>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE1\"><a href=\"exercise.aspx?TYPE=1" + sortQS + "#filter\">Korta övningar</a></li>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE2\"><a href=\"exercise.aspx?TYPE=2" + sortQS + "#filter\">Längre övningar</a></li>"));
                            break;
                        case 2:
                            TypeID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Show all</span></a></dt><dd><ul>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE1\"><a href=\"exercise.aspx?TYPE=1" + sortQS + "#filter\">Short exercises</a></li>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE2\"><a href=\"exercise.aspx?TYPE=2" + sortQS + "#filter\">Longer exercises</a></li>"));
                            break;
                    }
                }
                else if (TYPE == 1)
                {
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1:
                            TypeID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Korta övningar</span></a></dt><dd><ul>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE0\"><a href=\"exercise.aspx?TYPE=0" + sortQS + "#filter\">Visa alla</a></li>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE2\"><a href=\"exercise.aspx?TYPE=2" + sortQS + "#filter\">Längre övningar</a></li>"));
                            break;
                        case 2:
                            TypeID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Short exercises</span></a></dt><dd><ul>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE0\"><a href=\"exercise.aspx?TYPE=0" + sortQS + "#filter\">Show all</a></li>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE2\"><a href=\"exercise.aspx?TYPE=2" + sortQS + "#filter\">Longer exercises</a></li>"));
                            break;
                    }
                }
                else if (TYPE == 2)
                {
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        case 1:
                            TypeID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Längre övningar</span></a></dt><dd><ul>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE0\"><a href=\"exercise.aspx?TYPE=0" + sortQS + "#filter\">Visa alla</a></li>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE1\"><a href=\"exercise.aspx?TYPE=1" + sortQS + "#filter\">Korta övningar</a></li>"));
                            break;
                        case 2:
                            TypeID.Controls.Add(new LiteralControl("<dt><a href=\"javascript:;\"><span>Longer exercises</span></a></dt><dd><ul>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE0\"><a href=\"exercise.aspx?TYPE=0" + sortQS + "#filter\">Show all</a></li>"));
                            TypeID.Controls.Add(new LiteralControl("<li id=\"TYPE1\"><a href=\"exercise.aspx?TYPE=1" + sortQS + "#filter\">Short exercises</a></li>"));
                            break;
                    }
                }
                TypeID.Controls.Add(new LiteralControl("</ul></dd>"));
            }

            rs = Db.rs("SELECT " +
                    "el.New, " +                    // 0
                    "NULL, " +
                //"(" +
                //    "SELECT COUNT(*) FROM [ExerciseVariantLang] evlTmp " +
                //    "INNER JOIN [ExerciseVariant] evTmp ON evlTmp.ExerciseVariantID = evTmp.ExerciseVariantID " +
                //    "WHERE evTmp.ExerciseTypeID >= 3 " +
                //    "AND evTmp.ExerciseTypeID <= 4 " +
                //    "AND Lang = evl.Lang " +
                //    "AND evTmp.ExerciseID = ev.ExerciseID" +
                //") AS VariantCount, " +         // 1
                    "evl.ExerciseVariantLangID, " + // 2
                    "eal.ExerciseArea, " +          // 3
                    "eal.ExerciseAreaID, " +        // 4
                    "e.ExerciseImg, " +             // 5
                    "e.ExerciseID, " +              // 6
                    "ea.ExerciseAreaImg, " +        // 7
                    "el.Exercise, " +               // 8
                    "el.ExerciseTime, " +           // 9
                    "el.ExerciseTeaser, " +         // 10
                    "evl.ExerciseFile, " +          // 11
                    "evl.ExerciseFileSize, " +      // 12
                    "evl.ExerciseContent, " +       // 13
                    "evl.ExerciseWindowX, " +       // 14
                    "evl.ExerciseWindowY, " +       // 15
                    "et.ExerciseTypeID, " +         // 16
                    "etl.ExerciseType, " +          // 17
                    "etl.ExerciseSubtype " +        // 18
                    "FROM [ExerciseArea] ea " +
                    "INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID " +
                    "INNER JOIN [Exercise] e ON ea.ExerciseAreaID = e.ExerciseAreaID " +
                    "INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID " +
                    "INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID " +
                    "INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID " +
                    "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
                    "INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID " +
                    "WHERE eal.Lang = el.Lang " +
                    "AND e.RequiredUserLevel = 0 " +
                    "AND el.Lang = evl.Lang " +
                    "AND evl.Lang = etl.Lang " +
                    "AND etl.Lang = " + (Convert.ToInt32(HttpContext.Current.Session["LID"]) - 1) + " " +
                    (TYPE != 0 ? "AND e.Minutes " + (TYPE == 1 ? "<= 15" : "> 15") + " " : "") +
                    (EAID != 0 ? "AND e.ExerciseAreaID = " + EAID + " " : "") +
                    "ORDER BY " +
                //"ea.ExerciseAreaSortOrder ASC, " +
                //"e.ExerciseSortOrder ASC, " +
                    (SORT == 1 ? "(SELECT COUNT(*) FROM ExerciseStats esX INNER JOIN ExerciseVariantLang evlX ON esX.ExerciseVariantLangID = evlX.ExerciseVariantLangID INNER JOIN ExerciseVariant evX ON evlX.ExerciseVariantID = evX.ExerciseVariantID WHERE evX.ExerciseID = e.ExerciseID) DESC, " : (SORT == 2 ? "el.Exercise ASC, " : "")) +
                    "HASHBYTES('MD2',CAST(RAND(" + DateTime.Now.Second * DateTime.Now.Minute + ")*e.ExerciseID AS VARCHAR(16))) ASC, " +
                    "et.ExerciseTypeSortOrder ASC");
            while (rs.Read())
            {
                //if(rs.GetInt32(4) != rExerciseAreaID)
                //{
                //    if(rExerciseAreaID != 0)
                //    {
                //        ExerciseList.Text += "</tr></table></div>";
                //    }
                //    ExerciseList.Text += "\r\n<div class=\"boxTitle\" style=\"width:707px;\">" + rs.GetString(3) + "</div>";
                //    ExerciseList.Text += "\r\n<div class=\"box\" style=\"width:707px;\">";
                //}

                if (rs.GetInt32(6) != rExerciseID)
                {
                    BX++;
                    if (AX > 0)
                    {
                        sb.Append("</div><div class=\"bottom\">&nbsp;</div></div><!-- end .detail --> </div><!-- end .item -->");
                    }
                    //if(rExerciseAreaID == rs.GetInt32(4) && rExerciseID != 0)
                    //{
                    //    if(rs.IsDBNull(5) || rExerciseImg == "" || rVariantCount > 0)
                    //    {
                    //        ExerciseList.Text += "</tr></table><img src=\"img/null.gif\" width=\"1\" height=\"15\"><br>";
                    //    }
                    //    else
                    //    {
                    //        ExerciseList.Text += "</tr></table><img src=\"img/null.gif\" width=\"1\" height=\"35\"><br>";
                    //    }
                    //}

                    //rVariantCount = rs.GetInt32(1);
                    //rExerciseImg = (rs.IsDBNull(5) ? "" : rs.GetString(5));

                    sb.Append("<div class=\"item\"><div class=\"overview\"></div><div class=\"detail\">");

                    //if(!rs.IsDBNull(5) && rs.GetString(5) != "")
                    //{
                    //    sb.Append("<div class=\"image\"><img src=\"" + rs.GetString(5) + "\" width=\"94\" height=\"68\"></div>");
                    //}
                    sb.Append("<div class=\"image\">" + (!rs.IsDBNull(5) && rs.GetString(5) != "" ? "<img src=\"" + rs.GetString(5) + "\" width=\"121\" height=\"100\">" : "") + "</div>");

                    // new
                    //if(rs.GetBoolean(0))
                    //{
                    //    ExerciseList.Text += "<img src=\"img/exercise/new0.gif\" width=\"38\" height=\"10\"><img src=\"img/null.gif\" width=\"7\" height=\"1\">";
                    //}


                    // time
                    if (!rs.IsDBNull(9) && rs.GetString(9) != "")
                    {
                        sb.Append("<div class=\"time\">" + rs.GetString(9) + "<span class=\"time-end\"></span></div>");
                    }

                    // exercise
                    sb.Append("<div class=\"descriptions\">" + rs.GetString(3) + "</div><h2>" + rs.GetString(8) + "</h2>");

                    // teaser
                    if (!rs.IsDBNull(10) && rs.GetString(10) != "")
                    {
                        sb.Append("<p>" + rs.GetString(10) + "</p>");
                    }
                    //ExerciseList.Text += "<img src=\"img/null.gif\" width=\"1\" height=\"6\"><br><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
                    sb.Append("<div>");
                    //CX = 0;
                }
                //BX = 3;

                /*
                            
                            <a class="sidearrow" href="flashpopup.html" target="_blank" onClick="window.open('flashpopup.html','HealthWatch','scrollbars=yes,width=946,height=690'); return false;">
                                Med animering och ljud (flash)
                            </a>
                            <a class="sidearrow" href="htmlpopup.html" target="_blank" onClick="window.open('htmlpopup.html','HealthWatch','scrollbars=yes,width=946,height=690'); return false;">Utan animering och ljud (html)</a>
                            
                */

                //if(!rs.IsDBNull(5) && rs.GetInt32(1) > 0)
                //{
                //    BX = 2;
                //}

                //if(CX % BX == 0)
                //{
                //    if(CX != 0)
                //    {
                //        ExerciseList.Text += "</tr><tr><td colspan=\"8\"><img src=\"img/null.gif\" width=\"1\" height=\"6\"></td></tr>";
                //    }
                //    else
                //    {
                //        ExerciseList.Text += "<tr><td colspan=\"8\"><img src=\"img/null.gif\" width=\"1\" height=\"6\"></td></tr>";
                //    }
                //    ExerciseList.Text += "<tr>";
                //}
                //else
                //{
                //    ExerciseList.Text += "<td width=\"7\"><img src=\"img/null.gif\" width=\"7\" height=\"1\"></td>";
                //}

                sb.Append("<a class=\"sidearrow\" href=\"JavaScript:void(window.open('exerciseShow.aspx?ExerciseVariantLangID=" + rs.GetInt32(2) + "','EVLID" + rs.GetInt32(2) + "','scrollbars=yes,resizable=yes,");

                if (rs.IsDBNull(14))
                {
                    sb.Append("width=650,height=580");
                }
                else
                {
                    sb.Append("width=" + rs.GetInt32(14) + ",height=" + rs.GetInt32(15));
                }
                sb.Append("'));\">" + rs.GetString(17) + (!rs.IsDBNull(18) && rs.GetString(18) != "" ? " (" + rs.GetString(18) + ")" : "") + "</a>");

                //if(!rs.IsDBNull(18) && rs.GetString(18) != "" || !rs.IsDBNull(12))
                //{
                //    ExerciseList.Text += "&nbsp;[";
                //    if(!rs.IsDBNull(18) && rs.GetString(18) != "")
                //    {
                //        ExerciseList.Text += rs.GetString(18);
                //        if(!rs.IsDBNull(12))
                //        {
                //            ExerciseList.Text += ",&nbsp;";
                //        }
                //    }
                //    if(!rs.IsDBNull(12))
                //    {
                //        ExerciseList.Text += Math.Round(Convert.ToDecimal(rs.GetInt32(12)) / 1024, 0) + "kb";
                //    }
                //    ExerciseList.Text += "]";
                //}
                //ExerciseList.Text += "</td>";
                rExerciseAreaID = rs.GetInt32(4);
                rExerciseID = rs.GetInt32(6);
                //CX = CX + 1;
                AX++;
            }
            rs.Close();

            if (AX > 0)
            {
                sb.Append("</div><div class=\"bottom\">&nbsp;</div></div><!-- end .detail --> </div><!-- end .item -->");
            }
            //ExerciseList.Text += "</tr></table></div>";

            if (!IsPostBack)
            {
                ExerciseList.Controls.Add(new LiteralControl(sb.ToString()));

                string q = (EAID != 0 ? "&EAID=" + EAID : (TYPE != 0 ? "&TYPE=" + TYPE : ""));

                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1:
                        Sort.Controls.Add(new LiteralControl("<a" + (SORT == 0 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=0" + q + "#filter\"") + "><span>Slumpmässigt</span></a>"));
                        Sort.Controls.Add(new LiteralControl("<a" + (SORT == 1 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=1" + q + "#filter\"") + "><span>Popularitet</span></a>"));
                        Sort.Controls.Add(new LiteralControl("<a" + (SORT == 2 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=2" + q + "#filter\"") + "><span>Bokstavsordning</span></a>"));
                        break;
                    case 2:
                        Sort.Controls.Add(new LiteralControl("<a" + (SORT == 0 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=0" + q + "#filter\"") + "><span>Random</span></a>"));
                        Sort.Controls.Add(new LiteralControl("<a" + (SORT == 1 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=1" + q + "#filter\"") + "><span>Popularity</span></a>"));
                        Sort.Controls.Add(new LiteralControl("<a" + (SORT == 2 ? " class=\"active\" href=\"javascript:;\"" : " href=\"exercise.aspx?SORT=2" + q + "#filter\"") + "><span>Alphabethical</span></a>"));
                        break;
                }
            }
        }
    }
}