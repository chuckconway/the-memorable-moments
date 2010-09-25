<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.UserModels.UserHomeView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>
<%@ Import Namespace="TheMemorableMoments.UI.Widgets"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>'s home
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">

 <%= Html.Css(new []{"/Content/css/user/index.css"}, "/css/userindex.css") %>

        <style type="text/css"> 
            img{ max-height:<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>px; max-width:<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>px; }            
        </style> 
    
    <script language="javascript" type="text/javascript">
       var maxHeight = <%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>;
       var username = "<%= Model.Authorization.Owner.Username %>";    
    </script>

    <%= Html.Js(new []{
                        "/Scripts/Controls/jTagCloud.js",
                        "/Scripts/controls/FancyBox/jquery.fancybox-1.3.1.js",
                        "/Scripts/controls/jquery.timeago.js",
                        "/Scripts/Frameworks/grayscale.js",
                        "/Scripts/pages/user/index.js",},
                        "/js/userhome_2.1.0.370.js")%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

<%= Html.SiteNavigation(Model.Authorization) %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="userheader">
        <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>

       
    
    <div class="userrandomphotos" >
        <%=ImagesWidget.Render(Model.Media, Model.Authorization.Owner, Model.Set) %>
     </div>
      

      <div class="sidebar hyperlinks" >
      <h3>search</h3>
          <div class="searchform">
             <form action="/<%= Model.Authorization.Owner.Username %>/Search" method="get" name="searchForm" > 
		        <input  name="text" type="text"  class="width232" maxlength="150" /> 
                <input type="submit"  class="submit" value="Search" /> 
	        </form>
        </div>
             <%= RecentCommentsWidget.Render(Model.Authorization.Owner)%>
            <%= RecentUploadsWidget.Render(Model.SidebarRecentUploads, Model.Authorization.Owner.Username)%> 
            <%= AlbumWidget.Render(Model.SidebarAlbums, Model.Authorization.Owner.Username)%>
             <%= YearsWidget.Render(Model.SidebarYearWithCount, Model.Authorization.Owner.Username)%>
             <%= RecentActivityWidget.Render(Model.SidebarRecentActivity)%> 
             <%= FriendsWidget.Render(Model.SidebarFriends, Model.Authorization.Owner.Username) %>  
             <%= TagsWidget.Render(Model.SidebarTags, Model.Authorization.Owner.Username)%>                        
            

            <h3>feeds</h3>
            <ul class="comments lightgrey">
                <li><a href="/<%= Model.Authorization.Owner.Username %>/atom">atom</a></li>
                <li><a href="/<%= Model.Authorization.Owner.Username %>/rss">rss</a></li>
            </ul>            
        
     </div>
            
</asp:Content>
