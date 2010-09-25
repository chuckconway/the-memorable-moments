<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.UserSearchView>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Widgets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	search - <%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div id="userheader">
		<%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
	</div>

	<div id="contentarea" >
   <p class="paging"> <span style="color:#999;font-size:12px;line-height:190%;" ><%= Model.TotalResults  %> result(s) found</span> 
   <br />  <% if(Model.Pagination.TotalPages > 1){ %> showing <%=Model.Pagination.CurrentPage %> of <%= Model.Pagination.TotalPages %> pages <%} %></p>
	<%= Html.Grid(Model.Pagination.PageSet).Empty("no results found.")
		.Attributes(new Dictionary<string, object>{{"class","results width100percent"}})
		.Columns(c => c.For( m => Model.GetImageLink(Model.Set, m)).DoNotEncode().Named(string.Empty))
		.Columns(c => c.For( m => Model.RenderDetailColumn(m)).DoNotEncode().Named(string.Empty))
	%>
	
	<%= Model.Pagination.RenderedPagination %>

	</div>

	   <div class="sidebar hyperlinks" >
	  <h3>search</h3>
		  <div class="searchform">
			 <form action="/<%= Model.Authorization.Owner.Username %>/Search" method="get" name="searchForm" > 
				<input  name="text" type="text"  value="<%: Model.SearchText %>" class="width232" maxlength="150" /> 
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

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">

       <script language="javascript" type="text/javascript">
       var username = "<%= Model.Authorization.Owner.Username %>";
       var maxHeight = <%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>;
    </script>

	<style type="text/css">
		@import url("/Content/css/user/search.css");   
		img{ max-height:<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>px; max-width:<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>px;}
	</style>
    	
	<script language="javascript" type="text/javascript" src="/Scripts/Controls/jTagCloud.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.timeago.js"></script>  
	<script language="javascript" src="/Scripts/Pages/user/search.js" type="text/javascript"></script>
	
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">
<%= UserNavigationHelper.GetUserNavigation(Model.Authorization)%>
</asp:Content>
