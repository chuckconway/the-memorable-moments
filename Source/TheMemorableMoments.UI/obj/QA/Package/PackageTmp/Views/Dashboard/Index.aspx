<%@ Page Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Dashboard.DashboardView>" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Title="" %>
<%@ Import Namespace="TheMemorableMoments.Domain.Model.MediaClasses" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	dashboard  - <%= Model.User.DisplayName %>
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
    
    <p class="uploadlink" ><a class="buttonstyle" href="/<%= Model.User.Username %>/upload" >Upload Photos</a></p>
    <br class="clearboth" />
    
    <div class="leftcolumn">
        <h3 class="subtitle">activity</h3> 

        <%= Model.RecentFriendActivity  %>

    </div>

        <div class="middlecolumn">
    
             <h3 class="subtitle" >most viewed photos</h3>                
                 <%if(Model.TopTenViewed.Count > 0) {%>
            
                <ul class="topfive" >

                <%
                int count = 1; 
                foreach (var top in Model.TopTenViewed) {%>
          
                    <li>
                       <span ><%= count %> </span> 
                       <ul>
                            <li><img class="topten" src="<%= Model.UrlService.CreateImageUrl(Model.User,  top.GetImageByPhotoType(PhotoType.Thumbnail).FilePath) %>" alt="<%=  top.Title %>" /></li>
                            <li><span><%= top.ViewCount %> views</span></li>
                       </ul> 
                       <br class="clearboth" />
                    </li>
                
                <%
                    count++;
                }%>

            </ul>

        <% }%>
    
    </div>

    <div class="rightcolumn">

         <h3 class="subtitle" >statistics</h3>                
                 <%if(Model.SiteStats.Count > 0) {%>
            
            <ul class="dashboardstats" style="text-transform:lowercase;" >

            <% foreach (var stat in Model.SiteStats) {%>
      
                <li><%= stat.Name %> <span><%=stat.Count %></span></li>
            
            <% }%>

            </ul>

        <% }%>

       <h3 class="subtitle margintop30" >comments</h3> 

        <%if(Model.Comments.Count > 0) {%>
            
            <ul class="dashboardstats"  >

            <% foreach (var comment in Model.Comments) {%>
      
                <li><%= comment.Name %> <span><%=comment.Count %></span></li>
            
            <% }%>

            </ul>

        <% }%>

    </div>

    <br class="clearboth" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
        <style type="text/css"> 
            @import url("/Content/css/dashboard/index.css");
        </style> 
</asp:Content>
