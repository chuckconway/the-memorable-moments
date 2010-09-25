<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.DirectoryView>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model"%>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	site directory
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="directory">

    <%

    const string format = "<div class=\"container\"><span class=\"outer\" ><a class=\"member\" href=\"/{0}\"><span><img src=\"{1}\" /></span> </a></span><p class=\"clearboth\" >{2}</p></div>";

    foreach (Member member in Model.Members)
    {
      %><%= string.Format(format, member.User.Username, Model.GetImageSrc(member.Media, PhotoType.Thumbnail), member.User.FirstName + " " + member.User.LastName)%><%
    }
    
    %>


</div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">@import url("/Content/css/directory/index.css");</style>
</asp:Content>