<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.AlbumModels.BySearchingView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Managing '<%: Model.Album.Name %>' - <%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader">
        <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>

    <br style="display:inline;clear:both;" />
    
    <div id="fadeinarea" >

    <div class="nav"> 
		<ul style="float:right;" >
           <li><a  href="/<%= Model.Authorization.Owner.Username %>/albums/details/<%= Model.Album.AlbumId %>">Details</a></li>
           <li><a href="/<%= Model.Authorization.Owner.Username %>/albums/photos/<%= Model.Album.AlbumId %>">Photos</a></li>
 		   <li><a class="youarehere" href="/<%= Model.Authorization.Owner.Username %>/albums/addphotos/<%= Model.Album.AlbumId %>">Add</a></li> 
		</ul> 
		<br class="clearboth" />
	</div>  

    <%= Model.GetAlbumSubNavigation() %>
    
    <%Html.RenderPartial(Model.PartialViewName, Model);%> 

        </div>
        
        <%= Html.Message(Model.UIMessage) %>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">
    <%= Html.Js(new[]{"/Scripts/Controls/jquery.autosearch.min.js", "/Scripts/Pages/albums/addphotos.js"}, "/js/addphotos.js") %>

    <style type="text/css"> 
        @import url("/Content/css/controls/jquery.autosearch.css");  
        img{ max-height:<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>px; max-width:<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>px;}      
    </style>

    <script language="javascript" type="text/javascript">
        var username = "<%= Model.Authorization.Owner.Username %>";
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

