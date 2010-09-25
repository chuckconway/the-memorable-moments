<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.UserModels.UserHomeView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>
<%@ Import Namespace="TheMemorableMoments.UI.Widgets"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.User.DisplayName %>'s home
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
        <style type="text/css"> 
            @import url("/Content/css/controls/jquery.lightbox-0.5.css"); 
            @import url("/Content/css/controls/jquery.fancybox-1.3.1.css");
            @import url("/Content/css/user/index.css");    
            img{ max-height:<%= Model.User.Settings.WebViewMaxHeight %>px; max-width:<%= Model.User.Settings.WebViewMaxWidth %>px; }            
        </style> 
    
    <script language="javascript" type="text/javascript">
       var maxHeight = <%= Model.User.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.User.Settings.WebViewMaxWidth %>;
       var username = "<%= Model.User.Username %>";    
    </script>

    <script language="javascript" type="text/javascript" src="/Scripts/pages/user/index.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jTagCloud.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/JS.Class/core.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.lightbox-0.5.js"></script>  
    <script language="javascript" type="text/javascript" src="/Scripts/controls/FancyBox/jquery.fancybox-1.3.1.js"></script> 
    <script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.timeago.js"></script>   

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

<%= Html.SiteNavigation(Model.IsAuthenticated, Model.User) %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="userheader">
        <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>

       
    
    <div class="userrandomphotos" >
        <%=ImagesWidget.Render(Model.Media, Model.User) %>
     </div>
      

      <div class="sidebar hyperlinks" >
      <h3>search</h3>
          <div class="searchform">
             <form action="/<%= Model.User.Username %>/Search" method="get" name="searchForm" > 
		        <input  name="text" type="text"  class="width232" maxlength="150" /> 
                <input type="submit"  class="submit" value="Search" /> 
	        </form>
        </div>
             <%= RecentCommentsWidget.Render(Model.User)%>
            <%= RecentUploadsWidget.Render(Model.SidebarRecentUploads, Model.User.Username)%> 
            <%= AlbumWidget.Render(Model.SidebarAlbums, Model.User.Username)%>
             <%= YearsWidget.Render(Model.SidebarYearWithCount, Model.User.Username)%>
             <%= RecentActivityWidget.Render(Model.SidebarRecentActivity)%> 
             <%= FriendsWidget.Render(Model.SidebarFriends, Model.User.Username) %>  
             <%= TagsWidget.Render(Model.SidebarTags, Model.User.Username)%>                        
            

            <h3>feeds</h3>
            <ul class="comments lightgrey">
                <li><a href="/<%= Model.User.Username %>/atom">atom</a></li>
                <li><a href="/<%= Model.User.Username %>/rss">rss</a></li>
            </ul>            
        
     </div>
            
</asp:Content>
