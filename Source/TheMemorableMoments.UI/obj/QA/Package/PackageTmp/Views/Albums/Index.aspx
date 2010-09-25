<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.AlbumModels.ManageAlbumsView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	albums - <%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
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
    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/JS.Class/core.js"></script>
    
    <!-- jQuery and corMVC Application scripts. --> 
	<script type="text/javascript" src="/Scripts/Frameworks/Core/corMVC.js"></script> 
       
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/Treeview/thememorablemoments.album.details.js"></script>    
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/Treeview/jquery.cookie.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/Treeview/jquery.treeview.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/Treeview/jquery.treeview.async.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.easing-1.3.pack.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.mousewheel-3.0.2.pack.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.fancybox-1.3.1.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/Dialog/jquery-ui-1.8.1.dialog.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.lightbox-0.5.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/jTagCloud.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/controls/jquery.timeago.js"></script>   
    <script type="text/javascript" src="/Scripts/Pages/albums/index.js"></script>

    <!-- Controllers. --> 
	<script type="text/javascript" src="/Scripts/mvc/controllers/album.js"></script> 
	
	<!-- Model. --> 
	<script type="text/javascript" src="/Scripts/mvc/models/albums/albumService.js"></script> 
    <script type="text/javascript" src="/Scripts/mvc/models/albums/AlbumLandingPage.js"></script> 
    <script type="text/javascript" src="/Scripts/mvc/models/albums/AlbumView.js"></script> 
	
	<!-- View. --> 
	<script type="text/javascript" src="/Scripts/mvc/views/album/albumView.js"></script> 
	<script type="text/javascript" src="/Scripts/mvc/views/album/albumLandingPageView.js"></script>
    <script type="text/javascript" src="/Scripts/mvc/views/pageNotFoundView.js"></script>  
    
    <script language="javascript" type="text/javascript" >
       var username = "<%= Model.User.Username %>";

       var maxHeight = <%= Model.User.Settings.WebViewMaxHeight %>;
       var maxWidth = <%= Model.User.Settings.WebViewMaxWidth %>;
    </script>

    <style type="text/css"> 
        @import url("/Content/css/albums/index.css");   
        @import url("/Content/css/controls/jquery.treeview.css"); 
        @import url("/Content/css/controls/jquery.lightbox-0.5.css");
        @import url("/Content/css/controls/jquery.fancybox-1.3.1.css");
        @import url("/Content/css/controls/jquery-ui-1.8.1.custom.css");
    </style> 

    <style media="all" type="text/css">
        img{ max-height:<%= Model.User.Settings.WebViewMaxHeight %>px; max-width:<%= Model.User.Settings.WebViewMaxWidth %>px;}
        ul.groupedphotos.albums li {margin-bottom:50px;}
    </style>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

 <%= Html.SiteNavigation(Model.IsAuthenticated, Model.User) %>

</asp:Content>
