<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Friends.FriendAllView>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<%@ Import Namespace="TheMemorableMoments.UI.Models.Views" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	all friends  - <%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div id="userheader">
            <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
        </div>
    
     <div class="nav"> 
        <ul style="float:right;margin-top:-13px;" >
            <li><a href="/<%= Model.Authorization.Owner.Username %>/friends">Find</a></li>
            <li><a  class="youarehere"  href="/<%= Model.Authorization.Owner.Username %>/friends/all">Friends (<%= Model.Friends.Count %>)</a></li>            
            <li><a  href="/<%= Model.Authorization.Owner.Username %>/friends/invite">Invite</a></li>
        </ul> 
        <br style="clear:both;display:inline;" />       
        
    </div> 

    <%= Html.Grid(Model.Friends).Attributes(new Dictionary<string, object>{{"class","width100percent results"}}).Empty("add your first friend.")
        .Columns(c => c.For(x => string.Format("<a class=\"friendlink\" href=\"/{0}\" style=\"text-decoration: none;\" title=\"Vists {2}'s site\">{1}</a>", x.Username, PhotoHtmlHelper.GetThumbnailImage(x.Media), x.DisplayName)).DoNotEncode())
        .Columns(c => c.For(x => string.Format("<span class=\"albumname\">{0}</span><p>photos <span>{1}</span></p><p>tags <span>{2}</span></p> <p>friends <span>{3}</span></p><span style=\"clear:both;\"></span>", x.DisplayName, x.PhotoCount, x.TagCount, x.FriendCount)).DoNotEncode())
        .Columns(c => c.For(x => string.Format("<a class=\"redhover removefriend\" href=\"/{0}/{1}/{2}\" >remove</a> ",Model.Authorization.Owner.Username , "friends/remove",x.FriendId)).DoNotEncode().Attributes(o => new Dictionary<string, object>{{"style", "text-align:right;width:50px;" }})) %>

        <%= Html.Message(Model.UIMessage) %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        @import url("/Content/css/friends/index.css");
    </style>
    <script type="text/javascript" language="javascript" >
        $(document).ready(function () {

            $('a.removefriend').click(function () {
                var tr = $(this).parents('tr');
                var name = tr.find('span.albumname').text();
                tr.remove();
                $.post(this.href);
                showMessage(name + ' is no longer your friend.');
                return false;
            });

        });
	</script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">
    <%= UserNavigationHelper.GetUserNavigation(Model.Authorization)%>
</asp:Content>
