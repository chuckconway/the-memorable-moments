<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.UserSearchView>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Widgets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	search - <%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div id="userheader">
		<%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
	</div>

	<div id="contentarea" >
   <p class="paging"> <span style="color:#999;font-size:12px;line-height:190%;" ><%= Model.TotalResults  %> result(s) found</span> 
   <br />  <% if(Model.Pagination.TotalPages > 1){ %> showing <%=Model.Pagination.CurrentPage %> of <%= Model.Pagination.TotalPages %> pages <%} %></p>
	<%= Html.Grid(Model.Pagination.PageSet)
		.Attributes(new Dictionary<string, object>{{"class","results width100percent"}})
		.Columns(c => c.For( m => Model.GetImageLink(m)).DoNotEncode().Named(string.Empty))
		.Columns(c => c.For( m => Model.RenderDetailColumn(m)).DoNotEncode().Named(string.Empty)) 
	%>
	
	<%= Model.Pagination.RenderedPagination %>

	</div>

	   <div class="sidebar hyperlinks" >
	  <h3>search</h3>
		  <div class="searchform">
			 <form action="/<%= Model.User.Username %>/Search" method="get" name="searchForm" > 
				<input  name="text" type="text"  value="<%: Model.SearchText %>" class="width232" maxlength="150" /> 
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

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">

       <script language="javascript" type="text/javascript">
       var username = "<%= Model.User.Username %>";

       var maxHeight = <%= Model.User.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.User.Settings.WebViewMaxWidth %>;
    </script>

	<style type="text/css">
		@import url("/Content/css/user/search.css");   
		@import url("/Content/css/controls/jquery.lightbox-0.5.css"); 
		@import url("/Content/css/controls/jquery.fancybox-1.3.1.css");
		img{ max-height:<%= Model.User.Settings.WebViewMaxHeight %>px; max-width:<%= Model.User.Settings.WebViewMaxWidth %>px;}
	</style>
    	
	<script language="javascript" type="text/javascript" src="/Scripts/Controls/jTagCloud.js"></script>
	<script language="javascript" type="text/javascript"  src="/Scripts/Controls/jquery.jnotifica.min.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Frameworks/JS.Class/core.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.lightbox-0.5.js"></script>  
	<script language="javascript" type="text/javascript" src="/Scripts/controls/FancyBox/jquery.fancybox-1.3.1.js"></script> 
	<script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.timeago.js"></script>  
	<script language="javascript" src="/Scripts/Pages/user/search.js" type="text/javascript"></script>


	
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

	<% if (Model.IsAuthenticated){ %>
		<%= UserNavigationHelper.GetUserNavigation(Model.User.Username)%>
	<%} else { %>
		<li><a href="/" title="homepage">home</a></li>
		<li><a href="/about" title="about the site">about</a></li>
		<li><a href="/members" title="view our mem">our friends</a></li>
		<li><a href="/join" title="you want to contribute?">join</a></li>
		<li><a href="/login" title="login into the memorable moments">login</a></li>   
	<%} %>

</asp:Content>
