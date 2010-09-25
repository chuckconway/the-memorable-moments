<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.JoinView>" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 join
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">      
    
        <div class="form">    
        <p style="margin-top:30px;">the memorable moments is currently in <strong>beta</strong>. if you've received an invite token please enter it below and click join.</p>

        <% using (Html.BeginForm()) { %> 
        <p><label>Token</label><%= Html.TextBox("Token", string.Empty)%></p>        
      
        <p class="submit" ><%=Html.SubmitButton("Join", "Join")%></p>

        <p style="margin-top:60px;">if you'd like to <strong>join</strong> the beta please submit your <strong>email address</strong>. We will send you a token when we move into the next phase.</p>
             <p><label>Email</label><%= Html.TextBox("Email", "")%></p>        
          
            <p class="submit" ><%=Html.SubmitButton("AddToWaitingList", "Add to waiting list")%></p>
            
            <span class="message" ><%= Model.UIMessage %></span> 
        <% } %> 
        </div>           

</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        @import url("/Content/css/join/index.css");
        @import url("/Content/css/controls/jquery.lightbox-0.5.css"); 
    </style>
        
        <script language="javascript" type="text/javascript" src="/Scripts/Controls/jquery.jnotifica.min.js"></script>

        	<script type="text/javascript">
        	    $(document).ready(function () {
                
        	        var message = '<%= Model.UIMessage %>';
        	        showMessage(message);

        	    });
        </script>
</asp:Content>
