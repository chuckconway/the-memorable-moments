<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Photos.ManagePhotosView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	manage photos - <%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">
	<%= UserNavigationHelper.GetUserNavigation(Model.User.Username, Model.IsAuthenticated)%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div id="userheader">
			<%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
	</div>
	
	 <div class="nav"> 
		<ul style="float:right;" >
           <%= Model.RenderManageTabs() %>
 			<li><a href="/<%= Model.User.Username %>/comments">Comments</a></li> 
		</ul> 
		<br class="clearboth" />
	</div>    
	<% using (Html.BeginForm()) { %> 
	<div class="hidden whitebackground">

        <% Html.RenderPartial(Model.PartialView, Model); %>
		
		<p><%=Model.Pagination.RenderedPagination%></p>
		
		</div>
   <%
	 }%>
     
        <%= Html.Message(Model.UIMessage) %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
	
	<script language="javascript" type="text/javascript" src="/Scripts/Frameworks/JS.Class/core.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.autosearch.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.jnotifica.min.js" ></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.fancybox-1.3.1.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.lightbox-0.5.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Controls/Dialog/jquery-ui-1.8.1.dialog.min.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Pages/common.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/Pages/Photos/index.js"></script>
	
	<script type="text/javascript">
	   var username = "<%= Model.User.Username %>";
	   var maxHeight = <%= Model.User.Settings.WebViewMaxHeight %>;
	   var maxWidth = <%= Model.User.Settings.WebViewMaxWidth %>;	  
       var message = '<%= Model.UIMessage %>';  
	</script>

	<style type="text/css">
		@import url("/Content/css/photos/index.css");
		@import url("/Content/css/controls/jquery.autosearch.css");  
		@import url("/Content/css/controls/jquery.fancybox-1.3.1.css"); 
		@import url("/Content/css/controls/jquery.lightbox-0.5.css"); 
		@import url("/Content/css/controls/jquery-ui-1.8.1.custom.css");
	</style>

	<style media="all" type="text/css">
		img{ max-height:<%= Model.User.Settings.WebViewMaxHeight %>px; max-width:<%= Model.User.Settings.WebViewMaxWidth %>px;}
	</style>
</asp:Content>
