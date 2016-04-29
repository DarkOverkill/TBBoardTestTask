using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestTask.Models;

namespace TestTask
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btn_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            DataFolder folder = new DataFolder();
            folder.Level = Convert.ToInt32(b.Attributes["data-folder-level"]);
            folder.ParentId = Convert.ToInt32(b.Attributes["data-folder-parent"]);
            folder.Name = folderName.Value;
            Repository.GetRepository().AddFolder(folder);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
            //lblInfo.Text = b.Attributes["data-folder-parent"];
        }
        protected string ShowChildren(int parentId, int level)
        {
            List<DataFolder> children = new List<DataFolder>();
            children = Repository.GetRepository().GetChild(parentId, level);
            string html = "";
            foreach (DataFolder d in children)
            {
                string space = "";
                for (int i = 0; i < d.Level; i++)
                {
                    space += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                html += String.Format("<div id='{1}'>{4}<img src='content/folder.jpg' />{0}<button type='button' id='add_{2}' class='send glyphicon glyphicon-plus' runat='server' title='add folder'" +
                "data-folder-level='{3}' data-folder-parent='{2}'></button><button class='loadFile glyphicon glyphicon-download-alt' title='Load file' runat='server' id='load_{2}'>"+
                "</button>", d.Name, d.Level, d.Id, (d.Level + 1), space) + ShowChildren(d.Id, d.Level + 1);
                html += GetFilesByFolderId(d.Id) + "</div >";
            }
            return html;
        }
        protected string GetFilesByFolderId(int id)
        {
            List<DataFile> files = new List<DataFile>();
            files = Repository.GetRepository().GetFilesByFolderId(id);
            int level = Repository.GetRepository().GetFolderLevel(id);
            string html = "<div>";
            foreach (DataFile f in files)
            {
                for (int i = 0; i < level; i++)
                {
                    html += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                html += String.Format("<img src='content/file.jpg' />{0}</div>", f.FileName);
            }           
            return html;
        }

        [System.Web.Services.WebMethod]
        public static void AddChild(string Name, string Level, string parentId)
        {
            DataFolder folder = new DataFolder();
            folder.Level = Convert.ToInt32(Level);
            folder.ParentId = Convert.ToInt32(parentId);
            folder.Name = Name;
            Repository.GetRepository().AddFolder(folder);           
        }     
    }
}