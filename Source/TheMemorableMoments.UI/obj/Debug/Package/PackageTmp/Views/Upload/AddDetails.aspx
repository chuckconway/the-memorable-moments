<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Upload.AddDetailsView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.Authorization.Owner.Username %>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userheader">
            <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
    
    <ul class="uploadsteps" >
        <li><img src="/Content/Images/numbers/one.jpg" alt="Step One" /> Upload Photos</li>
        <li><img src="/Content/Images/numbers/two.jpg" alt="Step Two" /> <strong>Add Details</strong></li>
    </ul>
    <%= Html.Hidden("MediaKeys", Model.MediaKeys) %>
    <%= Html.Hidden("PersistentKey", Model.PersistentKey) %>
    <%= Html.Hidden("PhotoCount", Model.PhotoCount) %>
    <%= Html.Hidden("BatchId", Model.BatchId) %>


   <p style="margin-top:40px;" >Click any photo to add details.</p> 

   <div id="loadedimages" >    
        <ul id="groupedphotos" class="groupedphotos"></ul>   
   </div>

    <div style="clear:both;" ></div>
    <p style="text-align:right;font-size:13px;border-top:1px dotted #ccc;padding-top:10px;padding-right:10px;" ><a class="buttonstyle" href="/<%=Model.Authorization.Owner.Username %>/upload" title="upload more photos">Upload more photos</a>  <a class="buttonstyle" href="/<%= Model.Authorization.Owner.Username %>" title="return to the homepage">Return to homepage</a></p>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
	<style type="text/css"> 
       @import url("/Content/css/upload/adddetails.css");
       @import url("/Content/css/controls/jquery.fancybox-1.3.1.css");
    </style> 

    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/core/corMVC.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Controls/FancyBox/jquery.fancybox-1.3.1.pack.js"></script>

    <script language="javascript" type="text/javascript" src="/Scripts/mvc/Controllers/adddetails.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/mvc/Models/AddDetails/ViewModel.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/mvc/Views/AddDetails/view.js"></script>
    
    <script type="text/javascript">
        var username = '<%=Model.Authorization.Owner.Username %>';
	</script>
	
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="navigation" runat="server">
 <%= UserNavigationHelper.GetUserNavigation(Model.Authorization)%>
</asp:Content>
