<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.Friends.FriendInviteView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	invite friends  - <%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="userheader">
            <%=UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
   
     <div class="nav">  
        <ul style="float:right;margin-top:-13px;" >
            <li><a  href="/<%=Model.Authorization.Owner.Username%>/friends">Find</a></li>
            <li><a   href="/<%=Model.Authorization.Owner.Username%>/friends/all">Friends (<%=Model.FriendsCount%>)</a></li>            
            <li><a class="youarehere" href="/<%=Model.Authorization.Owner.Username%>/friends/invite">Invite</a></li>
        </ul> 
        <br style="clear:both;display:inline;" />
    </div> 
    
    <p style="margin-bottom:35px;margin-top:20px;"> <%= Model.RemainingInvitationsCount %> invitation(s) remaining. </p>

    <% if(Model.RemainingInvitationsCount > 0) { %>

             <% using (Html.BeginForm()) { %>    
                 <div class="form">    

                  <% for (byte i = 0; i < Model.RemainingInvitationsCount; i++) { %>
                    <p><label>email</label><input id="email" name="email" type="text" value="" /></p>          
                   <% } %>    
                </div>            
                <p class="submit" ><%=Html.SubmitButton("Send", "Send Invitations")%></p>
                
            <%}%>
         <%}%>

         <%= Html.Message(Model.UIMessage) %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        @import url("/Content/css/friends/invite.css");
    </style>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            fadeIn('div#photoarea');
            var message = '<%= Model.UIMessage %>';
            showMessage(message);
        });

        function fadeIn(name) {

            var sidebar = $(name);
            sidebar.removeClass("hidden");
            sidebar.hide();
            sidebar.fadeIn("slow");           
        }
		    
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">
    <%= UserNavigationHelper.GetUserNavigation(Model.Authorization)%>
</asp:Content>
