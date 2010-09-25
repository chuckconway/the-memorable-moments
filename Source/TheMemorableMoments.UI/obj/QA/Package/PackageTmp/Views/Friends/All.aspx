<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Friends.FriendAllView>" %>
<%@ Import Namespace="Chucksoft.Core.Web.Mvc.HtmlHelpers"%>
<%@ Import Namespace="TheMemorableMoments.UI.Models"%>
<%@ Import Namespace="TheMemorableMoments.UI.Models.Views" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	all friends  - <%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div id="userheader">
            <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
        </div>
    
     <div class="nav"> 
        <ul style="float:right;margin-top:-13px;" >
            <li><a href="/<%= Model.User.Username %>/friends">Find</a></li>
            <li><a  class="youarehere"  href="/<%= Model.User.Username %>/friends/all">Friends (<%= Model.Friends.Count %>)</a></li>            
            <li><a  href="/<%= Model.User.Username %>/friends/invite">Invite</a></li>
        </ul> 
        <br style="clear:both;display:inline;" />       
        
    </div> 
     
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
                                            string.Format("<a class=\"removefriend link\" href=\"/{0}/{1}/{2}\" >remove</a> ",Model.User.Username , "friends/remove",o.Id)
                                         },
                                         "results",
                                         "",
                                         "You have not added any friends.",
                                         "alt"
        ) %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        @import url("/Content/css/friends/all.css");
    </style>
    <script type="text/javascript" language="javascript" >
        $(document).ready(function() {
        
            $('a.removefriend').click(function() {
                var tr = $(this).parents('tr');
                var name = tr.find('span.albumname').text();
                $.jGrowl("<strong>" + name + "</strong> is <strong>no</strong> longer a friend.", { life: 2500 });
                tr.remove();
                $.get(this.href);
                return false;
            });

        });
	</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">
    <%= UserNavigationHelper.GetUserNavigation(Model.User.Username)%>
</asp:Content>
