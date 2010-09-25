<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.SettingsView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	account settings - <%= Model.Authorization.Owner.DisplayName %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">
    <%= UserNavigationHelper.GetUserNavigation(Model.Authorization)%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.Authorization.Owner.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <div id="userheader">
            <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
    
     <div class="nav"> 
        <ul style="float:right;" >
             <li><a   href="/<%=Model.Authorization.Owner.Username %>/account/">Details</a></li>
             <li><a class="youarehere" href="/<%=Model.Authorization.Owner.Username %>/account/settings">Settings</a></li>
        </ul> 
        <br style="clear:both;display:inline;" />
    </div> 
    
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) { %> 
    
        <div class="form">   

        <p><label style="display:inline;">I want to be notified when my friends add or update their photos</label><%= Html.CheckBox("EnableReceivingOfEmails", Model.EnableReceivingOfEmails, new Dictionary<string, object> { { "class", "checkboxinput" } })%></p>

        <h3>Photo diminsion</h3>
        <p>
            <label>Width (max.)</label>
            <%= Html.TextBox("WebViewMaxWidth", Model.WebViewMaxWidth, new Dictionary<string, object>{{"style","width:75px;"},{"maxlength","4"}})%>
            <%= Html.ValidationMessageFor(model =>model.WebViewMaxWidth) %>
        </p>
        <p>
            <label>Height (max.)</label>
            <%= Html.TextBox("WebViewMaxHeight", Model.WebViewMaxHeight, new Dictionary<string, object>{{"style","width:75px;"},{"maxlength","4"}})%>
            <%= Html.ValidationMessageFor(model => model.WebViewMaxHeight) %>
       </p>
        
        <p class="submit" ><%=Html.SubmitButton("Save", "Save")%></p>         
                 <%= Html.Message(Model.UIMessage) %>              
        </div>
        
    <% } %> 

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    
    <%= Html.Js(new[]{"/Scripts/Controls/jquery.jnotifica.min.js", 
    "/Scripts/Pages/common.js",
    "/Scripts/Pages/account/settings.js"}, "/js/usersettings.js") %>

    <script language="javascript" type="text/javascript">var message = '<%= Model.UIMessage %>'; </script>
</asp:Content>
