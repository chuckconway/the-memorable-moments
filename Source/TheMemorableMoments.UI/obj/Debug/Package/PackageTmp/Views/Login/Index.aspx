<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Modal.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.LoginForm>" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="TheMemorableMoments.UI.Web.Helpers" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <form action="<%= Url.Action("Index") %>" method="post">   
    
    <div style="position:absolute; top:25%; left:44%;width:270px;" >
        
        <span class="field-validation-error" ><%= Model.ErrorMessage %></span>
        <p>
            <label>Username</label><%= Html.TextBox("Username", Model.Username, new Dictionary<string, object>{{"style","width:250px;"}})%>
            <%= Html.ValidationMessageFor(model => model.Username) %>
        </p>
        <p>
            <label>Password</label><%= Html.Password("Password", Model.Password, new Dictionary<string, object> { { "style", "width:250px;" } })%>
            <%= Html.ValidationMessageFor(model => model.Username) %>
        </p>
                
        <p><%= Html.CheckBox("RememberMe", new Dictionary<string, object>{{"class","normalwidth"}}) %> <label style="display:inline;"  for="rememberMe">Remember me?</label></p>
        <p class="submit" style="margin-top:30px;text-align:right;" ><%=Html.SubmitButton("login","Login") %></p>
        
        <p> <%=Html.Message(Model.UIMessage)%></p>
    
    </div>    
    </form>				
						
</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        @import url("/Content/css/common.css");
        div#body{width:auto;}
    </style>
</asp:Content>
