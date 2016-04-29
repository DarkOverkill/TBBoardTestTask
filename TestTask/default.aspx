<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestTask.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="Scripts/lib/jquery/jquery-2.1.4.js"></script>
        <script src="Scripts/lib/jquery/jquery-2.1.4.min.js"></script>
        <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
        <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
        <script src="Scripts/page/AjaxRequests.js"></script>
        <link href="content/css/MyStyles.css" rel="stylesheet" />
</head>
<body>
    
    <form id="form1" runat="server" method="post">

    <div>
       <!--<asp:label id="lblInfo" runat="server">value - </asp:label>
       <input type="hidden" id="folderLevel_0" value="0" runat="server" />
       <input type="hidden" id="folderParentId_0" value="0" runat="server" />
       
       

        <input type="hidden" id="folderLevel_1" value="1" runat="server" />
        <input type="hidden" id="folderparentId_1" value="3" runat="server" />-->
       <input type="text" id="folderName" placeholder="folder name" runat="server" />
       <asp:Button onclick="btn_click" ID="add_0" runat="server" text="add folder"
          data-folder-level="0" data-folder-parent="0" />
    </div>
    </form>
   <div>

    <%var data = TestTask.Repository.GetRepository().GetMainFolders();
        foreach(var d in data){
            string html = String.Format("<div id='{1}'><img src='content/folder.jpg' />{0}<button type='button' id='add_{2}' class='send glyphicon glyphicon-plus' runat='server' title='add folder'"+
         "data-folder-level='{3}' data-folder-parent='{2}'></button><button class='loadFile glyphicon glyphicon-download-alt' title='Load file' runat='server' id='load_{2}'></button>", d.Name, d.Level, d.Id, (d.Level +1));
            Response.Write(html);
            
            Response.Write(ShowChildren(d.Id, d.Level + 1));
            Response.Write(GetFilesByFolderId(d.Id));
            Response.Write("</div>");
        }
    %>
       <input type="file" name="fileToUpload" id="fileToUpload">
   </div>
</body>
</html>
