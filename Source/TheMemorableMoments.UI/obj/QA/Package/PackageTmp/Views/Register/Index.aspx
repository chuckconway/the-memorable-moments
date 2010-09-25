<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TheMemorableMoments.UI.Models.Views.UserModels.UserRegisterView>" %>
<%@ Import Namespace="Microsoft.Web.Mvc"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 join
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

       <% Html.EnableClientValidation(); %>

       <% using (Html.BeginForm()) { %> 
       
        <div class="form">   
   
        <h3>Tell us about yourself</h3>
        <%=Model.ErrorMessage %>

        <p>
            <label>Username <span style="font-size:11px;" >(required, must be unique, min 3 characters - max 40 characters)</span></label>
            <%= Html.TextBox("Username", Model.Username, new Dictionary<string, object>{{"style", "width: 432px;"}, {"maxlength","40"}})%> 
            <%=Html.SubmitButton("CheckUsername", "Check Availability", new Dictionary<string, object>{{"style","margin:0;"}})%>
            <%= Html.ValidationMessageFor(model => model.Username) %>
         </p>
        <p>
            <label>First Name <span style="font-size:11px;" >(required, max 50 characters)</span></label>
            <%= Html.TextBoxFor(model => model.FirstName, new Dictionary<string, object>{{"maxlength","50"}})%> 
            <%= Html.ValidationMessageFor(model => model.FirstName) %>
        </p>
        <p>
            <label>Last Name<span style="font-size:11px;" >(required, max 50 characters)</span></label>
            <%= Html.TextBoxFor(model => model.LastName, new Dictionary<string, object>{{"maxlength","50"}})%>
            <%= Html.ValidationMessageFor(model => model.LastName) %>
       </p>
        <p>
            <label>Display Name <span style="font-size:11px;" >(required, max 100 characters)</span></label>    
            <%= Html.TextBoxFor(model => model.DisplayName, new Dictionary<string, object>{{"maxlength","100"}})%>
            <%= Html.ValidationMessageFor(model => model.DisplayName) %>
        </p>
        <p>
            <label>Email <span style="font-size:11px;" >(required, ie. name@yourdomain.com)</span></label>
            <%= Html.TextBoxFor(model => model.Email, new Dictionary<string, object>{{"maxlength","200"}})%>
            <%= Html.ValidationMessageFor(model => model.Email) %>
        </p>
        
        <h3>Choose a Password</h3>
        <p><%: Html.ValidationMessage("PasswordDontMatch") %></p>
        <p>
            <label>Password <span style="font-size:11px;" >(required, min 6 characters)</span></label>
            <%= Html.PasswordFor(model => model.Password, new Dictionary<string, object>{{"maxlength","50"}})%> 
            <%= Html.ValidationMessageFor(model => model.Password) %>
        </p>
        <p>
            <label>Confirm</label>
            <%= Html.PasswordFor(model => model.Confirm, new Dictionary<string, object>{{"maxlength","50"}})%> 
            <%= Html.ValidationMessageFor(model => model.Confirm) %>
         </p>
        
        
        <p class="submit" ><%=Html.SubmitButton("Join", "Join")%></p>        
        <p> <%= Html.Encode(Model.UIMessage)%></p>
    
        </div>
        
    <% } %> 

</asp:Content>

<asp:Content ID="content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        @import url("/Content/Generic.css");
        @import url("/Content/Site.css");   
    </style>
        
        <script language="javascript" type="text/javascript" src="/Scripts/jquery.jnotifica.min.js"></script>

        <script language="javascript" type="text/javascript" src="/Scripts/Microsoft/MicrosoftAjax.js"></script>
        <script language="javascript" type="text/javascript" src="/Scripts/Microsoft/MicrosoftMvcValidation.js"></script>

    	<script type="text/javascript">
    	    $(document).ready(function () {

    	        var message = '<%= Model.UIMessage %>';
    	        showMessage(message);

    	        $('input#CheckUsername').click(function () {

    	            var self = $('input#Username');
    	            var val = self.val();

    	            $.post("/register/CheckAvailability/" + val, function (data) {

    	                var messageContainer = $('span#Username_validationMessage');
    	                messageContainer.text("");

    	                var spanMessage = $('<span>');
    	                var displayMessage = function (color, message) {

    	                    spanMessage.css("color", color);
    	                    spanMessage.css("font-weight", "bold");
    	                    spanMessage.html(message);
    	                    messageContainer.append(spanMessage);

    	                };

    	                if (data == "0") {
    	                    displayMessage("green", "It's available!");
    	                } else {
    	                    displayMessage("red", "Sorry, it's unavailable.");
    	                }

    	            });
    	            return false;

    	        });
    	    });
    </script>
</asp:Content>
