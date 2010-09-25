<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Photos.PhotoView>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	view photo  - <%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader"  style="margin-bottom:40px;">
        <%= Model.RenderBreadCrumbs() %>
    </div>
    <div id="imagecontainer" class="hidden" >
          
            <%= Model.GetSizes(Model.Media) %>
             <img id="image" src="<%= Model.GetImageSrc(Model.Media, PhotoType.Websize)  %>" alt="<%= Model.Media.Title %>" />   
           <div class="details" >
             <h3 ><%=Model.Media.Title%></h3>
    		 <p><%=Model.Media.Description%></p>    
             
              <div id="photolinks" >
                    <%= Model.GetDetailSection(Model.Media, Model.IsAuthenticated) %>
              </div> 
          </div>
     </div>


</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

 <%= Html.SiteNavigation(Model.IsAuthenticated, Model.User) %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
        <style type="text/css"> 
            @import url("/Content/css/photos/show.css");   
            @import url("/Content/css/controls/jquery.fancybox-1.3.1.css");
        </style> 
       
        <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/JS.Class/core.js"></script>
        <script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.easing-1.3.pack.js"></script>
        <script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.mousewheel-3.0.2.pack.js"></script>
        <script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.fancybox-1.3.1.js"></script>
        <script language="javascript" type="text/javascript" src="/Scripts/Pages/photos/show.js"></script>
</asp:Content>
