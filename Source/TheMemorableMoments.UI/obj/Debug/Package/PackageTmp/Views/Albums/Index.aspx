<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.AlbumModels.ManageAlbumsView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	albums - <%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="userheader">
            <%=UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>            
    </div>
    <br class="clearboth" />    
    
     <div class="margintop40">
         <div id="albumstree" >
            <ul id="albums" ></ul>     
         </div>

         <div id="photos" class="hyperlinks" ></div>
         <br class="clearboth;" />
     </div>
     
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    
    <%=Html.Js(new[]{"/Scripts/Frameworks/Core/corMVC.js",
                "/Scripts/Controls/Treeview/jquery.cookie.js",
                "/Scripts/Controls/Treeview/jquery.treeview.js",
                "/Scripts/Controls/Treeview/jquery.treeview.async.js",
                "/Scripts/Controls/FancyBox/jquery.easing-1.3.pack.js",
                "/Scripts/Controls/FancyBox/jquery.mousewheel-3.0.2.pack.js",
                "/Scripts/Controls/FancyBox/jquery.fancybox-1.3.1.js",
                "/Scripts/Controls/Dialog/jquery-ui-1.8.1.dialog.min.js",
                "/Scripts/Controls/jTagCloud.js",
                "/Scripts/controls/jquery.timeago.js",
                "/Scripts/Pages/albums/index.js",
                "/Scripts/mvc/controllers/album.js",
                }, "/js/albumindex.js") %>

	<script type="text/javascript" src="/Scripts/mvc/models/albums/albumService.js"></script> 
    <script type="text/javascript" src="/Scripts/mvc/models/albums/AlbumLandingPage.js"></script> 
    <script type="text/javascript" src="/Scripts/mvc/models/albums/AlbumView.js"></script> 
	<script type="text/javascript" src="/Scripts/mvc/views/album/albumView.js"></script> 
	<script type="text/javascript" src="/Scripts/mvc/views/album/albumLandingPageView.js"></script>
    <script type="text/javascript" src="/Scripts/mvc/views/pageNotFoundView.js"></script>  
    
    <script language="javascript" type="text/javascript" >
       var username = '<%= Model.Authorization.Owner.Username %>';
       var maxHeight = <%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>;
    </script>

     <%=Html.Css(new[]{"/Content/css/albums/index.css",
     "/Content/css/controls/jquery.treeview.css",
     "/Content/css/controls/jquery.fancybox-1.3.1.css",
     "/Content/css/controls/jquery-ui-1.8.1.custom.css"},"/js/albumindex.css")
     %>

    <style media="all" type="text/css">
        img{ max-height:<%= Model.Authorization.Owner.Settings.WebViewMaxHeight %>px; max-width:<%= Model.Authorization.Owner.Settings.WebViewMaxWidth %>px;}
        ul.groupedphotos.albums li {margin-bottom:50px;}
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">
 <%= Html.SiteNavigation(Model.Authorization) %>
</asp:Content>
