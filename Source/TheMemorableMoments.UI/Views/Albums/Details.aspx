<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.AlbumModels.ManageDetailsView>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
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
           <li><a class="youarehere" href="/<%= Model.Authorization.Owner.Username %>/albums/details/<%= Model.Album.AlbumId %>">Details</a></li>
           <li><a href="/<%= Model.Authorization.Owner.Username %>/albums/photos/<%= Model.Album.AlbumId %>">Photos</a></li>
 		   <li><a href="/<%= Model.Authorization.Owner.Username %>/albums/addphotos/<%= Model.Album.AlbumId %>">Add</a></li> 
		</ul> 
		<br class="clearboth" />
	</div>  

      <% using (Html.BeginForm()){%>
      <div class="form editphoto" style="float:left;width:650px;">
        <p  class="margintop30 hyperlinks" ><%= (string.IsNullOrEmpty(Model.AlbumCrumbs) ? string.Empty : Model.AlbumCrumbs )  %> </p>
        <p><label>Name</label><%= Html.TextBox("Name", Model.Album.Name, new Dictionary<string, object>{{"style","width:600px;"}})%> </p>        
        <p><label>Description</label><%= Html.TextArea("Description", Model.Album.Description, new Dictionary<string, object>{{"style","height:200px;width:600px;"}})%> </p>
  
        <p class="submit" ><%=Html.SubmitButton("save", "Save")%></p>       
      </div>

      <div style="float:right;width:340px; margin-top:50px;" >
          
        <%= Model.RenderCoverMedia() %>
              
      </div>
            
      <br class="clearboth" />    
      <%} %>

      <%= Html.Message(Model.UIMessage) %>
      </div>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="head" runat="server">  

    <script language="javascript" type="text/javascript" src="/Scripts/Controls/Albums/jquery.tablednd.js"></script>
    <style type="text/css"> 
        @import url("/Content/css/albums/details.css");
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
