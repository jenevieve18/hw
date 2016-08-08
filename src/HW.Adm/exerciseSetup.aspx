<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" Inherits="exerciseSetup" Codebehind="exerciseSetup.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
   <%=Db.header()%>

    <%--<script src="https://cdn.tinymce.com/4/tinymce.min.js"></script>
    <script>
        tinymce.init({
            selector: 'textarea.text',
            plugins: [
              'advlist autolink lists link image charmap print preview anchor',
              'searchreplace visualblocks code fullscreen',
              'insertdatetime media table contextmenu paste code'
            ],
        });
    </script>--%>

    <link rel="stylesheet" type="text/css" href="https://cdn.rawgit.com/codemirror/CodeMirror/5.17.0/lib/codemirror.css">
    <link rel="stylesheet" type="text/css" href="https://codemirror.net/theme/ambiance.css">
      
    <script type="text/javascript" src="https://cdn.rawgit.com/codemirror/CodeMirror/5.17.0/lib/codemirror.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/codemirror/CodeMirror/5.17.0/mode/xml/xml.js"></script>
    <script type="text/javascript" src="https://cdn.rawgit.com/codemirror/CodeMirror/5.17.0/mode/javascript/javascript.js"></script>
      
  </head>
  <body>
	    <form id="Form1" method="post" runat="server">
		<%=Db.nav()%>
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Exercise setup</td></tr>
		</table>

        <!-- include libraries(jQuery, bootstrap) -->
        <link href="https://netdna.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.css" rel="stylesheet">
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.js"></script> 
        <script src="https://netdna.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.js"></script> 

        <!-- include summernote css/js-->
        <link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.1/summernote.css" rel="stylesheet">
        <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.1/summernote.js"></script>

		<table style="margin:20px;" width="800" border="0" cellspacing="0" cellpadding="0">
            <tr><td>Type&nbsp;</td><td><asp:DropDownList ID="RequiredUserLevel" runat=server><asp:ListItem Value="0">End user</asp:ListItem><asp:ListItem Value="10">Manager</asp:ListItem></asp:DropDownList></td><td rowspan="4"><asp:Label ID=ExerciseImgUploaded runat=server /></td></tr>
		    <tr><td>Area&nbsp;</td><td><asp:DropDownList ID=ExerciseAreaID runat=server /></td></tr>
            <tr><td>Category&nbsp;</td><td><asp:DropDownList ID=ExerciseCategoryID runat=server /></td></tr>
            <tr><td>Image (optional)&nbsp;</td><td><input type="file" runat=server id=ExerciseImg /></td></tr>
            <tr><td>Time (statistics)&nbsp;</td><td><asp:TextBox ID=Minutes runat=server Width=35 />minutes</td></tr>
            <tr><td valign="top">Javascript (for exercise)</td><td><asp:TextBox ID=textBoxJavascript runat=server Width="800" Height="150" ViewStateMode="Inherit" TextMode="MultiLine" /></td></tr>
            <asp:PlaceHolder ID="ExerciseLang" runat=server />
            <asp:PlaceHolder ID="ExerciseVariant" runat=server />
		</table>
        <asp:Button runat="server" Text="Cancel" id="buttonCancel" OnClientClick="window.location='exercise.aspx'; return false;">   </asp:Button>
        <!--<button onclick="location.href='exercise.aspx';">Cancel</button>-->
        <asp:Button ID=Save runat=server Text="Save" /> Add <asp:DropDownList ID=ExerciseTypeID runat=server />
		<%=Db.bottom()%>
		</form>

        <script>
            var editor = CodeMirror.fromTextArea(document.getElementById("<%= textBoxJavascript.ClientID %>"), {
                lineNumbers: true,
                mode: "javascript",
                theme: 'ambiance'
            });
            editor.setSize(800, 400);
        </script>

        <script>
            $(document).ready(function () {
                $('.summernote').summernote();
            });
        </script>
  </body>
</html>