<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.HomeView>" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model"%>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    the memorable moments
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="search">
             <form action="/Search" method="get" name="searchForm" > 
		        <input  name="text" type="text" maxlength="150"  /> 
                <input type="submit" class="submit" value="Search" /> 
	        </form>
    </div>
    <div id="randomphotos" >
         <ul>  
          <%
              foreach (Media media in Model.Media)
              {
                  %><li><%=Model.GetImageLink(media) %></li><%= Environment.NewLine %><% 
              }
          %>  
           </ul> 
      </div>
</asp:Content>
<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
         @import url("/Content/css/home/index.css");
     </style>
     <script language="javascript" type="text/javascript" src="/Scripts/Pages/home/index.js"></script>
     <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/JS.Class/core.js"></script>
</asp:Content>
