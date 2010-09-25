<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Friends.FriendsView>" %>
<%@ Import Namespace="Chucksoft.Core.Web.Mvc.HtmlHelpers"%>
<%@ Import Namespace="TheMemorableMoments.UI.Models.Views" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	find friends  - <%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">
    <%= UserNavigationHelper.GetUserNavigation(Model.User.Username)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
    <div id="userheader">
            <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
    
     <div class="nav"> 
        <ul style="float:right;margin-top:-13px;" >
            <li><a class="youarehere" href="/<%= Model.User.Username %>/friends">Find</a></li>
            <li><a   href="/<%= Model.User.Username %>/friends/all">Friends (<%= Model.FriendsCount %>)</a></li>            
            <li><a  href="/<%= Model.User.Username %>/friends/invite">Invite</a></li>
        </ul> 
        <br style="clear:both;display:inline;" />
    </div> 
    
        <% using (Html.BeginForm()){%>
       <p class="privacy" style="text-align: right;" >    
    
       <span> 
        <input  name="searchText" type="text" style="width:300px;" value="<%= Model.Query ?? string.Empty  %>" maxlength="150" /> 
        <input type="submit" class="submit" value="Search" /> 
       </span>
       <span style="clear:both;" ></span>
     </p> 
     
     <p class="successmessage" ><%= Model.UIMessage %></p>
             <%= Html.Grid(Model.Friends, () => new List<string>
                                         {
                                             "",
                                             "",
                                             ""
                                         },
                                o => new List<string>
                                         {
                                             
                                            string.Format("<a class=\"albumlink\" href=\"/{0}\" style=\"text-decoration: none;\" title=\"Vists {2}'s site\">{1}</a>", o.Username, PhotoHtmlHelper.GetThumbnailImage(o.Media), o.DisplayName),
                                            string.Format("<span class=\"albumname\">{0}</span><p>photos <span>{1}</span></p><p>tags <span>{2}</span></p> <p>friends <span>{3}</span></p><span style=\"clear:both;\"></span>", o.DisplayName, o.PhotoCount, o.TagCount, o.FriendCount),
                                            string.Format("<a class=\"addfriend\" href=\"/{0}/{1}/{2}\" >add</a> ",Model.User.Username , "friends/add",o.Id)
                                         },
                                         "width100percent results",
                                         "",
                                         "",
                                         "alt"
        ) %>
        
        <%} %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">    
    <script type="text/javascript" src="/Scripts/Controls/jquery.ui.all.js"></script>
    <style type="text/css">
        @import url("/Content/css/friends/index.css");
    </style>
</asp:Content>
