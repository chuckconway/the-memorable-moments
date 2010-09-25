<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.LoginForm>" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <form action="<%= Url.Action("Index") %>" method="post">   
    
    <div class="form center" style="margin-top: 75px;" >
    <%= Html.ValidationSummary() %>
        <p><label>Username</label><%= Html.TextBox("Username", Model.Username, new Dictionary<string, object>{{"style","width:250px;"}})%> <%= Html.ValidationMessage("UsernameErrorMessage")%></p>
        <p><label>Password</label><%= Html.Password("Password", Model.Password, new Dictionary<string, object> { { "style", "width:250px;" } })%> <%= Html.ValidationMessage("PasswordErrorMessage")%></p>
                
        <p><%= Html.CheckBox("RememberMe", new Dictionary<string, object>{{"class","normalwidth"}}) %> <label style="display:inline;"  for="rememberMe">Remember me?</label></p>
        <p class="submit" ><%=Html.SubmitButton("login","Login") %></p>
        
        <p> <%= Html.Encode(Model.UIMessage)%></p>
    
    </div>    
    </form>				
						
</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        @import url("/Content/Generic.css");
        @import url("/Content/Site.css");   
    </style>
</asp:Content>
