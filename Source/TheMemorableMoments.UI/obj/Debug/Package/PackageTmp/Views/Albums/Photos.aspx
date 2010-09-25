<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.AlbumModels.ManagePhotosView>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing '<%: Model.Album.Name %>' - <%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader">
        <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>

    <br style="display:inline;clear:both;" />
    
    <div id="fadeinarea" class="hidden whitebackground">

    	 <div class="nav"> 
		<ul style="float:right;" >
           <li><a  href="/<%= Model.Authorization.Owner.Username %>/albums/details/<%= Model.Album.AlbumId %>">Details</a></li>
           <li><a class="youarehere" href="/<%= Model.Authorization.Owner.Username %>/albums/photos/<%= Model.Album.AlbumId %>">Photos</a></li>
 		   <li><a href="/<%= Model.Authorization.Owner.Username %>/albums/addphotos/<%= Model.Album.AlbumId %>">Add</a></li> 
		</ul> 
		<br class="clearboth" />
	</div>  


    <label class="instructions" >photo position</label>
    <p>The position of the image can be changed by dragging the row up or down with in the photos below</p>

    <label class="instructions" >cover image</label>
    <p>Once a cover image is selected it will always be displayed as the thumbnail image. If a cover image is not selected then an image is randomly selected.</p>

    <label class="instructions" >removing images</label>
    <p>To remove an image from this album, simply click on the remove link.</p>

    <p style="margin-top:40px;" class="addlink" >
        <span> <%= Model.Media.Count + " " + (Model.Media.Count == 1 ? "photo" : "photos")%> in album</span>
    </p>
    
        <%= Html.Grid(Model.Media).Attributes(new Dictionary<string, object>{{"class", "width100percent results"}}).Empty("")
                .Columns(c => c.For(x => (x.Position.HasValue ? x.Position.GetValueOrDefault().ToString() : string.Empty)))
                .Columns(c => c.For(x =>  Model.GetImageLink(Model.Set, x) + "<input class=\"mediaid\" type=\"hidden\" value=\"" + x.MediaId + "\" />").DoNotEncode())
                .Columns(c => c.For(x => Model.RenderDetails(x)).DoNotEncode() )
                .Columns(c => c.For(x => Model.RenderActionLinks(x)).DoNotEncode().Attributes(x => new Dictionary<string, object>{{"style","text-align:right;"}}))  %>

        </div>
        <span class="message" ><%= Model.UIMessage %></span> 

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    
    <script language="javascript" type="text/javascript" src="/Scripts/Pages/common.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.autosearch.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.jnotifica.min.js"></script>   
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.fancybox-1.3.1.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.lightbox-0.5.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/Albums/jquery.tablednd.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Pages/albums/photos.js"></script>

    <style type="text/css"> 
        @import url("/Content/css/albums/photos.css");
        @import url("/Content/css/controls/jquery.fancybox-1.3.1.css"); 
        @import url("/Content/css/controls/jquery.lightbox-0.5.css"); 
    </style>

    <style media="all" type="text/css">
        img{ max-height:<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>px; max-width:<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>px;}
    </style>

    <script language="javascript" type="text/javascript">

        var username = "<%= Model.Authorization.Owner.Username %>";
        var message = '<%= Model.UIMessage %>';
        var albumId = '<%= Model.Album.AlbumId %>';
        var maxHeight = <%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>;
        var maxWidth = <%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>;
   
    </script>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="navigation" runat="server">
<%= Html.SiteNavigation(Model.Authorization) %>
</asp:Content>

