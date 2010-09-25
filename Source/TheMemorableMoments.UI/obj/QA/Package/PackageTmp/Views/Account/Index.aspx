<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.AccountDetailsView>" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	account details - <%= Model.User.DisplayName %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="navigation" runat="server">
    <%= UserNavigationHelper.GetUserNavigation(Model.User.Username)%>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="pageHeader" runat="server">
	<%= Model.User.DisplayName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <div id="userheader">
            <%= UserNavigationHelper.GetBreadCrumbs(Model.BreadCrumbs)%>
    </div>
    
     <div class="nav"> 
        <ul style="float:right;" >
             <li><a class="youarehere"  href="/<%=Model.User.Username %>/account/">Details</a></li>
             <li><a  href="/<%=Model.Username %>/account/settings">Settings</a></li>
        </ul> 
        <br style="clear:both;display:inline;" />
    </div> 
    
           <% Html.EnableClientValidation(); %>
           <% using (Html.BeginForm()) { %> 

        <div class="form">   

        <p>
            <label>First Name<span style="font-size:11px;" > (required, max 50 characters)</span></label>
            <%= Html.TextBoxFor(model => model.FirstName , new Dictionary<string, object>{{"maxlength","50"}})%> 
            <%= Html.ValidationMessageFor(model => model.FirstName) %>
        </p>
        <p>
            <label>Last Name<span style="font-size:11px;" > (required, max 50 characters)</span></label>
            <%= Html.TextBoxFor(model => model.LastName , new Dictionary<string, object>{{"maxlength","50"}})%>
            <%= Html.ValidationMessageFor(model => model.LastName) %>
        </p>
        <p>
            <label>Display Name<span style="font-size:11px;" > (required, max 100 characters)</span></label>
            <%= Html.TextBoxFor(model => model.DisplayName, new Dictionary<string, object>{{"maxlength","100"}})%>
            <%= Html.ValidationMessageFor(model => model.DisplayName) %>
        </p>
        <p>
            <label>Email <span style="font-size:11px;" > (required, ie. name@yourdomain.com)</span></label>
            <%= Html.TextBoxFor(model => model.Email, new Dictionary<string, object>{{"maxlength","200"}})%>
            <%= Html.ValidationMessageFor(model => model.Email) %>
        </p>
        

        <h3>Choose a Password</h3>
        <p><%: Html.ValidationMessage("PasswordDontMatch") %></p>
        <p>
            <label>Password <span style="font-size:11px;" > (required, min 6 characters)</span></label>
            <input type="password" name="Password" id="Password" maxlength="50" value="<%= Model.Password %>" />
            <%= Html.ValidationMessageFor(model => model.Password) %>
        </p>
        <p>
            <label>Confirm <span style="font-size:11px;" > (required, min 6 characters)</span></label>
             <input type="password" name="Confirm" id="Confirm" maxlength="50" value="<%= Model.Confirm %>" />
            <%= Html.ValidationMessageFor(model => model.Confirm) %>
         </p>
  
        <p class="submit" ><%=Html.SubmitButton("Save", "Save")%></p>
                 <%= Html.Message(Model.UIMessage) %>
        </div>
        
    <% } %> 

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    
    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/Microsoft/MicrosoftAjax.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/Frameworks/Microsoft/MicrosoftMvcValidation.js"></script>

    <script language="javascript" type="text/javascript">
       var message = '<%= Model.UIMessage %>';
    </script>
</asp:Content>
