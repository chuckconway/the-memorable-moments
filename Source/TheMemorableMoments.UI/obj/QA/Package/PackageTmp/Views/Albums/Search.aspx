<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.AlbumModels.AlbumSearchView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	search - <%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="userheader"><%=UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%> </div>
    <br style="clear:both;display:inline;" />
    
    <% using (Html.BeginForm()){%>
    <p style="text-align: right;" class="privacy"> 
       <span> 
        <input  name="searchText" type="text" style="width:300px;" value="<%= Model.Query ?? string.Empty  %>" maxlength="150" /> 
        <input type="submit" class="submit" value="search <%= Model.AlbumName %>" /> 
       </span>
       <span style="clear:both;" ></span>
     </p> 
     <%} %>

     <p> <%=Model.GridView.Albums.Count %> album(s) found</p>
    <% Html.RenderPartial("AlbumGrid", Model.GridView); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css"> 
        @import url("/Content/Generic.css");
        @import url("/Content/Site.css");
        @import url("/Content/colorbox.css");    
    </style> 
        

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">

 <%= Html.SiteNavigation(Model.GridView.IsAuthenticated, Model.User) %>

</asp:Content>
